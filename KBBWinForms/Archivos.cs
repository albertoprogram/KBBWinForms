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
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;
using System.Collections.Generic;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

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
            try
            {
                using (SqlCommand comandoSql = new SqlCommand())
                {
                    conexionDB.Open();

                    comandoSql.CommandType = CommandType.Text;
                    comandoSql.CommandText = "SELECT ID FROM Archivos WHERE Nombre = @Nombre";
                    comandoSql.Connection = conexionDB;

                    comandoSql.Parameters.AddWithValue("@Nombre", Nombre);

                    object result = comandoSql.ExecuteScalar();

                    if (result != null)
                    {
                        int id = Convert.ToInt32(result);
                        if (id > 0)
                        {
                            conexionDB.Close();
                            return "Archivo ya existe";
                        }
                    }

                    comandoSql.Parameters.Clear();

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

                    int identityValue = Convert.ToInt32(comandoSql.ExecuteScalar());

                    foreach (short categoria in categorias)
                    {
                        comandoSql.Parameters.Clear();

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
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return "ERROR";
            }
        }
        #endregion

        #region ActualizarDocumento
        public string ActualizarDocumento(int id, short[] categorias, bool archivoSeleccionado)
        {
            try
            {
                if (archivoSeleccionado == false)
                {
                    using (SqlCommand comandoSql = new SqlCommand())
                    {
                        comandoSql.CommandType = CommandType.Text;
                        comandoSql.CommandText =
                            "UPDATE Archivos " +
                            "SET Observaciones = " +
                            "@Observaciones " +
                            $"WHERE ID = {id}";
                        comandoSql.Connection = conexionDB;

                        comandoSql.Parameters.AddWithValue("@Observaciones", Observaciones);

                        conexionDB.Open();

                        comandoSql.ExecuteNonQuery();

                        //DELETE Categorías
                        comandoSql.CommandType = CommandType.Text;
                        comandoSql.CommandText =
                            "DELETE FROM ArchivosCategorias " +
                            $"WHERE ArchivoID = {id}";
                        comandoSql.Connection = conexionDB;

                        comandoSql.ExecuteNonQuery();

                        foreach (short categoria in categorias)
                        {
                            comandoSql.CommandText =
                            "INSERT INTO ArchivosCategorias " +
                            "(ArchivoID,CategoriaID) " +
                            "VALUES (@ArchivoID,@CategoriaID)";

                            comandoSql.Parameters.Clear();

                            comandoSql.Parameters.AddWithValue("@ArchivoID", id);
                            comandoSql.Parameters.AddWithValue("@CategoriaID", categoria);

                            comandoSql.ExecuteNonQuery();
                        }

                        conexionDB.Close();
                    }
                }
                else
                {
                    using (SqlCommand comandoSql = new SqlCommand())
                    {
                        comandoSql.CommandType = CommandType.Text;
                        comandoSql.CommandText =
                            "UPDATE Archivos " +
                            "SET Nombre = @Nombre, " +
                            "Archivo = @Archivo, " +
                            "Extension = @Extension, " +
                            "Observaciones = @Observaciones " +
                            $"WHERE ID = {id}";
                        comandoSql.Connection = conexionDB;

                        comandoSql.Parameters.AddWithValue("@Nombre", Nombre);
                        comandoSql.Parameters.AddWithValue("@Archivo", Archivo);
                        comandoSql.Parameters.AddWithValue("@Extension", Extension);
                        comandoSql.Parameters.AddWithValue("@Observaciones", Observaciones);

                        conexionDB.Open();

                        comandoSql.ExecuteNonQuery();

                        //DELETE Categorías
                        comandoSql.CommandType = CommandType.Text;
                        comandoSql.CommandText =
                            "DELETE FROM ArchivosCategorias " +
                            $"WHERE ArchivoID = {id}";
                        comandoSql.Connection = conexionDB;

                        comandoSql.ExecuteNonQuery();

                        foreach (short categoria in categorias)
                        {
                            comandoSql.CommandText =
                            "INSERT INTO ArchivosCategorias " +
                            "(ArchivoID,CategoriaID) " +
                            "VALUES (@ArchivoID,@CategoriaID)";

                            comandoSql.Parameters.Clear();

                            comandoSql.Parameters.AddWithValue("@ArchivoID", id);
                            comandoSql.Parameters.AddWithValue("@CategoriaID", categoria);

                            comandoSql.ExecuteNonQuery();
                        }

                        conexionDB.Close();
                    }
                }

                return "Actualizado con éxito";
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return "ERROR";
            }
        }
        #endregion

        #region ListarArchivos
        public System.Data.DataTable ListarArchivos(string pagina, string cantidadRegistros, string categoria, string busqueda)
        {
            short registrosIgnorar = 0;
            listArchivos.Clear();
            inIDsCategorias = string.Empty;
            System.Data.DataTable dt = new System.Data.DataTable();

            try
            {
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
                        comandoSql.CommandTimeout = 1000;
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
                            command.CommandTimeout = 1000;
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
                        string pages = string.Empty;

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
                            command.CommandTimeout = 1000;
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
                                DataRow[] filasEncontradas = dt.Select($"Nombre = '{row["Nombre"]}'");

                                if (filasEncontradas.Length > 0)
                                {
                                    foreach (DataRow fila in filasEncontradas)
                                    {
                                        fila.Delete();
                                    }

                                    dt.AcceptChanges();
                                }

                                DataRow dataRow = dt.NewRow();
                                dataRow["ID"] = row["ID"];
                                dataRow["Nombre"] = row["Nombre"];
                                dataRow["Observaciones"] = row["Observaciones"];
                                dataRow["Paginas"] = pages;
                                dt.Rows.Add(dataRow);
                            }
                        }

                        extension = "xlsx";

                        dataTableExtensiones.Rows.Clear();
                        pages = string.Empty;

                        query = "SELECT " +
                        "ID,Extension,Archivo,Nombre,Observaciones " +
                        "FROM Archivos " +
                        $"WHERE Extension LIKE '%.{extension}' " +
                        "ORDER BY ID";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.CommandTimeout = 1000;
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
                                DataRow[] filasEncontradas = dt.Select($"Nombre = '{row["Nombre"]}'");

                                if (filasEncontradas.Length > 0)
                                {
                                    foreach (DataRow fila in filasEncontradas)
                                    {
                                        fila.Delete();
                                    }

                                    dt.AcceptChanges();
                                }

                                DataRow dataRow = dt.NewRow();
                                dataRow["ID"] = row["ID"];
                                dataRow["Nombre"] = row["Nombre"];
                                dataRow["Observaciones"] = row["Observaciones"];
                                dataRow["Paginas"] = pages;
                                dt.Rows.Add(dataRow);
                            }
                        }

                        extension = "pptx";

                        dataTableExtensiones.Rows.Clear();
                        pages = string.Empty;

                        query = "SELECT " +
                        "ID,Extension,Archivo,Nombre,Observaciones " +
                        "FROM Archivos " +
                        $"WHERE Extension LIKE '%.{extension}' " +
                        "ORDER BY ID";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.CommandTimeout = 1000;
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
                            bool found = SearchTextInPowerPointFile(ubicacionCompleta, busqueda, out pages);

                            if (found)
                            {
                                DataRow[] filasEncontradas = dt.Select($"Nombre = '{row["Nombre"]}'");

                                if (filasEncontradas.Length > 0)
                                {
                                    foreach (DataRow fila in filasEncontradas)
                                    {
                                        fila.Delete();
                                    }

                                    dt.AcceptChanges();
                                }

                                DataRow dataRow = dt.NewRow();
                                dataRow["ID"] = row["ID"];
                                dataRow["Nombre"] = row["Nombre"];
                                dataRow["Observaciones"] = row["Observaciones"];
                                dataRow["Paginas"] = pages;
                                dt.Rows.Add(dataRow);
                            }
                        }

                        extension = "ppsx";

                        dataTableExtensiones.Rows.Clear();
                        pages = string.Empty;

                        query = "SELECT " +
                        "ID,Extension,Archivo,Nombre,Observaciones " +
                        "FROM Archivos " +
                        $"WHERE Extension LIKE '%.{extension}' " +
                        "ORDER BY ID";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.CommandTimeout = 1000;
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
                            bool found = SearchTextInPowerPointFile(ubicacionCompleta, busqueda, out pages);

                            if (found)
                            {
                                DataRow[] filasEncontradas = dt.Select($"Nombre = '{row["Nombre"]}'");

                                if (filasEncontradas.Length > 0)
                                {
                                    foreach (DataRow fila in filasEncontradas)
                                    {
                                        fila.Delete();
                                    }

                                    dt.AcceptChanges();
                                }

                                DataRow dataRow = dt.NewRow();
                                dataRow["ID"] = row["ID"];
                                dataRow["Nombre"] = row["Nombre"];
                                dataRow["Observaciones"] = row["Observaciones"];
                                dataRow["Paginas"] = pages;
                                dt.Rows.Add(dataRow);
                            }
                        }

                        extension = "pdf";

                        dataTableExtensiones.Rows.Clear();
                        pages = string.Empty;

                        query = "SELECT " +
                        "ID,Extension,Archivo,Nombre,Observaciones " +
                        "FROM Archivos " +
                        $"WHERE Extension LIKE '%.{extension}' " +
                        "ORDER BY ID";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.CommandTimeout = 1000;
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
                            bool found = SearchTextInPdf(ubicacionCompleta, busqueda, out pages);

                            if (found)
                            {
                                DataRow[] filasEncontradas = dt.Select($"Nombre = '{row["Nombre"]}'");

                                if (filasEncontradas.Length > 0)
                                {
                                    foreach (DataRow fila in filasEncontradas)
                                    {
                                        fila.Delete();
                                    }

                                    dt.AcceptChanges();
                                }

                                DataRow dataRow = dt.NewRow();
                                dataRow["ID"] = row["ID"];
                                dataRow["Nombre"] = row["Nombre"];
                                dataRow["Observaciones"] = row["Observaciones"];
                                dataRow["Paginas"] = pages;
                                dt.Rows.Add(dataRow);
                            }
                        }

                        extension = "txt";

                        dataTableExtensiones.Rows.Clear();
                        pages = string.Empty;

                        query = "SELECT " +
                        "ID,Extension,Archivo,Nombre,Observaciones " +
                        "FROM Archivos " +
                        $"WHERE Extension LIKE '%.{extension}' " +
                        "ORDER BY ID";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.CommandTimeout = 1000;
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
                            bool found = SearchTextInTXT(ubicacionCompleta, busqueda);

                            if (found)
                            {
                                DataRow[] filasEncontradas = dt.Select($"Nombre = '{row["Nombre"]}'");

                                if (filasEncontradas.Length > 0)
                                {
                                    foreach (DataRow fila in filasEncontradas)
                                    {
                                        fila.Delete();
                                    }

                                    dt.AcceptChanges();
                                }

                                DataRow dataRow = dt.NewRow();
                                dataRow["ID"] = row["ID"];
                                dataRow["Nombre"] = row["Nombre"];
                                dataRow["Observaciones"] = row["Observaciones"];
                                dataRow["Paginas"] = pages;
                                dt.Rows.Add(dataRow);
                            }
                        }

                        ///////////////////////////////////////////////////////////////////////////////////

                    }
                }

                return dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return dt;
            }
        }
        #endregion

        #region ArchivoPorId
        public System.Data.DataTable ArchivoPorId()
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            try
            {

                using (SqlCommand comandoSql = new SqlCommand())
                {
                    comandoSql.CommandTimeout = 1000;
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
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return dt;
            }
        }
        #endregion

        #region FiltroArchivos
        public List<Archivos> FiltroArchivos()
        {
            var tabla = ArchivoPorId();

            var infoArchivo = new List<Archivos>();

            try
            {

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
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return infoArchivo;
            }
        }
        #endregion

        #region CantidadTotalArchivos
        public long CantidadTotalArchivos(string categoria, string busqueda)
        {
            int total = 0;

            try
            {

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
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return total;
            }
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
                    MessageBox.Show($"El archivo no existe: {filePath}",
                        ElementosGlobales.NombreSistema,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
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
                MessageBox.Show($"Error en el formato del archivo: {ex.Message}",
                        ElementosGlobales.NombreSistema,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                return false;
            }
            catch (IOException ex)
            {
                // Handle IO exceptions
                MessageBox.Show($"Error de I/O - Entrada/Salida: {ex.Message}",
                        ElementosGlobales.NombreSistema,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                return false;
            }
            catch (UnauthorizedAccessException ex)
            {
                // Handle unauthorized access exceptions
                MessageBox.Show($"Error de acceso: {ex.Message}",
                        ElementosGlobales.NombreSistema,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                return false;
            }
            catch (Exception ex)
            {
                // Handle all other exceptions
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                    MessageBox.Show($"El archivo no existe: {filePath}",
                        ElementosGlobales.NombreSistema,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
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
                            // Store the sheet name along with the cell address
                            foundCellAddresses.Add($"{worksheet.Name}!{cell.Address}");
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
                MessageBox.Show($"Error en el formato del archivo: {ex.Message}", ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (IOException ex)
            {
                // Handle IO exceptions
                MessageBox.Show($"Error de I/O - Entrada/Salida: {ex.Message}", ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (UnauthorizedAccessException ex)
            {
                // Handle unauthorized access exceptions
                MessageBox.Show($"Error de acceso: {ex.Message}", ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                // Handle all other exceptions
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        #region SearchTextInPowerPointFile
        public bool SearchTextInPowerPointFile(string filePath, string searchText, out string foundSlides)
        {
            PowerPoint.Application pptApp = null;
            PowerPoint.Presentation pptPresentation = null;
            foundSlides = string.Empty;

            try
            {
                // Check if the file exists
                if (!File.Exists(filePath))
                {
                    MessageBox.Show($"El archivo no existe: {filePath}", ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // Initialize PowerPoint application
                pptApp = new PowerPoint.Application();
                pptPresentation = pptApp.Presentations.Open(filePath, WithWindow: Office.MsoTriState.msoFalse);

                bool textFound = false;
                var foundSlideIndexes = new List<string>();

                // Loop through each slide in the presentation
                foreach (PowerPoint.Slide slide in pptPresentation.Slides)
                {
                    // Loop through each shape in the slide
                    foreach (PowerPoint.Shape shape in slide.Shapes)
                    {
                        if (shape.HasTextFrame == Office.MsoTriState.msoTrue && shape.TextFrame.HasText == Office.MsoTriState.msoTrue)
                        {
                            PowerPoint.TextRange textRange = shape.TextFrame.TextRange;

                            if (textRange.Text.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                            {
                                textFound = true;
                                foundSlideIndexes.Add($"Slide {slide.SlideIndex}");
                            }
                        }
                    }
                }

                // Display the results
                if (textFound)
                {
                    foundSlides = string.Join(", ", foundSlideIndexes.Distinct());
                    //MessageBox.Show($"The text '{searchText}' was found in the following slides: {foundSlides}");
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
                MessageBox.Show($"Error en el formato del archivo: {ex.Message}", ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (IOException ex)
            {
                // Handle IO exceptions
                MessageBox.Show($"Error de I/O - Entrada/Salida: {ex.Message}", ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (UnauthorizedAccessException ex)
            {
                // Handle unauthorized access exceptions
                MessageBox.Show($"Error de acceso: {ex.Message}", ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                // Handle all other exceptions
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                // Ensure the presentation and application are properly closed
                if (pptPresentation != null)
                {
                    pptPresentation.Close();
                }
                if (pptApp != null)
                {
                    pptApp.Quit();
                }
            }
        }
        #endregion

        #region SearchTextInPdf
        public bool SearchTextInPdf(string filePath, string searchText, out string pagesWithText)
        {

            bool textFound = false;
            pagesWithText = string.Empty;
            var foundPages = new List<string>();
            try
            {
                // Check if the file exists
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"El archivo no existe: {filePath}");
                }

                // Load the PDF document
                using (PdfReader pdfReader = new PdfReader(filePath))
                using (PdfDocument pdfDocument = new PdfDocument(pdfReader))
                {
                    // Iterate through each page
                    for (int pageNum = 1; pageNum <= pdfDocument.GetNumberOfPages(); pageNum++)
                    {
                        // Extract text from the current page
                        PdfPage page = pdfDocument.GetPage(pageNum);
                        ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                        string pageText = PdfTextExtractor.GetTextFromPage(page, strategy);

                        // Check if the text is found on the current page
                        if (pageText.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                        {
                            textFound = true;
                            foundPages.Add(pageNum.ToString());
                        }
                    }
                }

                // Display the results
                if (textFound)
                {
                    pagesWithText = string.Join(", ", foundPages.Distinct());
                    //MessageBox.Show($"The text '{searchText}' was found in the following slides: {foundSlides}");
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
                MessageBox.Show($"Error en el formato del archivo: {ex.Message}", ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (IOException ex)
            {
                // Handle IO exceptions
                MessageBox.Show($"Error de I/O - Entrada/Salida: {ex.Message}", ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (UnauthorizedAccessException ex)
            {
                // Handle unauthorized access exceptions
                MessageBox.Show($"Error de acceso: {ex.Message}", ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                // Handle all other exceptions
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
            }
        }
        #endregion

        #region SearchTextInTXT
        public bool SearchTextInTXT(string filePath, string searchText)
        {
            try
            {
                // Check if the file exists
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"El archivo no existe: {filePath}");
                }

                // Read all lines from the file
                string[] lines = File.ReadAllLines(filePath);

                // Search for the specified text in each line
                foreach (string line in lines)
                {
                    if (line.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    {
                        return true; // Search text found
                    }
                }

                return false; // Search text not found
            }
            catch (FileFormatException ex)
            {
                // Handle specific file format exceptions
                MessageBox.Show($"Error en el formato del archivo: {ex.Message}", ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (IOException ex)
            {
                // Handle IO exceptions
                MessageBox.Show($"Error de I/O - Entrada/Salida: {ex.Message}", ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (UnauthorizedAccessException ex)
            {
                // Handle unauthorized access exceptions
                MessageBox.Show($"Error de acceso: {ex.Message}", ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                // Handle all other exceptions
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
            }
        }
        #endregion

        #region EliminarDocumento
        public string EliminarDocumento(int id)
        {
            try
            {

                conexionDB.Open();

                using (SqlCommand comandoSql = new SqlCommand())
                {
                    comandoSql.CommandType = CommandType.Text;
                    comandoSql.CommandText =
                        "DELETE FROM Archivos " +
                        $"WHERE ID = {id}";
                    comandoSql.Connection = conexionDB;

                    comandoSql.ExecuteNonQuery();
                }

                using (SqlCommand comandoSql = new SqlCommand())
                {
                    comandoSql.CommandType = CommandType.Text;
                    comandoSql.CommandText =
                        "DELETE FROM ArchivosCategorias " +
                        $"WHERE ArchivoID = {id}";
                    comandoSql.Connection = conexionDB;

                    comandoSql.ExecuteNonQuery();
                }

                conexionDB.Close();

                return "Archivo eliminado con éxito";

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return "ERROR";
            }
        }
        #endregion
    }
}
