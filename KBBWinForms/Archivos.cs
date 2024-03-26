using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string AgregarDocumento()
        {
            using (SqlCommand comandoSql = new SqlCommand())
            {
                comandoSql.CommandType = CommandType.Text;
                comandoSql.CommandText =
                    "INSERT INTO Archivos " +
                    "(Nombre,Archivo,Extension,Observaciones) " +
                    "VALUES (@Nombre,@Archivo,@Extension,@Observaciones)";
                comandoSql.Connection = conexionDB;

                comandoSql.Parameters.AddWithValue("@Nombre", Nombre);
                comandoSql.Parameters.AddWithValue("@Archivo", Archivo);
                comandoSql.Parameters.AddWithValue("@Extension", Extension);
                comandoSql.Parameters.AddWithValue("@Observaciones", Observaciones);

                conexionDB.Open();

                comandoSql.ExecuteNonQuery();

                conexionDB.Close();
            }

            return "Agregado con éxito";
        }
        #endregion

        #region ListarArchivos
        public DataTable ListarArchivos(string pagina, string cantidadRegistros)
        {
            short registrosIgnorar = 0;

            if (!string.IsNullOrWhiteSpace(pagina))
            {
                registrosIgnorar = short.Parse(pagina);
                registrosIgnorar -= 1;
                registrosIgnorar *= short.Parse(cantidadRegistros);
            }

            DataTable dt = new DataTable();

            using (SqlCommand comandoSql = new SqlCommand())
            {
                comandoSql.CommandType = CommandType.Text;
                comandoSql.CommandText =
                    "SELECT ID,Nombre " +
                    "FROM Archivos " +
                    "ORDER BY ID " +
                    "OFFSET " + registrosIgnorar.ToString() + " ROWS " +
                    "FETCH NEXT " + cantidadRegistros + " ROWS ONLY";
                comandoSql.Connection = conexionDB;

                conexionDB.Open();

                SqlDataReader reader = comandoSql.ExecuteReader();

                if (reader.HasRows) dt.Load(reader);

                reader.Close();
                conexionDB.Close();

                return dt;
            }
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
        public long CantidadTotalArchivos()
        {
            int total = 0;

            using (SqlCommand comandoSql = new SqlCommand())
            {
                comandoSql.CommandType = CommandType.Text;
                comandoSql.CommandText = "SELECT COUNT(*) FROM Archivos";
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

                return total;
            }
        }
        #endregion
    }
}
