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

        #region Propiedades
        public IContract Contrato { get; set; }
        #endregion

        #region Constructores
        public Categorias()
        {
            InitializeComponent();

            // Colores
            //----------------------------------------------------------------
            string hexColor = "#E7ECEF";

            Color color = ColorTranslator.FromHtml(hexColor);

            this.BackColor = color;
            btnGuardarCategoria.ForeColor = color;
            btnEnviar.ForeColor = color;

            hexColor = "#274C77";

            color = ColorTranslator.FromHtml(hexColor);

            this.ForeColor = color;

            txtCategoria.ForeColor = color;
            txtFiltroCategorias.ForeColor = color;

            hexColor = "#A3CEF1";

            color = ColorTranslator.FromHtml(hexColor);

            hexColor = "#6096BA";

            color = ColorTranslator.FromHtml(hexColor);

            btnGuardarCategoria.BackColor = color;
            btnEnviar.BackColor = color;
            //----------------------------------------------------------------
        }
        #endregion

        #region btnGuardarCategoria_Click
        private void btnGuardarCategoria_Click(object sender, EventArgs e)
        {
            try
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

                CargarCategorias();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Categorias_Load
        private void Categorias_Load(object sender, EventArgs e)
        {
            CargarCategorias();
        }
        #endregion

        #region CargarCategorias
        private void CargarCategorias()
        {
            try
            {

                dgvCategorias.Rows.Clear();

                string query = "SELECT ID, Categoria FROM Categorias ORDER BY Categoria";

                using (SqlConnection connection = new SqlConnection(ConexionDB.cadenaConexionSQLServer))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    dgvCategorias.Rows.Add(false, reader[0].ToString(), reader[1].ToString());
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region CargarCategoriasConFiltro
        private void CargarCategoriasConFiltro()
        {
            try
            {

                dgvCategorias.Rows.Clear();

                string query = "SELECT ID, Categoria FROM Categorias " +
                    "WHERE Categoria LIKE '%" + txtFiltroCategorias.Text + "%' " +
                    "ORDER BY Categoria";

                using (SqlConnection connection = new SqlConnection(ConexionDB.cadenaConexionSQLServer))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    dgvCategorias.Rows.Add(false, reader[0].ToString(), reader[1].ToString());
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region btnEnviar_Click
        private void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {

                List<(int, string)> tuplas = new List<(int, string)>();
                bool controlCategorias = false;

                foreach (DataGridViewRow row in dgvCategorias.Rows)
                {
                    DataGridViewCheckBoxCell cell = row.Cells["Seleccion"] as DataGridViewCheckBoxCell;

                    if (cell != null && (bool)cell.Value == true)
                    {
                        controlCategorias = true;
                        tuplas.Add((Convert.ToInt32(row.Cells["IdCategoria"].Value), row.Cells["Categoria"].Value.ToString()));
                    }
                }

                if (controlCategorias == true)
                {
                    Contrato.Compartir(tuplas);

                    Close();
                }
                else
                {
                    MessageBox.Show("Debe seleccionar al menos una categoría", Configuraciones.nombreSistema,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region txtFiltroCategorias_TextChanged
        private void txtFiltroCategorias_TextChanged(object sender, EventArgs e)
        {
            CargarCategoriasConFiltro();
        }
        #endregion
    }
}