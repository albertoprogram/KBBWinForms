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
            txtCategoria = new TextBox();
            btnGuardarCategoria = new Button();
            dgvCategorias = new DataGridView();
            Seleccion = new DataGridViewCheckBoxColumn();
            IdCategoria = new DataGridViewTextBoxColumn();
            Categoria = new DataGridViewTextBoxColumn();
            btnEnviar = new Button();
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
            // txtCategoria
            // 
            txtCategoria.Location = new Point(136, 73);
            txtCategoria.MaxLength = 350;
            txtCategoria.Name = "txtCategoria";
            txtCategoria.Size = new Size(561, 27);
            txtCategoria.TabIndex = 1;
            // 
            // btnGuardarCategoria
            // 
            btnGuardarCategoria.Location = new Point(603, 122);
            btnGuardarCategoria.Name = "btnGuardarCategoria";
            btnGuardarCategoria.Size = new Size(94, 29);
            btnGuardarCategoria.TabIndex = 2;
            btnGuardarCategoria.Text = "Guardar";
            btnGuardarCategoria.UseVisualStyleBackColor = true;
            btnGuardarCategoria.Click += btnGuardarCategoria_Click;
            // 
            // dgvCategorias
            // 
            dgvCategorias.AllowUserToAddRows = false;
            dgvCategorias.AllowUserToDeleteRows = false;
            dgvCategorias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCategorias.Columns.AddRange(new DataGridViewColumn[] { Seleccion, IdCategoria, Categoria });
            dgvCategorias.Location = new Point(12, 179);
            dgvCategorias.Name = "dgvCategorias";
            dgvCategorias.RowHeadersWidth = 51;
            dgvCategorias.Size = new Size(776, 385);
            dgvCategorias.TabIndex = 3;
            // 
            // Seleccion
            // 
            Seleccion.HeaderText = "Selección";
            Seleccion.MinimumWidth = 6;
            Seleccion.Name = "Seleccion";
            Seleccion.Width = 125;
            // 
            // IdCategoria
            // 
            IdCategoria.HeaderText = "ID Categoría";
            IdCategoria.MinimumWidth = 6;
            IdCategoria.Name = "IdCategoria";
            IdCategoria.ReadOnly = true;
            IdCategoria.Width = 125;
            // 
            // Categoria
            // 
            Categoria.HeaderText = "Categoría";
            Categoria.MinimumWidth = 6;
            Categoria.Name = "Categoria";
            Categoria.ReadOnly = true;
            Categoria.Width = 125;
            // 
            // btnEnviar
            // 
            btnEnviar.Location = new Point(603, 588);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new Size(94, 29);
            btnEnviar.TabIndex = 4;
            btnEnviar.Text = "Enviar";
            btnEnviar.UseVisualStyleBackColor = true;
            btnEnviar.Click += btnEnviar_Click;
            // 
            // Categorias
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(801, 641);
            Controls.Add(btnEnviar);
            Controls.Add(dgvCategorias);
            Controls.Add(btnGuardarCategoria);
            Controls.Add(txtCategoria);
            Controls.Add(lblCategoria);
            Name = "Categorias";
            Text = "Categorias";
            Load += Categorias_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCategorias).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCategoria;
        private TextBox txtCategoria;
        private Button btnGuardarCategoria;
        private DataGridView dgvCategorias;
        private DataGridViewCheckBoxColumn Seleccion;
        private DataGridViewTextBoxColumn IdCategoria;
        private DataGridViewTextBoxColumn Categoria;
        private Button btnEnviar;
    }
}