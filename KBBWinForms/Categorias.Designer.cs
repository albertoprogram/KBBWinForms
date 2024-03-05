namespace KBBWinForms
{
    partial class Categorias
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
            lblCategoria = new Label();
            textBox1 = new TextBox();
            btnGuardarCategoria = new Button();
            dgvCategorias = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvCategorias).BeginInit();
            SuspendLayout();
            // 
            // lblCategoria
            // 
            lblCategoria.AutoSize = true;
            lblCategoria.Location = new Point(56, 76);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Size = new Size(74, 20);
            lblCategoria.TabIndex = 0;
            lblCategoria.Text = "Categoría";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(136, 73);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(561, 27);
            textBox1.TabIndex = 1;
            // 
            // btnGuardarCategoria
            // 
            btnGuardarCategoria.Location = new Point(603, 122);
            btnGuardarCategoria.Name = "btnGuardarCategoria";
            btnGuardarCategoria.Size = new Size(94, 29);
            btnGuardarCategoria.TabIndex = 2;
            btnGuardarCategoria.Text = "Guardar";
            btnGuardarCategoria.UseVisualStyleBackColor = true;
            // 
            // dgvCategorias
            // 
            dgvCategorias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCategorias.Location = new Point(12, 179);
            dgvCategorias.Name = "dgvCategorias";
            dgvCategorias.RowHeadersWidth = 51;
            dgvCategorias.Size = new Size(776, 385);
            dgvCategorias.TabIndex = 3;
            // 
            // Categorias
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 576);
            Controls.Add(dgvCategorias);
            Controls.Add(btnGuardarCategoria);
            Controls.Add(textBox1);
            Controls.Add(lblCategoria);
            Name = "Categorias";
            Text = "Categorias";
            ((System.ComponentModel.ISupportInitialize)dgvCategorias).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCategoria;
        private TextBox textBox1;
        private Button btnGuardarCategoria;
        private DataGridView dgvCategorias;
    }
}