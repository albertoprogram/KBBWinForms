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
            txtFiltroCategorias = new TextBox();
            lblFiltroCategorias = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvCategorias).BeginInit();
            SuspendLayout();
            // 
            // lblCategoria
            // 
            lblCategoria.AutoSize = true;
            lblCategoria.Font = new Font("Century Gothic", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCategoria.Location = new Point(70, 44);
            lblCategoria.Margin = new Padding(4, 0, 4, 0);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Size = new Size(90, 19);
            lblCategoria.TabIndex = 0;
            lblCategoria.Text = "Categoría";
            // 
            // txtCategoria
            // 
            txtCategoria.Location = new Point(170, 36);
            txtCategoria.Margin = new Padding(4, 3, 4, 3);
            txtCategoria.MaxLength = 350;
            txtCategoria.Name = "txtCategoria";
            txtCategoria.Size = new Size(888, 28);
            txtCategoria.TabIndex = 1;
            // 
            // btnGuardarCategoria
            // 
            btnGuardarCategoria.Font = new Font("Century Gothic", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardarCategoria.Location = new Point(940, 71);
            btnGuardarCategoria.Margin = new Padding(4, 3, 4, 3);
            btnGuardarCategoria.Name = "btnGuardarCategoria";
            btnGuardarCategoria.Size = new Size(118, 30);
            btnGuardarCategoria.TabIndex = 2;
            btnGuardarCategoria.Text = "Guardar";
            btnGuardarCategoria.UseVisualStyleBackColor = true;
            btnGuardarCategoria.Click += btnGuardarCategoria_Click;
            // 
            // dgvCategorias
            // 
            dgvCategorias.AllowUserToAddRows = false;
            dgvCategorias.AllowUserToDeleteRows = false;
            dgvCategorias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvCategorias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCategorias.Columns.AddRange(new DataGridViewColumn[] { Seleccion, IdCategoria, Categoria });
            dgvCategorias.Location = new Point(15, 207);
            dgvCategorias.Margin = new Padding(4, 3, 4, 3);
            dgvCategorias.Name = "dgvCategorias";
            dgvCategorias.RowHeadersWidth = 51;
            dgvCategorias.RowTemplate.DefaultCellStyle.BackColor = Color.FromArgb(231, 236, 239);
            dgvCategorias.RowTemplate.DefaultCellStyle.ForeColor = Color.FromArgb(39, 76, 119);
            dgvCategorias.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.FromArgb(39, 76, 119);
            dgvCategorias.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.FromArgb(231, 236, 239);
            dgvCategorias.Size = new Size(1108, 405);
            dgvCategorias.TabIndex = 3;
            // 
            // Seleccion
            // 
            Seleccion.HeaderText = "Selección";
            Seleccion.MinimumWidth = 6;
            Seleccion.Name = "Seleccion";
            Seleccion.Width = 95;
            // 
            // IdCategoria
            // 
            IdCategoria.HeaderText = "ID Categoría";
            IdCategoria.MinimumWidth = 6;
            IdCategoria.Name = "IdCategoria";
            IdCategoria.ReadOnly = true;
            IdCategoria.Width = 136;
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
            btnEnviar.Font = new Font("Century Gothic", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEnviar.Location = new Point(940, 627);
            btnEnviar.Margin = new Padding(4, 3, 4, 3);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new Size(118, 30);
            btnEnviar.TabIndex = 4;
            btnEnviar.Text = "Enviar";
            btnEnviar.UseVisualStyleBackColor = true;
            btnEnviar.Click += btnEnviar_Click;
            // 
            // txtFiltroCategorias
            // 
            txtFiltroCategorias.Location = new Point(744, 138);
            txtFiltroCategorias.Margin = new Padding(4, 3, 4, 3);
            txtFiltroCategorias.Name = "txtFiltroCategorias";
            txtFiltroCategorias.Size = new Size(314, 28);
            txtFiltroCategorias.TabIndex = 5;
            txtFiltroCategorias.TextChanged += txtFiltroCategorias_TextChanged;
            // 
            // lblFiltroCategorias
            // 
            lblFiltroCategorias.AutoSize = true;
            lblFiltroCategorias.Font = new Font("Century Gothic", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFiltroCategorias.Location = new Point(678, 141);
            lblFiltroCategorias.Margin = new Padding(4, 0, 4, 0);
            lblFiltroCategorias.Name = "lblFiltroCategorias";
            lblFiltroCategorias.Size = new Size(45, 19);
            lblFiltroCategorias.TabIndex = 6;
            lblFiltroCategorias.Text = "Filtro";
            // 
            // Categorias
            // 
            AutoScaleDimensions = new SizeF(10F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1136, 674);
            Controls.Add(lblFiltroCategorias);
            Controls.Add(txtFiltroCategorias);
            Controls.Add(btnEnviar);
            Controls.Add(dgvCategorias);
            Controls.Add(btnGuardarCategoria);
            Controls.Add(txtCategoria);
            Controls.Add(lblCategoria);
            Font = new Font("Century Gothic", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "Categorias";
            StartPosition = FormStartPosition.CenterScreen;
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
        private TextBox txtFiltroCategorias;
        private Label lblFiltroCategorias;
    }
}