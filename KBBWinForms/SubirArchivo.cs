using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        #endregion

        #region Constructores
        public SubirArchivo()
        {
            InitializeComponent();
        }
        #endregion

        #region btnExaminar_Click

        private void btnExaminar_Click(object sender, EventArgs e)
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

            openFileDialog1.FileName = string.Empty;

            openFileDialog1.Filter =
                "Archivos de Word|*.doc;*.docx|" +
                "Archivos de Excel|*.xls;*.xlsx|" +
                "Archivos de Power Point|*.ppt;*.pptx;*.pps;*.ppsx|" +
                "Archivos PDF|*.pdf|" +
                "Archivos de imagen|*.jpg;*.jpeg;*.png|" +
                "Archivos de texto|*.txt|" +
                "Archivos de audio|*.mp3|" +
                "Archivos de vídeo|*.mp4;*.wmv";

            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                rutaSeleccionada = Path.GetDirectoryName(openFileDialog1.FileName);
                txtRutaArchivo.Text = openFileDialog1.FileName;
                txtTituloArchivo.Text = openFileDialog1.SafeFileName;
            }
        }
        #endregion

        #region btnGuardar_Click
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            byte[] data = null;

            Stream stream = openFileDialog1.OpenFile();

            MemoryStream memoryStream = new MemoryStream();

            stream.CopyTo(memoryStream);

            data = memoryStream.ToArray();

            archivo.Nombre = txtTituloArchivo.Text;
            archivo.Archivo = data;
            archivo.Extension = openFileDialog1.SafeFileName;
            archivo.Observaciones = txtObservaciones.Text;

            short[] categorias = new short[lbCategorias.Items.Count];

            for (int i = 0; i < categorias.Length; i++)
            {
                categorias[i] = Convert.ToInt16(lbCategorias.Items[i].ToString().Substring(0, 1));
            }

            MessageBox.Show(archivo.AgregarDocumento(categorias));

            txtTituloArchivo.Text = string.Empty;
            txtRutaArchivo.Text = string.Empty;
            txtObservaciones.Text = string.Empty;
        }
        #endregion

        #region btnCategorias_Click
        private void btnCategorias_Click(object sender, EventArgs e)
        {
            Categorias categorias = new Categorias();

            categorias.Contrato = this;

            categorias.ShowDialog();
        }
        #endregion

        #region Compartir
        public void Compartir(List<(int, string)> values)
        {
            lbCategorias.Items.Clear();

            foreach (var item in values)
            {
                lbCategorias.Items.Add(item.Item1.ToString() + "-" + item.Item2.ToString());
            }
        }
        #endregion

    }
}
