﻿namespace KBBWinForms
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
            SuspendLayout();
            // 
            // lblSeleccionArchivo
            // 
            lblSeleccionArchivo.AutoSize = true;
            lblSeleccionArchivo.Location = new Point(44, 41);
            lblSeleccionArchivo.Name = "lblSeleccionArchivo";
            lblSeleccionArchivo.Size = new Size(257, 20);
            lblSeleccionArchivo.TabIndex = 0;
            lblSeleccionArchivo.Text = "Busque y seleccione el archivo a subir";
            // 
            // lblRutaArchivo
            // 
            lblRutaArchivo.AutoSize = true;
            lblRutaArchivo.Location = new Point(44, 98);
            lblRutaArchivo.Name = "lblRutaArchivo";
            lblRutaArchivo.Size = new Size(116, 20);
            lblRutaArchivo.TabIndex = 1;
            lblRutaArchivo.Text = "Ruta del archivo";
            // 
            // lblTituloArchivo
            // 
            lblTituloArchivo.AutoSize = true;
            lblTituloArchivo.Location = new Point(44, 152);
            lblTituloArchivo.Name = "lblTituloArchivo";
            lblTituloArchivo.Size = new Size(124, 20);
            lblTituloArchivo.TabIndex = 2;
            lblTituloArchivo.Text = "Título del archivo";
            // 
            // txtRutaArchivo
            // 
            txtRutaArchivo.Location = new Point(198, 95);
            txtRutaArchivo.Name = "txtRutaArchivo";
            txtRutaArchivo.ReadOnly = true;
            txtRutaArchivo.Size = new Size(568, 27);
            txtRutaArchivo.TabIndex = 3;
            // 
            // txtTituloArchivo
            // 
            txtTituloArchivo.Location = new Point(198, 149);
            txtTituloArchivo.Name = "txtTituloArchivo";
            txtTituloArchivo.ReadOnly = true;
            txtTituloArchivo.Size = new Size(568, 27);
            txtTituloArchivo.TabIndex = 4;
            // 
            // btnExaminar
            // 
            btnExaminar.Location = new Point(782, 93);
            btnExaminar.Name = "btnExaminar";
            btnExaminar.Size = new Size(94, 29);
            btnExaminar.TabIndex = 5;
            btnExaminar.Text = "Examinar";
            btnExaminar.UseVisualStyleBackColor = true;
            btnExaminar.Click += btnExaminar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(672, 422);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(94, 29);
            btnGuardar.TabIndex = 6;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(782, 422);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(94, 29);
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
            btnCategorias.Location = new Point(782, 244);
            btnCategorias.Name = "btnCategorias";
            btnCategorias.Size = new Size(94, 29);
            btnCategorias.TabIndex = 8;
            btnCategorias.Text = "Categorías";
            btnCategorias.UseVisualStyleBackColor = true;
            btnCategorias.Click += btnCategorias_Click;
            // 
            // lbCategorias
            // 
            lbCategorias.FormattingEnabled = true;
            lbCategorias.Location = new Point(198, 208);
            lbCategorias.Name = "lbCategorias";
            lbCategorias.Size = new Size(568, 124);
            lbCategorias.TabIndex = 9;
            // 
            // SubirArchivo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(888, 463);
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
    }
}