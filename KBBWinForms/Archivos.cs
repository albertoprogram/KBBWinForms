using DocumentFormat.OpenXml.Packaging;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using System.Text;
using DocumentFormat.OpenXml.Wordprocessing;
using WordA = Microsoft.Office.Interop.Word;
using WApplication = Microsoft.Office.Interop.Word.Application;
using WDocument = Microsoft.Office.Interop.Word.Document;
using Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;

namespace KBBWinForms
{
    public class Archivos
    {
        #region Variables
        private int id;
        private string nombre;
        private byte[] archivo;
        private string extension;
        private string observaciones;
        List<int> listArchivos = new List<int>();
        string inIDsCategorias;
        int idCategoria;

        SqlConnection conexionDB = new SqlConnection(ConexionDB.cadenaConexionSQLServer);
        #endregion

        #region Propiedades        
        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public byte[] Archivo { get => archivo; set => archivo = value; }
        public string Extension { get => extension; set => extension = value; }
        public string Observaciones { get => observaciones; set => observaciones = value; }
        #endregion

        #region AgregarDocumento
        public string AgregarDocumento(short[] categorias)
        {
            using (SqlCommand comandoSql = new SqlCommand())
            {
                comandoSql.CommandType = CommandType.Text;
                comandoSql.CommandText =
                    "INSERT INTO Archivos " +
                    "(Nombre,Archivo,Extension,Observaciones) " +
                    "VALUES (@Nombre,@Archivo,@Extension,@Observaciones); " +
                    "SELECT SCOPE_IDENTITY();";
                comandoSql.Connection = conexionDB;

                comandoSql.Parameters.AddWithValue("@Nombre", Nombre);
                comandoSql.Parameters.AddWithValue("@Archivo", Archivo);
                comandoSql.Parameters.AddWithValue("@Extension", Extension);
                comandoSql.Parameters.AddWithValue("@Observaciones", Observaciones);

                conexionDB.Open();

                int identityValue = Convert.ToInt32(comandoSql.ExecuteScalar());

                foreach (short categoria in categorias)
                {
                    comandoSql.CommandText =
                    "INSERT INTO ArchivosCategorias " +
                    "(ArchivoID,CategoriaID) " +
                    "VALUES (@ArchivoID,@CategoriaID)";

                    comandoSql.Parameters.Clear();

                    comandoSql.Parameters.AddWithValue("@ArchivoID", identityValue);
                    comandoSql.Parameters.AddWithValue("@CategoriaID", categoria);

                    comandoSql.ExecuteNonQuery();
                }

                conexionDB.Close();
            }

            return "Agregado con éxito";
        }
        #endregion

