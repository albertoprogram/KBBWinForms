namespace KBBWinForms
{
    partial class SubirArchivo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubirArchivo));
            lblSeleccionArchivo = new Label();
            lblRutaArchivo = new Label();
            lblTituloArchivo = new Label();
            txtRutaArchivo = new TextBox();
            txtTituloArchivo = new TextBox();
            btnExaminar = new Button();
            btnGuardar = new Button();
            btnCancelar = new Button();
            openFileDialog1 = new OpenFileDialog();
            btnCategorias = new Button();
            lbCategorias = new ListBox();
            lblCategorias = new Label();
            lblObservaciones = new Label();
            txtObservaciones = new TextBox();
            SuspendLayout();
            // 
            // lblSeleccionArchivo
            // 
            lblSeleccionArchivo.AutoSize = true;
            lblSeleccionArchivo.Font = new Font("Century Gothic", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSeleccionArchivo.Location = new Point(13, 38);
            lblSeleccionArchivo.Margin = new Padding(4, 0, 4, 0);
            lblSeleccionArchivo.Name = "lblSeleccionArchivo";
            lblSeleccionArchivo.Size = new Size(327, 19);
            lblSeleccionArchivo.TabIndex = 0;
            lblSeleccionArchivo.Text = "Busque y seleccione el archivo a subir";
            // 
            // lblRutaArchivo
            // 
            lblRutaArchivo.AutoSize = true;
            lblRutaArchivo.Font = new Font("Century Gothic", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRutaArchivo.Location = new Point(13, 104);
            lblRutaArchivo.Margin = new Padding(4, 0, 4, 0);
            lblRutaArchivo.Name = "lblRutaArchivo";
            lblRutaArchivo.Size = new Size(142, 19);
            lblRutaArchivo.TabIndex = 1;
            lblRutaArchivo.Text = "Ruta del archivo";
            // 
            // lblTituloArchivo
            // 
            lblTituloArchivo.AutoSize = true;
            lblTituloArchivo.Font = new Font("Century Gothic", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTituloArchivo.Location = new Point(13, 156);
            lblTituloArchivo.Margin = new Padding(4, 0, 4, 0);
            lblTituloArchivo.Name = "lblTituloArchivo";
            lblTituloArchivo.Size = new Size(147, 19);
            lblTituloArchivo.TabIndex = 2;
            lblTituloArchivo.Text = "Título del archivo";
            // 
            // txtRutaArchivo
            // 
            txtRutaArchivo.ForeColor = Color.FromArgb(39, 76, 119);
            txtRutaArchivo.Location = new Point(168, 99);
            txtRutaArchivo.Margin = new Padding(4, 3, 4, 3);
            txtRutaArchivo.Name = "txtRutaArchivo";
            txtRutaArchivo.ReadOnly = true;
            txtRutaArchivo.Size = new Size(985, 28);
            txtRutaArchivo.TabIndex = 3;
            // 
            // txtTituloArchivo
            // 
            txtTituloArchivo.ForeColor = Color.FromArgb(39, 76, 119);
            txtTituloArchivo.Location = new Point(168, 156);
            txtTituloArchivo.Margin = new Padding(4, 3, 4, 3);
            txtTituloArchivo.Name = "txtTituloArchivo";
            txtTituloArchivo.ReadOnly = true;
            txtTituloArchivo.Size = new Size(985, 28);
            txtTituloArchivo.TabIndex = 4;
            // 
            // btnExaminar
            // 
            btnExaminar.Font = new Font("Century Gothic", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExaminar.Location = new Point(1161, 99);
            btnExaminar.Margin = new Padding(4, 3, 4, 3);
            btnExaminar.Name = "btnExaminar";
            btnExaminar.Size = new Size(118, 30);
            btnExaminar.TabIndex = 5;
            btnExaminar.Text = "Examinar";
            btnExaminar.UseVisualStyleBackColor = true;
            btnExaminar.Click += btnExaminar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Font = new Font("Century Gothic", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardar.Location = new Point(1161, 609);
            btnGuardar.Margin = new Padding(4, 3, 4, 3);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(118, 30);
            btnGuardar.TabIndex = 6;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Font = new Font("Century Gothic", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancelar.Location = new Point(1036, 609);
            btnCancelar.Margin = new Padding(4, 3, 4, 3);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(118, 30);
            btnCancelar.TabIndex = 7;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnCategorias
            // 
            btnCategorias.Font = new Font("Century Gothic", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCategorias.Location = new Point(1161, 267);
            btnCategorias.Margin = new Padding(4, 3, 4, 3);
            btnCategorias.Name = "btnCategorias";
            btnCategorias.Size = new Size(118, 30);
            btnCategorias.TabIndex = 8;
            btnCategorias.Text = "Categorías";
            btnCategorias.UseVisualStyleBackColor = true;
            btnCategorias.Click += btnCategorias_Click;
            // 
            // lbCategorias
            // 
            lbCategorias.FormattingEnabled = true;
            lbCategorias.ItemHeight = 21;
            lbCategorias.Location = new Point(168, 212);
            lbCategorias.Margin = new Padding(4, 3, 4, 3);
            lbCategorias.Name = "lbCategorias";
            lbCategorias.ScrollAlwaysVisible = true;
            lbCategorias.Size = new Size(985, 130);
            lbCategorias.TabIndex = 9;
            // 
            // lblCategorias
            // 
            lblCategorias.AutoSize = true;
            lblCategorias.Font = new Font("Century Gothic", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCategorias.Location = new Point(13, 257);
            lblCategorias.Margin = new Padding(4, 0, 4, 0);
            lblCategorias.Name = "lblCategorias";
            lblCategorias.Size = new Size(98, 19);
            lblCategorias.TabIndex = 10;
            lblCategorias.Text = "Categorías";
            // 
            // lblObservaciones
            // 
            lblObservaciones.AutoSize = true;
            lblObservaciones.Font = new Font("Century Gothic", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblObservaciones.Location = new Point(13, 380);
            lblObservaciones.Margin = new Padding(4, 0, 4, 0);
            lblObservaciones.Name = "lblObservaciones";
            lblObservaciones.Size = new Size(265, 19);
            lblObservaciones.TabIndex = 11;
            lblObservaciones.Text = "Breve Reseña / Observaciones";
            // 
            // txtObservaciones
            // 
            txtObservaciones.Location = new Point(13, 414);
            txtObservaciones.Margin = new Padding(4, 3, 4, 3);
            txtObservaciones.Multiline = true;
            txtObservaciones.Name = "txtObservaciones";
            txtObservaciones.ScrollBars = ScrollBars.Vertical;
            txtObservaciones.Size = new Size(1266, 168);
            txtObservaciones.TabIndex = 12;
            // 
            // SubirArchivo
            // 
            AutoScaleDimensions = new SizeF(10F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1294, 652);
            Controls.Add(txtObservaciones);
            Controls.Add(lblObservaciones);
            Controls.Add(lblCategorias);
            Controls.Add(lbCategorias);
            Controls.Add(btnCategorias);
            Controls.Add(btnCancelar);
            Controls.Add(btnGuardar);
            Controls.Add(btnExaminar);
            Controls.Add(txtTituloArchivo);
            Controls.Add(txtRutaArchivo);
            Controls.Add(lblTituloArchivo);
            Controls.Add(lblRutaArchivo);
            Controls.Add(lblSeleccionArchivo);
            Font = new Font("Century Gothic", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "SubirArchivo";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Subir Archivo";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblSeleccionArchivo;
        private Label lblRutaArchivo;
        private Label lblTituloArchivo;
        private TextBox txtRutaArchivo;
        private TextBox txtTituloArchivo;
        private Button btnExaminar;
        private Button btnGuardar;
        private Button btnCancelar;
        private OpenFileDialog openFileDialog1;
        private Button btnCategorias;
        private ListBox lbCategorias;
        private Label lblCategorias;
        private Label lblObservaciones;
        private TextBox txtObservaciones;
    }
}