using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KBBWinForms
{
    public partial class SubirArchivo : Form, IContract
    {
        #region Variables
        Archivos archivo = new Archivos();
        string? rutaSeleccionada = string.Empty;
        string? filtroSeleccionado = string.Empty;
        int idArchivo = 0;
        bool archivoSeleccionado = false;
        #endregion

        #region Constructores
        public SubirArchivo(int id)
        {
            InitializeComponent();

            // Colores
            //----------------------------------------------------------------
            string hexColor = "#E7ECEF";

            Color color = ColorTranslator.FromHtml(hexColor);

            this.BackColor = color;
            btnExaminar.ForeColor = color;
            btnCategorias.ForeColor = color;
            btnCancelar.ForeColor = color;
            btnGuardar.ForeColor = color;

            hexColor = "#274C77";

            color = ColorTranslator.FromHtml(hexColor);

            this.ForeColor = color;

            txtRutaArchivo.ForeColor = color;
            txtTituloArchivo.ForeColor = color;
            lbCategorias.ForeColor = color;
            txtObservaciones.ForeColor = color;

            hexColor = "#A3CEF1";

            color = ColorTranslator.FromHtml(hexColor);

            hexColor = "#6096BA";

            color = ColorTranslator.FromHtml(hexColor);

            btnExaminar.BackColor = color;
            btnCategorias.BackColor = color;
            btnCancelar.BackColor = color;
            btnGuardar.BackColor = color;
            //----------------------------------------------------------------

            if (id > 0)
            {
                idArchivo = id;

                CargarDatosArchivo();
            }
        }
        #endregion

        #region btnExaminar_Click

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(rutaSeleccionada))
                {
                    openFileDialog1.InitialDirectory =
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                }
                else
                {
                    openFileDialog1.InitialDirectory = rutaSeleccionada;
                }

                if (string.IsNullOrEmpty(filtroSeleccionado))
                {
                    openFileDialog1.Filter =
                    "Archivos de Word|*.docx|" +
                    "Archivos de Excel|*.xlsx|" +
                    "Archivos de Power Point|*.pptx;*.ppsx|" +
                    "Archivos PDF|*.pdf|" +
                    "Archivos de imagen|*.jpg;*.jpeg;*.png|" +
                    "Archivos de texto|*.txt|" +
                    "Archivos de audio|*.mp3|" +
                    "Archivos de vídeo|*.mp4;*.wmv|" +
                    "Todos los Archivos|*.*";

                    openFileDialog1.FilterIndex = 1;
                }
                else
                {
                    openFileDialog1.Filter = filtroSeleccionado;
                }

                openFileDialog1.FileName = string.Empty;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    rutaSeleccionada = Path.GetDirectoryName(openFileDialog1.FileName);
                    filtroSeleccionado = openFileDialog1.Filter;
                    txtRutaArchivo.Text = openFileDialog1.FileName;
                    txtTituloArchivo.Text = openFileDialog1.SafeFileName;
                    archivoSeleccionado = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region btnGuardar_Click
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {

                if (archivoSeleccionado)
                {
                    byte[] data = null;

                    Stream stream = openFileDialog1.OpenFile();

                    MemoryStream memoryStream = new MemoryStream();

                    stream.CopyTo(memoryStream);

                    data = memoryStream.ToArray();

                    archivo.Nombre = txtTituloArchivo.Text;
                    archivo.Archivo = data;
                    archivo.Extension = openFileDialog1.SafeFileName;
                }

                archivo.Observaciones = txtObservaciones.Text;

                short[] categorias = new short[lbCategorias.Items.Count];

                for (int i = 0; i < categorias.Length; i++)
                {
                    string item = lbCategorias.Items[i].ToString();

                    int guionPos = item.IndexOf('-');

                    string valor = item.Substring(0, guionPos);

                    categorias[i] = Convert.ToInt16(valor);

                    //categorias[i] = Convert.ToInt16(lbCategorias.Items[i].ToString().Substring(0, 1));
                }

                if (idArchivo == 0)
                {
                    MessageBox.Show(archivo.AgregarDocumento(categorias), ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(archivo.ActualizarDocumento(idArchivo, categorias, archivoSeleccionado), ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                txtTituloArchivo.Text = string.Empty;
                txtRutaArchivo.Text = string.Empty;
                txtObservaciones.Text = string.Empty;

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region btnCategorias_Click
        private void btnCategorias_Click(object sender, EventArgs e)
        {
            try
            {

                Categorias categorias = new Categorias();

                categorias.Contrato = this;

                categorias.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Compartir
        public void Compartir(List<(int, string)> values)
        {
            try
            {

                lbCategorias.Items.Clear();

                foreach (var item in values)
                {
                    lbCategorias.Items.Add(item.Item1.ToString() + "-" + item.Item2.ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " Detalle: " + ex.StackTrace, ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region CargarDatosArchivo
        private void CargarDatosArchivo()
        {
            try
            {

                string query = $"SELECT Nombre, Extension, Observaciones FROM Archivos WHERE ID = {idArchivo}";

                using (SqlConnection connection = new SqlConnection(ConexionDB.cadenaConexionSQLServer))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    txtRutaArchivo.Text = reader["Nombre"].ToString();
                                    txtTituloArchivo.Text = reader["Extension"].ToString();
                                    txtObservaciones.Text = reader["Observaciones"].ToString();
                                }
                            }
                            else
                            {
                                // Opcional: Manejar el caso cuando no hay registros encontrados.
                                MessageBox.Show("No se encontraron registros con el ID especificado.", ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }

                    query = $"SELECT CategoriaID FROM ArchivosCategorias WHERE ArchivoID = {idArchivo}";
                    List<int> categoriasIds = new List<int>();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    categoriasIds.Add(Convert.ToInt32(reader["CategoriaID"]));
                                }
                            }
                            else
                            {
                                // Opcional: Manejar el caso cuando no hay registros encontrados.
                                MessageBox.Show("No se encontraron categorías con el ID especificado.", ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }

                    string inClause = string.Join(",", categoriasIds);
                    query = $"SELECT ID,Categoria FROM Categorias WHERE ID IN ({inClause})";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    lbCategorias.Items.Add(reader["ID"].ToString() + "-" + reader["Categoria"]);
                                }
                            }
                            else
                            {
                                // Opcional: Manejar el caso cuando no hay registros encontrados.
                                MessageBox.Show("No se encontraron descripciones de categorías con los IDs especificados.", ElementosGlobales.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        #region btnCancelar_Click
        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
