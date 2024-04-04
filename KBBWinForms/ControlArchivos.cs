using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KBBWinForms
{
    public partial class ControlArchivos : Form
    {
        #region Variables
        Archivos archivo = new Archivos();
        string categoria;

        SqlConnection conexionDB = new SqlConnection(ConexionDB.cadenaConexionSQLServer);
        #endregion

        #region Constructores
        public ControlArchivos()
        {
            InitializeComponent();
            LlenarData();
        }
        #endregion

        #region btnAgregar_Click
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            SubirArchivo frmSubirArchivo = new SubirArchivo();
            frmSubirArchivo.ShowDialog();
            LlenarData();
        }
        #endregion

        #region LlenarData
        private void LlenarData()
        {
            ControlBotonesPaginado();

            if (cmbCantidadRegistrosXPagina.Text.Length == 0)
                cmbCantidadRegistrosXPagina.Text = "25";

            dgvDocumentos.DataSource = archivo.ListarArchivos(txtPagina.Text, cmbCantidadRegistrosXPagina.Text, categoria);

            txtTotalRegistros.Text = archivo.CantidadTotalArchivos().ToString();

            if ((short.Parse(txtTotalRegistros.Text) % short.Parse(cmbCantidadRegistrosXPagina.Text)) == 0)
            {
                txtTotalPaginas.Text = (short.Parse(txtTotalRegistros.Text) / short.Parse(cmbCantidadRegistrosXPagina.Text)).ToString();
            }
            else
            {
                txtTotalPaginas.Text = ((short.Parse(txtTotalRegistros.Text) / short.Parse(cmbCantidadRegistrosXPagina.Text)) + 1).ToString();
            }
        }
        #endregion

        #region btnVer_Click
        private void btnVer_Click(object sender, EventArgs e)
        {
            if (dgvDocumentos.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvDocumentos.CurrentRow.Cells[0].Value);
                archivo.Id = id;

                var lista = new List<Archivos>();
                lista = archivo.FiltroArchivos();

                foreach (Archivos archivo in lista)
                {
                    string ruta = AppDomain.CurrentDomain.BaseDirectory;

                    string carpetaTemporal = ruta + @"temp\";

                    string ubicacionCompleta = carpetaTemporal + archivo.Extension;

                    if (!Directory.Exists(carpetaTemporal))
                        Directory.CreateDirectory(carpetaTemporal);

                    if (File.Exists(ubicacionCompleta))
                        File.Delete(ubicacionCompleta);

                    File.WriteAllBytes(ubicacionCompleta, archivo.Archivo);

                    var processStartInfo = new ProcessStartInfo(ubicacionCompleta)
                    {
                        UseShellExecute = true
                    };

                    Process.Start(processStartInfo);
                }
            }
        }
        #endregion

        #region btnSiguiente_Click
        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (txtPagina.Text.Length > 0)
            {
                short pagina = short.Parse(txtPagina.Text);
                pagina += 1;

                txtPagina.Text = pagina.ToString();

                dgvDocumentos.DataSource = archivo.ListarArchivos(pagina.ToString(), cmbCantidadRegistrosXPagina.Text, categoria);

                ControlBotonesPaginado();
            }
        }
        #endregion

        #region btnAnterior_Click
        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (txtPagina.Text.Length > 0)
            {
                short pagina = short.Parse(txtPagina.Text);
                pagina -= 1;

                txtPagina.Text = pagina.ToString();

                dgvDocumentos.DataSource = archivo.ListarArchivos(pagina.ToString(), cmbCantidadRegistrosXPagina.Text, categoria);

                ControlBotonesPaginado();
            }
        }
        #endregion

        #region ControlBotonesPaginado
        private void ControlBotonesPaginado()
        {
            if (txtPagina.Text == "1")
            {
                btnAnterior.Enabled = false;
                btnInicio.Enabled = false;
                btnSiguiente.Enabled = true;
                btnFin.Enabled = true;
            }

            if (txtPagina.Text != "1")
            {
                btnAnterior.Enabled = true;
                btnInicio.Enabled = true;
                btnSiguiente.Enabled = true;
                btnFin.Enabled = true;
            }

            if (txtPagina.Text == txtTotalPaginas.Text)
            {
                btnAnterior.Enabled = true;
                btnInicio.Enabled = true;
                btnSiguiente.Enabled = false;
                btnFin.Enabled = false;
            }

            if (txtPagina.Text == "1" && txtTotalPaginas.Text == "1")
            {
                btnAnterior.Enabled = false;
                btnInicio.Enabled = false;
                btnSiguiente.Enabled = false;
                btnFin.Enabled = false;
            }
        }
        #endregion

        #region cmbCantidadRegistrosXPagina_SelectedIndexChanged
        private void cmbCantidadRegistrosXPagina_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPagina.Text = "1";

            LlenarData();

            ControlBotonesPaginado();
        }
        #endregion

        #region txtPagina_TextChanged
        private void txtPagina_TextChanged(object sender, EventArgs e)
        {

            if (txtPagina.Text.Length > 0 && short.Parse(txtPagina.Text) > short.Parse(txtTotalPaginas.Text) ||
                txtPagina.Text.Length > 0 && txtPagina.Text == "0")
            {
                txtPagina.Text = "1";
                return;
            }

            if (txtPagina.Text.Length > 0)
            {
                LlenarData();

                ControlBotonesPaginado();
            }
            else
            {
                btnSiguiente.Enabled = false;
                btnAnterior.Enabled = false;
            }

            txtPagina.Select(0, txtPagina.Text.Length);
            txtPagina.Focus();
        }
        #endregion

        #region btnInicio_Click
        private void btnInicio_Click(object sender, EventArgs e)
        {
            txtPagina.Text = "1";

            LlenarData();

            ControlBotonesPaginado();
        }
        #endregion

        #region btnFin_Click
        private void btnFin_Click(object sender, EventArgs e)
        {
            txtPagina.Text = txtTotalPaginas.Text;

            LlenarData();

            ControlBotonesPaginado();
        }
        #endregion

        #region txtPagina_KeyPress
        private void txtPagina_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar == 22)
            {
                e.Handled = true;
            }
        }
        #endregion

        #region ControlArchivos_Load
        private void ControlArchivos_Load(object sender, EventArgs e)
        {
            var blankContextMenu = new ContextMenuStrip();
            txtPagina.ContextMenuStrip = blankContextMenu;

            ListarCategorias();
        }
        #endregion

        #region txtPagina_Click
        private void txtPagina_Click(object sender, EventArgs e)
        {
            txtPagina.Select(0, txtPagina.Text.Length);
        }
        #endregion

        #region ListarCategorias
        private void ListarCategorias()
        {
            tvCategorias.Nodes.Clear();

            string query = "SELECT Categoria FROM Categorias ORDER BY Categoria";

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
                                tvCategorias.Nodes.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region tvCategorias_AfterSelect
        private void tvCategorias_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvCategorias.SelectedNode != null)
            {
                categoria = tvCategorias.SelectedNode.Text;

                LlenarData();
            }
        }
        #endregion
    }
}
