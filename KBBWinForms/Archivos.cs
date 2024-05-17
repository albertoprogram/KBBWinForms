using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public DataTable ListarArchivos(string pagina, string cantidadRegistros, string categoria, string busqueda)
        {
            short registrosIgnorar = 0;
            listArchivos.Clear();
            inIDsCategorias = string.Empty;
            DataTable dt = new DataTable();

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

                    DataTable dataTableExtensiones = new DataTable("Extensiones");
                    string extension = string.Empty;

                    dataTableExtensiones.Columns.Add("ID", typeof(int));
                    dataTableExtensiones.Columns.Add("Extension", typeof(string));
                    dataTableExtensiones.Columns.Add("Archivo", typeof(byte[]));

                    extension = "doc";

                    query = "SELECT " +
                    "ID,Extension,Archivo " +
                    "FROM Archivos " +
                    $"WHERE Extension LIKE '%.{extension}'";

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
                                    dataTableExtensiones.Rows.Add(dataRow);
                                }
                            }
                        }
                    }

                    string ruta = AppDomain.CurrentDomain.BaseDirectory;

                    string carpetaTemporal = ruta + @"temp\";

                    if (!Directory.Exists(carpetaTemporal))
                        Directory.CreateDirectory(carpetaTemporal);

                    foreach (DataRow row in dataTableExtensiones.Rows)
                    {
                        string ubicacionCompleta = carpetaTemporal + row["Extension"];

                        if (File.Exists(ubicacionCompleta))
                            File.Delete(ubicacionCompleta);

                        File.WriteAllBytes(ubicacionCompleta, (byte[])row["Archivo"]);
                    }

                    //Abrir el documento, leerlo y ver si hay alguna expresión según la búsqueda que se escribió

                    extension = "docx";

                    query = "SELECT " +
                    "ID,Extension,Archivo " +
                    "FROM Archivos " +
                    $"WHERE Extension LIKE '%.{extension}'";

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

                    extension = "xls";

                    query = "SELECT " +
                    "ID,Extension,Archivo " +
                    "FROM Archivos " +
                    $"WHERE Extension LIKE '%.{extension}'";

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

                    extension = "xlsx";

                    query = "SELECT " +
                    "ID,Extension,Archivo " +
                    "FROM Archivos " +
                    $"WHERE Extension LIKE '%.{extension}'";

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

                    extension = "ppt";

                    query = "SELECT " +
                    "ID,Extension,Archivo " +
                    "FROM Archivos " +
                    $"WHERE Extension LIKE '%.{extension}'";

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
                    $"WHERE Extension LIKE '%.{extension}'";

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
                    $"WHERE Extension LIKE '%.{extension}'";

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
                    $"WHERE Extension LIKE '%.{extension}'";

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
                    $"WHERE Extension LIKE '%.{extension}'";

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
                    $"WHERE Extension LIKE '%.{extension}'";

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

                    //Obtener el Archivo, es decir, descargarlo y leerlo
                    //Descarga del archivo: Ver ArchivoPorId() y FiltroArchivos() y en ControlArchivo.cs btnVer_Click
                    //foreach (int idArchivo in listArchivos)
                    //{
                    //    string ruta = AppDomain.CurrentDomain.BaseDirectory;

                    //    string carpetaTemporal = ruta + @"temp\";

                    //    string ubicacionCompleta = carpetaTemporal + archivo.Extension;

                    //    if (!Directory.Exists(carpetaTemporal))
                    //        Directory.CreateDirectory(carpetaTemporal);

                    //    if (File.Exists(ubicacionCompleta))
                    //        File.Delete(ubicacionCompleta);

                    //    File.WriteAllBytes(ubicacionCompleta, archivo.Archivo);
                    //}
                    ///////////////////////////////////////////////////////////////////////////////////

                }
            }

            return dt;

        }
        #endregion

        #region ArchivoPorId
        public DataTable ArchivoPorId()
        {
            DataTable dt = new DataTable();

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
    }
}
