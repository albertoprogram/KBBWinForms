namespace KBBWinForms
{
    partial class ControlArchivos
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
            dgvDocumentos = new DataGridView();
            btnAgregar = new Button();
            btnEditar = new Button();
            btnVer = new Button();
            lblPagina = new Label();
            txtPagina = new TextBox();
            lblCantidadRegistros = new Label();
            btnAnterior = new Button();
            btnSiguiente = new Button();
            lblTotalRegistros = new Label();
            txtTotalRegistros = new TextBox();
            txtTotalPaginas = new TextBox();
            lblTotalPaginas = new Label();
            cmbCantidadRegistrosXPagina = new ComboBox();
            btnInicio = new Button();
            btnFin = new Button();
            tvCategorias = new TreeView();
            ((System.ComponentModel.ISupportInitialize)dgvDocumentos).BeginInit();
            SuspendLayout();
            // 
            // dgvDocumentos
            // 
            dgvDocumentos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDocumentos.Location = new Point(324, 12);
            dgvDocumentos.MultiSelect = false;
            dgvDocumentos.Name = "dgvDocumentos";
            dgvDocumentos.ReadOnly = true;
            dgvDocumentos.RowHeadersWidth = 51;
            dgvDocumentos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDocumentos.Size = new Size(1059, 560);
            dgvDocumentos.TabIndex = 0;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(1089, 691);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(94, 29);
            btnAgregar.TabIndex = 1;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(1189, 691);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(94, 29);
            btnEditar.TabIndex = 2;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            // 
            // btnVer
            // 
            btnVer.Location = new Point(1289, 691);
            btnVer.Name = "btnVer";
            btnVer.Size = new Size(94, 29);
            btnVer.TabIndex = 3;
            btnVer.Text = "Ver";
            btnVer.UseVisualStyleBackColor = true;
            btnVer.Click += btnVer_Click;
            // 
            // lblPagina
            // 
            lblPagina.AutoSize = true;
            lblPagina.Location = new Point(324, 581);
            lblPagina.Name = "lblPagina";
            lblPagina.Size = new Size(53, 20);
            lblPagina.TabIndex = 4;
            lblPagina.Text = "Página";
            // 
            // txtPagina
            // 
            txtPagina.Location = new Point(383, 578);
            txtPagina.Name = "txtPagina";
            txtPagina.Size = new Size(87, 27);
            txtPagina.TabIndex = 5;
            txtPagina.Text = "1";
            txtPagina.Click += txtPagina_Click;
            txtPagina.TextChanged += txtPagina_TextChanged;
            txtPagina.KeyPress += txtPagina_KeyPress;
            // 
            // lblCantidadRegistros
            // 
            lblCantidadRegistros.AutoSize = true;
            lblCantidadRegistros.Location = new Point(476, 582);
            lblCantidadRegistros.Name = "lblCantidadRegistros";
            lblCantidadRegistros.Size = new Size(230, 20);
            lblCantidadRegistros.TabIndex = 6;
            lblCantidadRegistros.Text = "Cantidad de Registros por Página";
            // 
            // btnAnterior
            // 
            btnAnterior.Location = new Point(1179, 578);
            btnAnterior.Name = "btnAnterior";
            btnAnterior.Size = new Size(54, 29);
            btnAnterior.TabIndex = 8;
            btnAnterior.Text = "<<";
            btnAnterior.UseVisualStyleBackColor = true;
            btnAnterior.Click += btnAnterior_Click;
            // 
            // btnSiguiente
            // 
            btnSiguiente.Location = new Point(1239, 578);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(53, 29);
            btnSiguiente.TabIndex = 9;
            btnSiguiente.Text = ">>";
            btnSiguiente.UseVisualStyleBackColor = true;
            btnSiguiente.Click += btnSiguiente_Click;
            // 
            // lblTotalRegistros
            // 
            lblTotalRegistros.AutoSize = true;
            lblTotalRegistros.Location = new Point(805, 581);
            lblTotalRegistros.Name = "lblTotalRegistros";
            lblTotalRegistros.Size = new Size(128, 20);
            lblTotalRegistros.TabIndex = 10;
            lblTotalRegistros.Text = "Total de Registros";
            // 
            // txtTotalRegistros
            // 
            txtTotalRegistros.Location = new Point(939, 578);
            txtTotalRegistros.Name = "txtTotalRegistros";
            txtTotalRegistros.ReadOnly = true;
            txtTotalRegistros.Size = new Size(116, 27);
            txtTotalRegistros.TabIndex = 11;
            txtTotalRegistros.Text = "0";
            // 
            // txtTotalPaginas
            // 
            txtTotalPaginas.Location = new Point(939, 615);
            txtTotalPaginas.Name = "txtTotalPaginas";
            txtTotalPaginas.ReadOnly = true;
            txtTotalPaginas.Size = new Size(116, 27);
            txtTotalPaginas.TabIndex = 13;
            txtTotalPaginas.Text = "0";
            // 
            // lblTotalPaginas
            // 
            lblTotalPaginas.AutoSize = true;
            lblTotalPaginas.Location = new Point(805, 618);
            lblTotalPaginas.Name = "lblTotalPaginas";
            lblTotalPaginas.Size = new Size(117, 20);
            lblTotalPaginas.TabIndex = 12;
            lblTotalPaginas.Text = "Total de Páginas";
            // 
            // cmbCantidadRegistrosXPagina
            // 
            cmbCantidadRegistrosXPagina.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCantidadRegistrosXPagina.FormattingEnabled = true;
            cmbCantidadRegistrosXPagina.Items.AddRange(new object[] { "2", "3", "5", "10", "15", "20", "25" });
            cmbCantidadRegistrosXPagina.Location = new Point(712, 578);
            cmbCantidadRegistrosXPagina.Name = "cmbCantidadRegistrosXPagina";
            cmbCantidadRegistrosXPagina.Size = new Size(75, 28);
            cmbCantidadRegistrosXPagina.TabIndex = 14;
            cmbCantidadRegistrosXPagina.SelectedIndexChanged += cmbCantidadRegistrosXPagina_SelectedIndexChanged;
            // 
            // btnInicio
            // 
            btnInicio.Location = new Point(1106, 578);
            btnInicio.Name = "btnInicio";
            btnInicio.Size = new Size(67, 29);
            btnInicio.TabIndex = 15;
            btnInicio.Text = "Inicio";
            btnInicio.UseVisualStyleBackColor = true;
            btnInicio.Click += btnInicio_Click;
            // 
            // btnFin
            // 
            btnFin.Location = new Point(1298, 578);
            btnFin.Name = "btnFin";
            btnFin.Size = new Size(64, 29);
            btnFin.TabIndex = 16;
            btnFin.Text = "Fin";
            btnFin.UseVisualStyleBackColor = true;
            btnFin.Click += btnFin_Click;
            // 
            // tvCategorias
            // 
            tvCategorias.Location = new Point(12, 12);
            tvCategorias.Name = "tvCategorias";
            tvCategorias.Size = new Size(306, 560);
            tvCategorias.TabIndex = 17;
            tvCategorias.AfterSelect += tvCategorias_AfterSelect;
            // 
            // ControlArchivos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1395, 732);
            Controls.Add(tvCategorias);
            Controls.Add(btnFin);
            Controls.Add(btnInicio);
            Controls.Add(cmbCantidadRegistrosXPagina);
            Controls.Add(txtTotalPaginas);
            Controls.Add(lblTotalPaginas);
            Controls.Add(txtTotalRegistros);
            Controls.Add(lblTotalRegistros);
            Controls.Add(btnSiguiente);
            Controls.Add(btnAnterior);
            Controls.Add(lblCantidadRegistros);
            Controls.Add(txtPagina);
            Controls.Add(lblPagina);
            Controls.Add(btnVer);
            Controls.Add(btnEditar);
            Controls.Add(btnAgregar);
            Controls.Add(dgvDocumentos);
            Name = "ControlArchivos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Control Archivos";
            Load += ControlArchivos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDocumentos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvDocumentos;
        private Button btnAgregar;
        private Button btnEditar;
        private Button btnVer;
        private Label lblPagina;
        private TextBox txtPagina;
        private Label lblCantidadRegistros;
        private Button btnAnterior;
        private Button btnSiguiente;
        private Label lblTotalRegistros;
        private TextBox txtTotalRegistros;
        private TextBox txtTotalPaginas;
        private Label lblTotalPaginas;
        private ComboBox cmbCantidadRegistrosXPagina;
        private Button btnInicio;
        private Button btnFin;
        private TreeView tvCategorias;
    }
}