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
    public partial class Contenedor : Form
    {
        #region Constructors
        public Contenedor()
        {
            InitializeComponent();

            //Colores
            //----------------------------------------------------------------
            string hexColor = "#E7ECEF";

            Color color = ColorTranslator.FromHtml(hexColor);

            this.BackColor = color;

            controlDeArchivosToolStripMenuItem.BackColor = color;

            hexColor = "#274C77";

            color = ColorTranslator.FromHtml(hexColor);

            this.ForeColor = color;

            controlDeArchivosToolStripMenuItem.ForeColor = color;
            //----------------------------------------------------------------

            Text = ElementosGlobales.NombreSistema;

            this.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);
        }
        #endregion

        #region controlDeArchivosToolStripMenuItem_Click
        private void controlDeArchivosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ControlArchivos controlArchivos = new ControlArchivos(this);

            controlArchivos.MdiParent = this;

            controlArchivos.Show();
        }
        #endregion

        #region MainForm_FormClosing
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                string tempFolderPath = Path.Combine(Application.StartupPath, "temp");

                if (Directory.Exists(tempFolderPath))
                {
                    string[] files = Directory.GetFiles(tempFolderPath);

                    foreach (string file in files)
                    {
                        File.Delete(file);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar archivos en la carpeta 'temp': {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
