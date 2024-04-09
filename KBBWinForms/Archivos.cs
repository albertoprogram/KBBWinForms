﻿using System;
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
        public DataTable ListarArchivos(string pagina, string cantidadRegistros, string categoria)
        {
            short registrosIgnorar = 0;
            listArchivos.Clear();
            inIDsCategorias = string.Empty;

            if (!string.IsNullOrWhiteSpace(pagina))
            {
                registrosIgnorar = short.Parse(pagina);
                registrosIgnorar -= 1;
                registrosIgnorar *= short.Parse(cantidadRegistros);
            }

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

                DataTable dt = new DataTable();

                if (listArchivos.Count > 0)
                {
                    comandoSql.CommandText =
                    "SELECT ID,Nombre " +
                    "FROM Archivos " +
                    $"WHERE ID IN ({inIDsCategorias}) " +
                    "ORDER BY ID " +
                    "OFFSET " + registrosIgnorar.ToString() + " ROWS " +
                    "FETCH NEXT " + cantidadRegistros + " ROWS ONLY";

                    reader.Close();
                    reader.Dispose();

                    reader = comandoSql.ExecuteReader();

                    if (reader.HasRows) dt.Load(reader);
                }

                reader.Close();
                reader.Dispose();
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

            if (listArchivos.Count > 0)
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

                    return total;
                }
            }
            else
            {
                return total;
            }
        }
        #endregion
    }
}
