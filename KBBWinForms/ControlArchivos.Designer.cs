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
            ID = new DataGridViewTextBoxColumn();
            Nombre = new DataGridViewTextBoxColumn();
            Observaciones = new DataGridViewTextBoxColumn();
            Paginas = new DataGridViewTextBoxColumn();
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
            lblBusqueda = new Label();
            txtBusqueda = new TextBox();
            btnBuscar = new Button();
            lblStatus = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvDocumentos).BeginInit();
            SuspendLayout();
            // 
            // dgvDocumentos
            // 
            dgvDocumentos.AllowUserToAddRows = false;
            dgvDocumentos.AllowUserToDeleteRows = false;
            dgvDocumentos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvDocumentos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvDocumentos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDocumentos.Columns.AddRange(new DataGridViewColumn[] { ID, Nombre, Observaciones, Paginas });
            dgvDocumentos.Location = new Point(404, 93);
            dgvDocumentos.MultiSelect = false;
            dgvDocumentos.Name = "dgvDocumentos";
            dgvDocumentos.ReadOnly = true;
            dgvDocumentos.RowHeadersWidth = 51;
            dgvDocumentos.RowTemplate.DefaultCellStyle.BackColor = Color.FromArgb(231, 236, 239);
            dgvDocumentos.RowTemplate.DefaultCellStyle.ForeColor = Color.FromArgb(39, 76, 119);
            dgvDocumentos.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.FromArgb(39, 76, 119);
            dgvDocumentos.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.FromArgb(231, 236, 239);
            dgvDocumentos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDocumentos.Size = new Size(1323, 576);
            dgvDocumentos.TabIndex = 0;
            // 
            // ID
            // 
            ID.HeaderText = "ID";
            ID.MinimumWidth = 6;
            ID.Name = "ID";
            ID.ReadOnly = true;
            ID.Width = 57;
            // 
            // Nombre
            // 
            Nombre.HeaderText = "Nombre";
            Nombre.MinimumWidth = 6;
            Nombre.Name = "Nombre";
            Nombre.ReadOnly = true;
            Nombre.Width = 106;
            // 
            // Observaciones
            // 
            Observaciones.HeaderText = "Observaciones";
            Observaciones.MinimumWidth = 6;
            Observaciones.Name = "Observaciones";
            Observaciones.ReadOnly = true;
            Observaciones.Width = 165;
            // 
            // Paginas
            // 
            Paginas.HeaderText = "Páginas";
            Paginas.MinimumWidth = 6;
            Paginas.Name = "Paginas";
            Paginas.ReadOnly = true;
            Paginas.Width = 104;
            // 
            // btnAgregar
            // 
            btnAgregar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnAgregar.Font = new Font("Century Gothic", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAgregar.Location = new Point(1361, 726);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(118, 30);
            btnAgregar.TabIndex = 1;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnEditar
            // 
            btnEditar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnEditar.Font = new Font("Century Gothic", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEditar.Location = new Point(1487, 726);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(118, 30);
            btnEditar.TabIndex = 2;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            // 
            // btnVer
            // 
            btnVer.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnVer.Font = new Font("Century Gothic", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnVer.Location = new Point(1611, 726);
            btnVer.Name = "btnVer";
            btnVer.Size = new Size(118, 30);
            btnVer.TabIndex = 3;
            btnVer.Text = "Ver";
            btnVer.UseVisualStyleBackColor = true;
            btnVer.Click += btnVer_Click;
            // 
            // lblPagina
            // 
            lblPagina.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblPagina.AutoSize = true;
            lblPagina.Location = new Point(408, 680);
            lblPagina.Name = "lblPagina";
            lblPagina.Size = new Size(68, 21);
            lblPagina.TabIndex = 4;
            lblPagina.Text = "Página";
            lblPagina.Visible = false;
            // 
            // txtPagina
            // 
            txtPagina.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            txtPagina.Location = new Point(481, 677);
            txtPagina.Name = "txtPagina";
            txtPagina.Size = new Size(107, 28);
            txtPagina.TabIndex = 5;
            txtPagina.Text = "1";
            txtPagina.Visible = false;
            txtPagina.Click += txtPagina_Click;
            txtPagina.TextChanged += txtPagina_TextChanged;
            txtPagina.KeyPress += txtPagina_KeyPress;
            // 
            // lblCantidadRegistros
            // 
            lblCantidadRegistros.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblCantidadRegistros.AutoSize = true;
            lblCantidadRegistros.Location = new Point(598, 681);
            lblCantidadRegistros.Name = "lblCantidadRegistros";
            lblCantidadRegistros.Size = new Size(293, 21);
            lblCantidadRegistros.TabIndex = 6;
            lblCantidadRegistros.Text = "Cantidad de Registros por Página";
            lblCantidadRegistros.Visible = false;
            // 
            // btnAnterior
            // 
            btnAnterior.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnAnterior.Location = new Point(1477, 677);
            btnAnterior.Name = "btnAnterior";
            btnAnterior.Size = new Size(68, 30);
            btnAnterior.TabIndex = 8;
            btnAnterior.Text = "<<";
            btnAnterior.UseVisualStyleBackColor = true;
            btnAnterior.Visible = false;
            btnAnterior.Click += btnAnterior_Click;
            // 
            // btnSiguiente
            // 
            btnSiguiente.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSiguiente.Location = new Point(1551, 677);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(67, 30);
            btnSiguiente.TabIndex = 9;
            btnSiguiente.Text = ">>";
            btnSiguiente.UseVisualStyleBackColor = true;
            btnSiguiente.Visible = false;
            btnSiguiente.Click += btnSiguiente_Click;
            // 
            // lblTotalRegistros
            // 
            lblTotalRegistros.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblTotalRegistros.AutoSize = true;
            lblTotalRegistros.Location = new Point(1009, 680);
            lblTotalRegistros.Name = "lblTotalRegistros";
            lblTotalRegistros.Size = new Size(156, 21);
            lblTotalRegistros.TabIndex = 10;
            lblTotalRegistros.Text = "Total de Registros";
            lblTotalRegistros.Visible = false;
            // 
            // txtTotalRegistros
            // 
            txtTotalRegistros.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            txtTotalRegistros.Location = new Point(1177, 677);
            txtTotalRegistros.Name = "txtTotalRegistros";
            txtTotalRegistros.ReadOnly = true;
            txtTotalRegistros.Size = new Size(144, 28);
            txtTotalRegistros.TabIndex = 11;
            txtTotalRegistros.Text = "0";
            txtTotalRegistros.Visible = false;
            // 
            // txtTotalPaginas
            // 
            txtTotalPaginas.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            txtTotalPaginas.Location = new Point(1177, 716);
            txtTotalPaginas.Name = "txtTotalPaginas";
            txtTotalPaginas.ReadOnly = true;
            txtTotalPaginas.Size = new Size(144, 28);
            txtTotalPaginas.TabIndex = 13;
            txtTotalPaginas.Text = "0";
            txtTotalPaginas.Visible = false;
            // 
            // lblTotalPaginas
            // 
            lblTotalPaginas.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblTotalPaginas.AutoSize = true;
            lblTotalPaginas.Location = new Point(1009, 719);
            lblTotalPaginas.Name = "lblTotalPaginas";
            lblTotalPaginas.Size = new Size(149, 21);
            lblTotalPaginas.TabIndex = 12;
            lblTotalPaginas.Text = "Total de Páginas";
            lblTotalPaginas.Visible = false;
            // 
            // cmbCantidadRegistrosXPagina
            // 
            cmbCantidadRegistrosXPagina.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cmbCantidadRegistrosXPagina.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCantidadRegistrosXPagina.Enabled = false;
            cmbCantidadRegistrosXPagina.FormattingEnabled = true;
            cmbCantidadRegistrosXPagina.Items.AddRange(new object[] { "2", "3", "5", "10", "15", "20", "25" });
            cmbCantidadRegistrosXPagina.Location = new Point(892, 677);
            cmbCantidadRegistrosXPagina.Name = "cmbCantidadRegistrosXPagina";
            cmbCantidadRegistrosXPagina.Size = new Size(93, 29);
            cmbCantidadRegistrosXPagina.TabIndex = 14;
            cmbCantidadRegistrosXPagina.Visible = false;
            cmbCantidadRegistrosXPagina.SelectedIndexChanged += cmbCantidadRegistrosXPagina_SelectedIndexChanged;
            // 
            // btnInicio
            // 
            btnInicio.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnInicio.Location = new Point(1384, 677);
            btnInicio.Name = "btnInicio";
            btnInicio.Size = new Size(83, 30);
            btnInicio.TabIndex = 15;
            btnInicio.Text = "Inicio";
            btnInicio.UseVisualStyleBackColor = true;
            btnInicio.Visible = false;
            btnInicio.Click += btnInicio_Click;
            // 
            // btnFin
            // 
            btnFin.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnFin.Location = new Point(1624, 677);
            btnFin.Name = "btnFin";
            btnFin.Size = new Size(80, 30);
            btnFin.TabIndex = 16;
            btnFin.Text = "Fin";
            btnFin.UseVisualStyleBackColor = true;
            btnFin.Visible = false;
            btnFin.Click += btnFin_Click;
            // 
            // tvCategorias
            // 
            tvCategorias.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            tvCategorias.Font = new Font("Century Gothic", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tvCategorias.ForeColor = Color.FromArgb(39, 76, 119);
            tvCategorias.Location = new Point(16, 93);
            tvCategorias.Name = "tvCategorias";
            tvCategorias.Size = new Size(382, 577);
            tvCategorias.TabIndex = 17;
            tvCategorias.AfterSelect += tvCategorias_AfterSelect;
            // 
            // lblBusqueda
            // 
            lblBusqueda.AutoSize = true;
            lblBusqueda.Font = new Font("Century Gothic", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBusqueda.Location = new Point(409, 37);
            lblBusqueda.Name = "lblBusqueda";
            lblBusqueda.Size = new Size(64, 19);
            lblBusqueda.TabIndex = 18;
            lblBusqueda.Text = "Buscar";
            // 
            // txtBusqueda
            // 
            txtBusqueda.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBusqueda.Location = new Point(481, 33);
            txtBusqueda.Name = "txtBusqueda";
            txtBusqueda.Size = new Size(986, 28);
            txtBusqueda.TabIndex = 19;
            // 
            // btnBuscar
            // 
            btnBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBuscar.Font = new Font("Century Gothic", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBuscar.Location = new Point(1477, 33);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(229, 30);
            btnBuscar.TabIndex = 20;
            btnBuscar.Text = "Iniciar Búsqueda";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblStatus.AutoSize = true;
            lblStatus.BackColor = Color.FromArgb(255, 255, 192);
            lblStatus.Font = new Font("Century Gothic", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStatus.ForeColor = Color.Black;
            lblStatus.Location = new Point(481, 64);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(74, 19);
            lblStatus.TabIndex = 21;
            lblStatus.Text = "lblStatus";
            lblStatus.Visible = false;
            // 
            // ControlArchivos
            // 
            AutoScaleDimensions = new SizeF(10F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1743, 769);
            Controls.Add(lblStatus);
            Controls.Add(btnBuscar);
            Controls.Add(txtBusqueda);
            Controls.Add(lblBusqueda);
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
            Font = new Font("Century Gothic", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "ControlArchivos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Control Archivos";
            WindowState = FormWindowState.Maximized;
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
        private Label lblBusqueda;
        private TextBox txtBusqueda;
        private Button btnBuscar;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn Nombre;
        private DataGridViewTextBoxColumn Observaciones;
        private DataGridViewTextBoxColumn Paginas;
        private Label lblStatus;
    }
}