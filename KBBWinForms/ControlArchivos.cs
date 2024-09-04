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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace KBBWinForms
{
    public partial class ControlArchivos : Form
    {
        #region Variables
        Archivos archivo = new Archivos();
        string categoria;
        string busqueda;
        private Panel overlayPanel;
        private Contenedor mdiParentForm;

        SqlConnection conexionDB = new SqlConnection(ConexionDB.cadenaConexionSQLServer);
        #endregion

        #region Constructores
        public ControlArchivos(Contenedor parent)
        {
            InitializeComponent();

            this.mdiParentForm = parent;

            //Colores
            //----------------------------------------------------------------
            string hexColor = "#E7ECEF";

            Color color = ColorTranslator.FromHtml(hexColor);

            this.BackColor = color;
            tvCategorias.BackColor = color;
            dgvDocumentos.BackgroundColor = color;
            btnBuscar.ForeColor = color;
            btnAgregar.ForeColor = color;
            btnEditar.ForeColor = color;
            btnVer.ForeColor = color;
            btnEliminar.ForeColor = color;

            hexColor = "#274C77";

            color = ColorTranslator.FromHtml(hexColor);

            this.ForeColor = color;

            txtBusqueda.ForeColor = color;

            hexColor = "#A3CEF1";

            color = ColorTranslator.FromHtml(hexColor);

            lblStatus.ForeColor = color;
            lblStatus.BackColor = color;

            hexColor = "#6096BA";

            color = ColorTranslator.FromHtml(hexColor);

            btnBuscar.BackColor = color;
            btnAgregar.BackColor = color;
            btnEditar.BackColor = color;
            btnVer.BackColor = color;
            btnEliminar.BackColor = color;
            //----------------------------------------------------------------

            LlenarData();
        }
        #endregion

        #region btnAgregar_Click
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            SubirArchivo frmSubirArchivo = new SubirArchivo(0);
            frmSubirArchivo.ShowDialog();
            ListarCategorias();
            LlenarData();
        }
        #endregion

        #region LlenarData
        private void LlenarData()
        {
            try
            {

                if (cmbCantidadRegistrosXPagina.Text.Length == 0)
                    cmbCantidadRegistrosXPagina.Text = "25";

                dgvDocumentos.Rows.Clear();

                string pagina = txtPagina.Text;
                string cantidadRegistros = cmbCantidadRegistrosXPagina.Text;

                DataTable dt = new DataTable();
                dt = archivo.ListarArchivos(pagina, cantidadRegistros, categoria, busqueda);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        dgvDocumentos.Rows.Add(dr["ID"].ToString(), dr["Nombre"].ToString(),
                            dr["Observaciones"].ToString(), dr["Paginas"].ToString());
                    }
                }

                txtTotalRegistros.Text = archivo.CantidadTotalArchivos(categoria, busqueda).ToString();

                if ((short.Parse(txtTotalRegistros.Text) % short.Parse(cmbCantidadRegistrosXPagina.Text)) == 0)
                {
                    txtTotalPaginas.Text = (short.Parse(txtTotalRegistros.Text) / short.Parse(cmbCantidadRegistrosXPagina.Text)).ToString();
                }
                else
                {
                    txtTotalPaginas.Text = ((short.Parse(txtTotalRegistros.Text) / short.Parse(cmbCantidadRegistrosXPagina.Text)) + 1).ToString();
                }

                ControlBotonesPaginado();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region btnVer_Click
        private void btnVer_Click(object sender, EventArgs e)
        {
            try
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
                else
                {
                    MessageBox.Show("No hay algún archivo seleccionado",
                        ElementosGlobales.NombreSistema,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region btnSiguiente_Click
        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtPagina.Text.Length > 0)
                {
                    short pagina = short.Parse(txtPagina.Text);
                    pagina += 1;

                    txtPagina.Text = pagina.ToString();

                    LlenarData();

                    ControlBotonesPaginado();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region btnAnterior_Click
        private void btnAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPagina.Text.Length > 0)
                {
                    short pagina = short.Parse(txtPagina.Text);
                    pagina -= 1;

                    txtPagina.Text = pagina.ToString();

                    LlenarData();

                    ControlBotonesPaginado();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region ControlBotonesPaginado
        private void ControlBotonesPaginado()
        {
            try
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

                if (txtTotalPaginas.Text == "0")
                {
                    btnAnterior.Enabled = false;
                    btnInicio.Enabled = false;
                    btnSiguiente.Enabled = false;
                    btnFin.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region cmbCantidadRegistrosXPagina_SelectedIndexChanged
        private void cmbCantidadRegistrosXPagina_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                txtPagina.Text = "1";

                LlenarData();

                ControlBotonesPaginado();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region txtPagina_TextChanged
        private void txtPagina_TextChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region btnInicio_Click
        private void btnInicio_Click(object sender, EventArgs e)
        {
            try
            {

                txtPagina.Text = "1";

                LlenarData();

                ControlBotonesPaginado();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region btnFin_Click
        private void btnFin_Click(object sender, EventArgs e)
        {
            try
            {

                txtPagina.Text = txtTotalPaginas.Text;

                LlenarData();

                ControlBotonesPaginado();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region txtPagina_KeyPress
        private void txtPagina_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region ControlArchivos_Load
        private void ControlArchivos_Load(object sender, EventArgs e)
        {
            try
            {

                var blankContextMenu = new ContextMenuStrip();
                txtPagina.ContextMenuStrip = blankContextMenu;

                ListarCategorias();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region txtPagina_Click
        private void txtPagina_Click(object sender, EventArgs e)
        {
            try
            {

                txtPagina.Select(0, txtPagina.Text.Length);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region ListarCategorias
        private void ListarCategorias()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region btnBuscar_Click
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtBusqueda.Text.Trim().Length > 0)
                {
                    categoria = string.Empty;
                    busqueda = txtBusqueda.Text.Trim();

                    lblStatus.ForeColor = Color.White;
                    lblStatus.Text = "                                          Buscando...                                          ";
                    lblStatus.Visible = true;
                    this.Enabled = false;

                    //LockForm();

                    LlenarData();

                    lblStatus.Visible = false;
                    this.Enabled = true;

                    //UnlockForm();
                }
                else
                {
                    MessageBox.Show("Ingrese una palabra o frase a buscar en el cuadro de texto",
                        ElementosGlobales.NombreSistema,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);

                    txtBusqueda.Focus();
                    txtBusqueda.SelectAll();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region tvCategorias_AfterSelect
        private void tvCategorias_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {

                if (tvCategorias.SelectedNode != null)
                {
                    txtBusqueda.Text = string.Empty;
                    busqueda = string.Empty;
                    categoria = tvCategorias.SelectedNode.Text;

                    LlenarData();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region LockForm
        private void LockForm()
        {
            try
            {

                if (overlayPanel == null)
                {
                    // Create and configure the overlay panel
                    overlayPanel = new Panel
                    {
                        BackColor = Color.FromArgb(128, 0, 0, 0), // Adjust the alpha value for transparency
                        Size = this.ClientSize,
                        Location = this.Location,
                        Visible = true
                    };

                    // Add the overlay panel to the MDI parent form
                    this.mdiParentForm.Controls.Add(overlayPanel);
                    overlayPanel.BringToFront();

                    //Attach event handlers to keep overlay panel in sync with main form
                    this.LocationChanged += MainForm_LocationChanged;
                    this.SizeChanged += MainForm_SizeChanged;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region UnlockForm
        private void UnlockForm()
        {
            try
            {

                if (overlayPanel != null)
                {
                    // Remove the overlay panel from the MDI parent form
                    this.mdiParentForm.Controls.Remove(overlayPanel);
                    overlayPanel.Dispose();
                    overlayPanel = null;

                    // Detach event handlers
                    this.LocationChanged -= MainForm_LocationChanged;
                    this.SizeChanged -= MainForm_SizeChanged;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region MainForm_LocationChanged
        private void MainForm_LocationChanged(object sender, EventArgs e)
        {
            try
            {

                if (overlayPanel != null)
                {
                    UpdateOverlayForm();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region MainForm_SizeChanged
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            try
            {

                if (overlayPanel != null)
                {
                    UpdateOverlayForm();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region UpdateOverlayForm
        private void UpdateOverlayForm()
        {
            try
            {

                if (overlayPanel != null)
                {
                    overlayPanel.Size = this.ClientSize;
                    overlayPanel.Location = this.Location;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region btnEditar_Click
        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {

                if (dgvDocumentos.SelectedRows.Count > 0)
                {
                    int id = Convert.ToInt32(dgvDocumentos.CurrentRow.Cells[0].Value);

                    SubirArchivo frmSubirArchivo = new SubirArchivo(id);
                    frmSubirArchivo.ShowDialog();
                    LlenarData();
                }
                else
                {
                    MessageBox.Show("No hay algún archivo seleccionado",
                        ElementosGlobales.NombreSistema,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region btnEliminar_Click
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {

                if (dgvDocumentos.SelectedRows.Count > 0)
                {
                    int id = Convert.ToInt32(dgvDocumentos.CurrentRow.Cells[0].Value);

                    if (MessageBox.Show($"Está seguro de eliminar el archivo de ID: {id}", ElementosGlobales.NombreSistema, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        MessageBox.Show(archivo.EliminarDocumento(id), ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LlenarData();
                    }
                }
                else
                {
                    MessageBox.Show("No hay algún archivo seleccionado",
                        ElementosGlobales.NombreSistema,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}