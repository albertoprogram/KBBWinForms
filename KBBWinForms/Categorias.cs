using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KBBWinForms
{
    public partial class Categorias : Form
    {
        #region Variables
        SqlConnection conexionDB = new SqlConnection(ConexionDB.cadenaConexionSQLServer);
        #endregion

        #region Constructores
        public Categorias()
        {
            InitializeComponent();
        }
        #endregion

        #region btnGuardarCategoria_Click
        private void btnGuardarCategoria_Click(object sender, EventArgs e)
        {
            using (SqlCommand comandoSql = new SqlCommand())
            {
                comandoSql.CommandType = CommandType.Text;
                comandoSql.CommandText =
                    "INSERT INTO Categorias " +
                    "(Categoria) " +
                    "VALUES ('" + txtCategoria.Text.Trim() + "')";
                comandoSql.Connection = conexionDB;

                conexionDB.Open();

                comandoSql.ExecuteNonQuery();

                conexionDB.Close();
            }
        }
        #endregion
    }
}
