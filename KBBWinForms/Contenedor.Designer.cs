namespace KBBWinForms
{
    partial class Contenedor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Contenedor));
            menuStrip1 = new MenuStrip();
            controlDeArchivosToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { controlDeArchivosToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1063, 27);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // controlDeArchivosToolStripMenuItem
            // 
            controlDeArchivosToolStripMenuItem.Font = new Font("Century Gothic", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            controlDeArchivosToolStripMenuItem.Name = "controlDeArchivosToolStripMenuItem";
            controlDeArchivosToolStripMenuItem.Size = new Size(185, 23);
            controlDeArchivosToolStripMenuItem.Text = "Control de Archivos";
            controlDeArchivosToolStripMenuItem.Click += controlDeArchivosToolStripMenuItem_Click;
            // 
            // Contenedor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1063, 637);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            IsMdiContainer = true;
            MainMenuStrip = menuStrip1;
            Name = "Contenedor";
            Text = "Contenedor";
            WindowState = FormWindowState.Maximized;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem controlDeArchivosToolStripMenuItem;
    }
}