        #region ListarArchivos
        public System.Data.DataTable ListarArchivos(string pagina, string cantidadRegistros, string categoria, string busqueda)
        {
            short registrosIgnorar = 0;
            listArchivos.Clear();
            inIDsCategorias = string.Empty;
            System.Data.DataTable dt = new System.Data.DataTable();

            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Observaciones", typeof(string));
            dt.Columns.Add("Paginas", typeof(string));

            if (!string.IsNullOrWhiteSpace(pagina))
            {
                registrosIgnorar = short.Parse(pagina);
                registrosIgnorar -= 1;
                registrosIgnorar *= short.Parse(cantidadRegistros);
            }

            if (busqueda == string.Empty && categoria != string.Empty)
            {
                using (SqlCommand comandoSql = new SqlCommand())
                {
                    comandoSql.CommandType = CommandType.Text;
                    comandoSql.CommandText =
                        "SELECT ID " +
                        "FROM Categorias " +
                        "WHERE Categoria = '" + categoria + "'";
                    comandoSql.Connection = conexionDB;

                    conexionDB.Open();

                    idCategoria = Convert.ToInt32(comandoSql.ExecuteScalar());

                    comandoSql.CommandText =
                        "SELECT ArchivoID " +
                        "FROM ArchivosCategorias " +
                        "WHERE CategoriaID = " + idCategoria.ToString();
                    comandoSql.Connection = conexionDB;

                    SqlDataReader reader;

                    using (reader = comandoSql.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                listArchivos.Add(Convert.ToInt32(reader["ArchivoID"]));
                            }
                        }
                    }

                    inIDsCategorias = string.Join(",", listArchivos);

                    if (listArchivos.Count > 0)
                    {
                        //comandoSql.CommandText =
                        //"SELECT ID,Nombre,Observaciones " +
                        //"FROM Archivos " +
                        //$"WHERE ID IN ({inIDsCategorias}) " +
                        //"ORDER BY ID " +
                        //"OFFSET " + registrosIgnorar.ToString() + " ROWS " +
                        //"FETCH NEXT " + cantidadRegistros + " ROWS ONLY";

                        comandoSql.CommandText =
                        "SELECT ID,Nombre,Observaciones " +
                        "FROM Archivos " +
                        $"WHERE ID IN ({inIDsCategorias}) " +
                        "ORDER BY Nombre";

                        reader.Close();
                        reader.Dispose();

                        reader = comandoSql.ExecuteReader();

                        if (reader.HasRows) dt.Load(reader);
                    }

                    reader.Close();
                    reader.Dispose();
                    conexionDB.Close();
                }
            }
            else if (busqueda != string.Empty && categoria == string.Empty)
            {
                //string query = "SELECT " +
                //"ID, Nombre, Observaciones " +
                //"FROM Archivos " +
                //"WHERE Nombre LIKE '%" + busqueda + "%' " +
                //"OR Observaciones LIKE '%" + busqueda + "%' " +
                //"ORDER BY Nombre " +
                //"OFFSET " + registrosIgnorar.ToString() + " ROWS " +
                //"FETCH NEXT " + cantidadRegistros + " ROWS ONLY";

                string query = "SELECT " +
                "ID, Nombre, Observaciones " +
                "FROM Archivos " +
                "WHERE Nombre LIKE '%" + busqueda + "%' " +
                "OR Observaciones LIKE '%" + busqueda + "%' " +
                "ORDER BY Nombre";

                using (SqlConnection connection = new SqlConnection(ConexionDB.cadenaConexionSQLServer))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                            }
                        }
                    }

                    ///////////////////////////////////////////////////////////////////////////////////
                    ///Búsqueda en documentos

                    System.Data.DataTable dataTableExtensiones = new System.Data.DataTable("Extensiones");
                    string extension = string.Empty;
                    string pages;

                    string ruta = AppDomain.CurrentDomain.BaseDirectory;

                    string carpetaTemporal = ruta + @"temp\";

                    if (!Directory.Exists(carpetaTemporal))
                        Directory.CreateDirectory(carpetaTemporal);

                    dataTableExtensiones.Columns.Add("ID", typeof(int));
                    dataTableExtensiones.Columns.Add("Extension", typeof(string));
                    dataTableExtensiones.Columns.Add("Archivo", typeof(byte[]));
                    dataTableExtensiones.Columns.Add("Nombre", typeof(string));
                    dataTableExtensiones.Columns.Add("Observaciones", typeof(string));

                    extension = "docx";

                    query = "SELECT " +
                    "ID,Extension,Archivo,Nombre,Observaciones " +
                    "FROM Archivos " +
                    $"WHERE Extension LIKE '%.{extension}' " +
                    "ORDER BY ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    DataRow dataRow = dataTableExtensiones.NewRow();
                                    dataRow["ID"] = reader.GetInt32("ID");
                                    dataRow["Extension"] = reader.GetString("Extension");
                                    dataRow["Archivo"] = (byte[])reader["Archivo"];
                                    dataRow["Nombre"] = reader.GetString("Nombre");
                                    dataRow["Observaciones"] = reader.GetString("Observaciones");
                                    dataTableExtensiones.Rows.Add(dataRow);
                                }
                            }
                        }
                    }

                    foreach (DataRow row in dataTableExtensiones.Rows)
                    {
                        string ubicacionCompleta = carpetaTemporal + row["Extension"];

                        if (File.Exists(ubicacionCompleta))
                            File.Delete(ubicacionCompleta);

                        File.WriteAllBytes(ubicacionCompleta, (byte[])row["Archivo"]);

                        //Abrir el documento, leerlo y ver si hay alguna expresión según la búsqueda que se escribió
                        bool found = SearchTextInWordDocument(ubicacionCompleta, busqueda, out pages);

                        if (found)
                        {
                            DataRow dataRow = dt.NewRow();
                            dataRow["ID"] = row["ID"];
                            dataRow["Nombre"] = row["Nombre"];
                            dataRow["Observaciones"] = row["Observaciones"];
                            dataRow["Paginas"] = pages;
                            dt.Rows.Add(dataRow);
                        }
                    }

                    extension = "xlsx";

                    query = "SELECT " +
                    "ID,Extension,Archivo,Nombre,Observaciones " +
                    "FROM Archivos " +
                    $"WHERE Extension LIKE '%.{extension}' " +
                    "ORDER BY ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    DataRow dataRow = dataTableExtensiones.NewRow();
                                    dataRow["ID"] = reader.GetInt32("ID");
                                    dataRow["Extension"] = reader.GetString("Extension");
                                    dataRow["Archivo"] = (byte[])reader["Archivo"];
                                    dataRow["Nombre"] = reader.GetString("Nombre");
                                    dataRow["Observaciones"] = reader.GetString("Observaciones");
                                    dataTableExtensiones.Rows.Add(dataRow);
                                }
                            }
                        }
                    }

                    foreach (DataRow row in dataTableExtensiones.Rows)
                    {
                        string ubicacionCompleta = carpetaTemporal + row["Extension"];

                        if (File.Exists(ubicacionCompleta))
                            File.Delete(ubicacionCompleta);

                        File.WriteAllBytes(ubicacionCompleta, (byte[])row["Archivo"]);

                        //Abrir el documento, leerlo y ver si hay alguna expresión según la búsqueda que se escribió
                        bool found = SearchTextInExcelFile(ubicacionCompleta, busqueda, out pages);

                        if (found)
                        {
                            DataRow dataRow = dt.NewRow();
                            dataRow["ID"] = row["ID"];
                            dataRow["Nombre"] = row["Nombre"];
                            dataRow["Observaciones"] = row["Observaciones"];
                            dataRow["Paginas"] = pages;
                            dt.Rows.Add(dataRow);
                        }
                    }

                    extension = "ppt";

                    query = "SELECT " +
                    "ID,Extension,Archivo " +
                    "FROM Archivos " +
                    $"WHERE Extension LIKE '%.{extension}' " +
                    "ORDER BY ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    DataRow dataRow = dataTableExtensiones.NewRow();
                                    dataRow["ID"] = reader.GetInt32("ID");
                                    dataRow["Extension"] = reader.GetString("Extension");
                                    dataTableExtensiones.Rows.Add(dataRow);
                                }
                            }
                        }
                    }

                    extension = "pptx";

                    query = "SELECT " +
                    "ID,Extension,Archivo " +
                    "FROM Archivos " +
                    $"WHERE Extension LIKE '%.{extension}' " +
                    "ORDER BY ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    DataRow dataRow = dataTableExtensiones.NewRow();
                                    dataRow["ID"] = reader.GetInt32("ID");
                                    dataRow["Extension"] = reader.GetString("Extension");
                                    dataTableExtensiones.Rows.Add(dataRow);
                                }
                            }
                        }
                    }

                    extension = "pps";

                    query = "SELECT " +
                    "ID,Extension,Archivo " +
                    "FROM Archivos " +
                    $"WHERE Extension LIKE '%.{extension}' " +
                    "ORDER BY ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    DataRow dataRow = dataTableExtensiones.NewRow();
                                    dataRow["ID"] = reader.GetInt32("ID");
                                    dataRow["Extension"] = reader.GetString("Extension");
                                    dataTableExtensiones.Rows.Add(dataRow);
                                }
                            }
                        }
                    }

                    extension = "ppsx";

                    query = "SELECT " +
                    "ID,Extension,Archivo " +
                    "FROM Archivos " +
                    $"WHERE Extension LIKE '%.{extension}' " +
                    "ORDER BY ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    DataRow dataRow = dataTableExtensiones.NewRow();
                                    dataRow["ID"] = reader.GetInt32("ID");
                                    dataRow["Extension"] = reader.GetString("Extension");
                                    dataTableExtensiones.Rows.Add(dataRow);
                                }
                            }
                        }
                    }

                    extension = "pdf";

                    query = "SELECT " +
                    "ID,Extension " +
                    "FROM Archivos " +
                    $"WHERE Extension LIKE '%.{extension}' " +
                    "ORDER BY ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    DataRow dataRow = dataTableExtensiones.NewRow();
                                    dataRow["ID"] = reader.GetInt32("ID");
                                    dataRow["Extension"] = reader.GetString("Extension");
                                    dataTableExtensiones.Rows.Add(dataRow);
                                }
                            }
                        }
                    }

                    extension = "txt";

                    query = "SELECT " +
                    "ID,Extension,Archivo " +
                    "FROM Archivos " +
                    $"WHERE Extension LIKE '%.{extension}' " +
                    "ORDER BY ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    DataRow dataRow = dataTableExtensiones.NewRow();
                                    dataRow["ID"] = reader.GetInt32("ID");
                                    dataRow["Extension"] = reader.GetString("Extension");
                                    dataTableExtensiones.Rows.Add(dataRow);
                                }
                            }
                        }
                    }


                    ///////////////////////////////////////////////////////////////////////////////////

                }
            }

            return dt;

        }
        #endregion

        #region ArchivoPorId
        public System.Data.DataTable ArchivoPorId()
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            using (SqlCommand comandoSql = new SqlCommand())
            {
                comandoSql.CommandType = CommandType.Text;
                comandoSql.CommandText = "SELECT * FROM Archivos WHERE ID = @id";
                comandoSql.Connection = conexionDB;

                comandoSql.Parameters.AddWithValue("@id", Id);

                conexionDB.Open();

                SqlDataReader reader = comandoSql.ExecuteReader();

                if (reader.HasRows) dt.Load(reader);

                reader.Close();
                conexionDB.Close();

                return dt;
            }
        }
        #endregion

        #region FiltroArchivos
        public List<Archivos> FiltroArchivos()
        {
            var tabla = ArchivoPorId();

            var infoArchivo = new List<Archivos>();

            foreach (DataRow item in tabla.Rows)
            {
                infoArchivo.Add(new Archivos
                {
                    Id = Convert.ToInt32(item[0]),
                    Nombre = item[1].ToString(),
                    Archivo = (byte[])item[2],
                    Extension = item[3].ToString()
                });
            }

            return infoArchivo;
        }
        #endregion

        #region CantidadTotalArchivos
        public long CantidadTotalArchivos(string categoria, string busqueda)
        {
            int total = 0;
            if (busqueda == string.Empty && categoria != string.Empty)
            {
                using (SqlCommand comandoSql = new SqlCommand())
                {
                    comandoSql.CommandType = CommandType.Text;
                    comandoSql.CommandText = $"SELECT COUNT(*) FROM ArchivosCategorias WHERE CategoriaID = {idCategoria}";
                    comandoSql.Connection = conexionDB;

                    conexionDB.Open();

                    SqlDataReader reader = comandoSql.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            total = reader.GetInt32(0);
                        }
                    }

                    reader.Close();
                    conexionDB.Close();
                }
            }
            else if (busqueda != string.Empty && categoria == string.Empty)
            {
                using (SqlCommand comandoSql = new SqlCommand())
                {
                    comandoSql.CommandType = CommandType.Text;
                    comandoSql.CommandText = $"SELECT COUNT(*) FROM Archivos " +
                        "WHERE Nombre LIKE '%" + busqueda + "%' " +
                        "OR Observaciones LIKE '%" + busqueda + "%'";
                    comandoSql.Connection = conexionDB;

                    conexionDB.Open();

                    SqlDataReader reader = comandoSql.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            total = reader.GetInt32(0);
                        }
                    }

                    reader.Close();
                    conexionDB.Close();
                }
            }

            return total;
        }
        #endregion

        #region SearchTextInWordDocument
        public bool SearchTextInWordDocument(string filePath, string searchText, out string pages)
        {
            //try
            //{
            //    // Check if the file exists
            //    if (!File.Exists(filePath))
            //    {
            //        MessageBox.Show("The file does not exist.");
            //        return false;
            //    }

            //    // Open the Word document
            //    using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(filePath, false))
            //    {
            //        // Access the main document part
            //        var docText = GetDocumentText(wordDoc);

            //        // Search for the specified text
            //        return docText.Contains(searchText, StringComparison.OrdinalIgnoreCase);
            //    }
            //}
            //catch (FileFormatException ex)
            //{
            //    // Handle specific file format exceptions
            //    MessageBox.Show($"File format error: {ex.Message}");
            //    return false;
            //}
            //catch (IOException ex)
            //{
            //    // Handle IO exceptions
            //    MessageBox.Show($"IO error: {ex.Message}");
            //    return false;
            //}
            //catch (UnauthorizedAccessException ex)
            //{
            //    // Handle unauthorized access exceptions
            //    MessageBox.Show($"Access error: {ex.Message}");
            //    return false;
            //}
            //catch (Exception ex)
            //{
            //    // Handle all other exceptions
            //    MessageBox.Show($"An error occurred: {ex.Message}");
            //    return false;
            //}

            WApplication wordApp = null;
            WDocument wordDoc = null;
            pages = string.Empty;

            try
            {
                // Check if the file exists
                if (!File.Exists(filePath))
                {
                    MessageBox.Show($"El archivo no existe: {filePath}");
                    return false;
                }

                // Initialize Word application
                wordApp = new WApplication();
                wordDoc = wordApp.Documents.Open(filePath, ReadOnly: true);

                bool textFound = false;
                var foundPages = new List<int>();

                // Loop through each story in the document
                foreach (WordA.Range storyRange in wordDoc.StoryRanges)
                {
                    WordA.Range searchRange = storyRange.Duplicate;
                    while (searchRange.Find.Execute(searchText, MatchCase: false, MatchWholeWord: false))
                    {
                        textFound = true;
                        foundPages.Add((int)searchRange.Information[WdInformation.wdActiveEndPageNumber]);
                        searchRange.Collapse(WdCollapseDirection.wdCollapseEnd);
                    }
                }

                // Display the results
                if (textFound)
                {
                    pages = string.Join(", ", foundPages.Distinct());
                    //MessageBox.Show($"The text '{searchText}' was found on the following pages: {pages}");
                }
                else
                {
                    //MessageBox.Show($"The text '{searchText}' was not found.");
                }

                return textFound;
            }
            catch (FileFormatException ex)
            {
                // Handle specific file format exceptions
                MessageBox.Show($"File format error: {ex.Message}");
                return false;
            }
            catch (IOException ex)
            {
                // Handle IO exceptions
                MessageBox.Show($"IO error: {ex.Message}");
                return false;
            }
            catch (UnauthorizedAccessException ex)
            {
                // Handle unauthorized access exceptions
                MessageBox.Show($"Access error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                // Handle all other exceptions
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
            finally
            {
                // Ensure the document and application are properly closed
                if (wordDoc != null)
                {
                    wordDoc.Close(false);
                }
                if (wordApp != null)
                {
                    wordApp.Quit(false);
                }
            }
        }
        #endregion

        #region GetDocumentText
        //private string GetDocumentText(WordprocessingDocument wordDoc)
        //{
        //    using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
        //    {
        //        return sr.ReadToEnd();
        //    }
        //}
        #endregion

        #region SearchTextInExcelFile
        public bool SearchTextInExcelFile(string filePath, string searchText, out string foundCells)
        {
            Excel.Application excelApp = null;
            Excel.Workbook excelWorkbook = null;
            foundCells = string.Empty;

            try
            {
                // Check if the file exists
                if (!File.Exists(filePath))
                {
                    MessageBox.Show($"El archivo no existe: {filePath}");
                    return false;
                }

                // Initialize Excel application
                excelApp = new Excel.Application();
                excelWorkbook = excelApp.Workbooks.Open(filePath, ReadOnly: true);

                bool textFound = false;
                var foundCellAddresses = new List<string>();

                // Loop through each worksheet in the workbook
                foreach (Excel.Worksheet worksheet in excelWorkbook.Worksheets)
                {
                    Excel.Range usedRange = worksheet.UsedRange;

                    // Loop through each cell in the used range
                    foreach (Excel.Range cell in usedRange.Cells)
                    {
                        if (cell.Value != null && cell.Value.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase))
                        {
                            textFound = true;
                            foundCellAddresses.Add(cell.Address);
                        }
                    }
                }

                // Display the results
                if (textFound)
                {
                    foundCells = string.Join(", ", foundCellAddresses.Distinct());
                    //MessageBox.Show($"The text '{searchText}' was found in the following cells: {foundCells}");
                }
                else
                {
                    //MessageBox.Show($"The text '{searchText}' was not found.");
                }

                return textFound;
            }
            catch (FileFormatException ex)
            {
                // Handle specific file format exceptions
                MessageBox.Show($"File format error: {ex.Message}");
                return false;
            }
            catch (IOException ex)
            {
                // Handle IO exceptions
                MessageBox.Show($"IO error: {ex.Message}");
                return false;
            }
            catch (UnauthorizedAccessException ex)
            {
                // Handle unauthorized access exceptions
                MessageBox.Show($"Access error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                // Handle all other exceptions
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
            finally
            {
                // Ensure the workbook and application are properly closed
                if (excelWorkbook != null)
                {
                    excelWorkbook.Close(false);
                }
                if (excelApp != null)
                {
                    excelApp.Quit();
                }
            }
        }
        #endregion
    }
}
