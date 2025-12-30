namespace Car_Rental_Management_System.Forms
{
    partial class MaintenanceForm
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
            dgvMaintenances = new DataGridView();
            btnAddMaintenance = new FontAwesome.Sharp.IconButton();
            btnEditMaintenance = new FontAwesome.Sharp.IconButton();
            btnDeleteMaintenance = new FontAwesome.Sharp.IconButton();
            btnCompleteMaintenance = new FontAwesome.Sharp.IconButton();
            btnStartMaintenance = new FontAwesome.Sharp.IconButton();
            btnCancelMaintenance = new FontAwesome.Sharp.IconButton();
            txtSearch = new TextBox();
            cmbStatusFilter = new ComboBox();
            lblStatusFilter = new Label();
            statusStrip = new StatusStrip();
            lblStatusBar = new ToolStripStatusLabel();
            panelDetails = new Panel();
            txtNotes = new RichTextBox();
            txtMechanicPhone = new TextBox();
            txtMechanicName = new TextBox();
            txtCost = new TextBox();
            txtMileage = new TextBox();
            txtDescription = new TextBox();
            cmbMaintenanceType = new ComboBox();
            dtpScheduledDate = new DateTimePicker();
            cmbVehicle = new ComboBox();
            lblNotes = new Label();
            lblMechanicPhone = new Label();
            lblMechanicName = new Label();
            lblCost = new Label();
            lblMileage = new Label();
            lblDescription = new Label();
            lblMaintenanceType = new Label();
            lblScheduledDate = new Label();
            lblVehicle = new Label();
            btnSave = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)dgvMaintenances).BeginInit();
            statusStrip.SuspendLayout();
            panelDetails.SuspendLayout();
            SuspendLayout();
            // 
            // dgvMaintenances
            // 
            dgvMaintenances.AllowUserToAddRows = false;
            dgvMaintenances.AllowUserToDeleteRows = false;
            dgvMaintenances.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvMaintenances.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMaintenances.BackgroundColor = Color.FromArgb(45, 45, 65);
            dgvMaintenances.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMaintenances.Location = new Point(883, 120);
            dgvMaintenances.Name = "dgvMaintenances";
            dgvMaintenances.ReadOnly = true;
            dgvMaintenances.RowHeadersWidth = 62;
            dgvMaintenances.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMaintenances.Size = new Size(1284, 944);
            dgvMaintenances.TabIndex = 0;
            dgvMaintenances.CellContentClick += dgvMaintenances_CellContentClick;
            // 
            // btnAddMaintenance
            // 
            btnAddMaintenance.Anchor = AnchorStyles.Bottom;
            btnAddMaintenance.BackColor = Color.RoyalBlue;
            btnAddMaintenance.FlatStyle = FlatStyle.Flat;
            btnAddMaintenance.Font = new Font("Segoe UI", 9F);
            btnAddMaintenance.ForeColor = Color.White;
            btnAddMaintenance.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;
            btnAddMaintenance.IconColor = Color.White;
            btnAddMaintenance.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnAddMaintenance.IconSize = 25;
            btnAddMaintenance.Location = new Point(626, 1085);
            btnAddMaintenance.Name = "btnAddMaintenance";
            btnAddMaintenance.Size = new Size(150, 50);
            btnAddMaintenance.TabIndex = 1;
            btnAddMaintenance.Text = "Add New";
            btnAddMaintenance.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAddMaintenance.UseVisualStyleBackColor = false;
            btnAddMaintenance.Click += btnAddMaintenance_Click_1;
            // 
            // btnEditMaintenance
            // 
            btnEditMaintenance.Anchor = AnchorStyles.Bottom;
            btnEditMaintenance.BackColor = Color.MediumSeaGreen;
            btnEditMaintenance.FlatStyle = FlatStyle.Flat;
            btnEditMaintenance.Font = new Font("Segoe UI", 9F);
            btnEditMaintenance.ForeColor = Color.White;
            btnEditMaintenance.IconChar = FontAwesome.Sharp.IconChar.Edit;
            btnEditMaintenance.IconColor = Color.White;
            btnEditMaintenance.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnEditMaintenance.IconSize = 25;
            btnEditMaintenance.Location = new Point(792, 1085);
            btnEditMaintenance.Name = "btnEditMaintenance";
            btnEditMaintenance.Size = new Size(150, 50);
            btnEditMaintenance.TabIndex = 2;
            btnEditMaintenance.Text = "Edit";
            btnEditMaintenance.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEditMaintenance.UseVisualStyleBackColor = false;
            btnEditMaintenance.Click += btnEditMaintenance_Click_1;
            // 
            // btnDeleteMaintenance
            // 
            btnDeleteMaintenance.Anchor = AnchorStyles.Bottom;
            btnDeleteMaintenance.BackColor = Color.IndianRed;
            btnDeleteMaintenance.FlatStyle = FlatStyle.Flat;
            btnDeleteMaintenance.Font = new Font("Segoe UI", 9F);
            btnDeleteMaintenance.ForeColor = Color.White;
            btnDeleteMaintenance.IconChar = FontAwesome.Sharp.IconChar.TrashAlt;
            btnDeleteMaintenance.IconColor = Color.White;
            btnDeleteMaintenance.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnDeleteMaintenance.IconSize = 25;
            btnDeleteMaintenance.Location = new Point(957, 1085);
            btnDeleteMaintenance.Name = "btnDeleteMaintenance";
            btnDeleteMaintenance.Size = new Size(150, 50);
            btnDeleteMaintenance.TabIndex = 3;
            btnDeleteMaintenance.Text = "Delete";
            btnDeleteMaintenance.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDeleteMaintenance.UseVisualStyleBackColor = false;
            btnDeleteMaintenance.Click += btnDeleteMaintenance_Click_1;
            // 
            // btnCompleteMaintenance
            // 
            btnCompleteMaintenance.Anchor = AnchorStyles.Bottom;
            btnCompleteMaintenance.BackColor = Color.MediumSeaGreen;
            btnCompleteMaintenance.FlatStyle = FlatStyle.Flat;
            btnCompleteMaintenance.Font = new Font("Segoe UI", 9F);
            btnCompleteMaintenance.ForeColor = Color.White;
            btnCompleteMaintenance.IconChar = FontAwesome.Sharp.IconChar.CheckCircle;
            btnCompleteMaintenance.IconColor = Color.White;
            btnCompleteMaintenance.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCompleteMaintenance.IconSize = 25;
            btnCompleteMaintenance.Location = new Point(1118, 1085);
            btnCompleteMaintenance.Name = "btnCompleteMaintenance";
            btnCompleteMaintenance.Size = new Size(169, 50);
            btnCompleteMaintenance.TabIndex = 5;
            btnCompleteMaintenance.Text = "Complete";
            btnCompleteMaintenance.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCompleteMaintenance.UseVisualStyleBackColor = false;
            btnCompleteMaintenance.Click += btnCompleteMaintenance_Click_1;
            // 
            // btnStartMaintenance
            // 
            btnStartMaintenance.Anchor = AnchorStyles.Bottom;
            btnStartMaintenance.BackColor = Color.Orange;
            btnStartMaintenance.FlatStyle = FlatStyle.Flat;
            btnStartMaintenance.Font = new Font("Segoe UI", 9F);
            btnStartMaintenance.ForeColor = Color.White;
            btnStartMaintenance.IconChar = FontAwesome.Sharp.IconChar.PlayCircle;
            btnStartMaintenance.IconColor = Color.White;
            btnStartMaintenance.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnStartMaintenance.IconSize = 25;
            btnStartMaintenance.Location = new Point(1301, 1085);
            btnStartMaintenance.Name = "btnStartMaintenance";
            btnStartMaintenance.Size = new Size(150, 50);
            btnStartMaintenance.TabIndex = 6;
            btnStartMaintenance.Text = "Start";
            btnStartMaintenance.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnStartMaintenance.UseVisualStyleBackColor = false;
            btnStartMaintenance.Click += btnStartMaintenance_Click_1;
            // 
            // btnCancelMaintenance
            // 
            btnCancelMaintenance.Anchor = AnchorStyles.Bottom;
            btnCancelMaintenance.BackColor = Color.Crimson;
            btnCancelMaintenance.FlatStyle = FlatStyle.Flat;
            btnCancelMaintenance.Font = new Font("Segoe UI", 9F);
            btnCancelMaintenance.ForeColor = Color.White;
            btnCancelMaintenance.IconChar = FontAwesome.Sharp.IconChar.XmarkCircle;
            btnCancelMaintenance.IconColor = Color.White;
            btnCancelMaintenance.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelMaintenance.IconSize = 25;
            btnCancelMaintenance.Location = new Point(1466, 1085);
            btnCancelMaintenance.Name = "btnCancelMaintenance";
            btnCancelMaintenance.Size = new Size(150, 50);
            btnCancelMaintenance.TabIndex = 7;
            btnCancelMaintenance.Text = "Cancel";
            btnCancelMaintenance.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCancelMaintenance.UseVisualStyleBackColor = false;
            btnCancelMaintenance.Click += btnCancelMaintenance_Click_1;
            // 
            // txtSearch
            // 
            txtSearch.BackColor = Color.FromArgb(45, 45, 65);
            txtSearch.Font = new Font("Segoe UI", 10F);
            txtSearch.ForeColor = Color.White;
            txtSearch.Location = new Point(883, 50);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search by vehicle plate, description, or mechanic...";
            txtSearch.Size = new Size(715, 43);
            txtSearch.TabIndex = 8;
            txtSearch.TextChanged += txtSearch_TextChanged_1;
            // 
            // cmbStatusFilter
            // 
            cmbStatusFilter.BackColor = Color.FromArgb(45, 45, 65);
            cmbStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatusFilter.FlatStyle = FlatStyle.Flat;
            cmbStatusFilter.Font = new Font("Segoe UI", 10F);
            cmbStatusFilter.ForeColor = Color.White;
            cmbStatusFilter.FormattingEnabled = true;
            cmbStatusFilter.Items.AddRange(new object[] { "All", "Scheduled", "In Progress", "Completed", "Cancelled" });
            cmbStatusFilter.Location = new Point(1917, 47);
            cmbStatusFilter.Name = "cmbStatusFilter";
            cmbStatusFilter.Size = new Size(250, 45);
            cmbStatusFilter.TabIndex = 10;
            cmbStatusFilter.SelectedIndexChanged += cmbStatusFilter_SelectedIndexChanged_1;
            // 
            // lblStatusFilter
            // 
            lblStatusFilter.AutoSize = true;
            lblStatusFilter.Font = new Font("Segoe UI", 10F);
            lblStatusFilter.ForeColor = Color.White;
            lblStatusFilter.Location = new Point(1817, 53);
            lblStatusFilter.Name = "lblStatusFilter";
            lblStatusFilter.Size = new Size(94, 37);
            lblStatusFilter.TabIndex = 11;
            lblStatusFilter.Text = "Status:";
            // 
            // statusStrip
            // 
            statusStrip.BackColor = Color.FromArgb(64, 64, 64);
            statusStrip.ImageScalingSize = new Size(24, 24);
            statusStrip.Items.AddRange(new ToolStripItem[] { lblStatusBar });
            statusStrip.Location = new Point(0, 1138);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(2179, 42);
            statusStrip.TabIndex = 12;
            statusStrip.Text = "statusStrip1";
            // 
            // lblStatusBar
            // 
            lblStatusBar.Name = "lblStatusBar";
            lblStatusBar.Size = new Size(78, 32);
            lblStatusBar.Text = "Ready";
            // 
            // panelDetails
            // 
            panelDetails.BackColor = Color.FromArgb(45, 45, 65);
            panelDetails.BorderStyle = BorderStyle.FixedSingle;
            panelDetails.Controls.Add(txtNotes);
            panelDetails.Controls.Add(txtMechanicPhone);
            panelDetails.Controls.Add(txtMechanicName);
            panelDetails.Controls.Add(txtCost);
            panelDetails.Controls.Add(txtMileage);
            panelDetails.Controls.Add(txtDescription);
            panelDetails.Controls.Add(cmbMaintenanceType);
            panelDetails.Controls.Add(dtpScheduledDate);
            panelDetails.Controls.Add(cmbVehicle);
            panelDetails.Controls.Add(lblNotes);
            panelDetails.Controls.Add(lblMechanicPhone);
            panelDetails.Controls.Add(lblMechanicName);
            panelDetails.Controls.Add(lblCost);
            panelDetails.Controls.Add(lblMileage);
            panelDetails.Controls.Add(lblDescription);
            panelDetails.Controls.Add(lblMaintenanceType);
            panelDetails.Controls.Add(lblScheduledDate);
            panelDetails.Controls.Add(lblVehicle);
            panelDetails.Controls.Add(btnSave);
            panelDetails.Location = new Point(88, 47);
            panelDetails.Name = "panelDetails";
            panelDetails.Size = new Size(776, 1017);
            panelDetails.TabIndex = 13;
            panelDetails.Visible = false;
            // 
            // txtNotes
            // 
            txtNotes.BackColor = Color.FromArgb(45, 45, 65);
            txtNotes.Font = new Font("Segoe UI", 9F);
            txtNotes.ForeColor = Color.White;
            txtNotes.Location = new Point(265, 565);
            txtNotes.Name = "txtNotes";
            txtNotes.Size = new Size(450, 80);
            txtNotes.TabIndex = 20;
            txtNotes.Text = "";
            // 
            // txtMechanicPhone
            // 
            txtMechanicPhone.BackColor = Color.FromArgb(45, 45, 65);
            txtMechanicPhone.Font = new Font("Segoe UI", 9F);
            txtMechanicPhone.ForeColor = Color.White;
            txtMechanicPhone.Location = new Point(265, 505);
            txtMechanicPhone.Name = "txtMechanicPhone";
            txtMechanicPhone.Size = new Size(450, 39);
            txtMechanicPhone.TabIndex = 19;
            // 
            // txtMechanicName
            // 
            txtMechanicName.BackColor = Color.FromArgb(45, 45, 65);
            txtMechanicName.Font = new Font("Segoe UI", 9F);
            txtMechanicName.ForeColor = Color.White;
            txtMechanicName.Location = new Point(265, 446);
            txtMechanicName.Name = "txtMechanicName";
            txtMechanicName.Size = new Size(450, 39);
            txtMechanicName.TabIndex = 18;
            // 
            // txtCost
            // 
            txtCost.BackColor = Color.FromArgb(45, 45, 65);
            txtCost.Font = new Font("Segoe UI", 9F);
            txtCost.ForeColor = Color.White;
            txtCost.Location = new Point(265, 385);
            txtCost.Name = "txtCost";
            txtCost.Size = new Size(450, 39);
            txtCost.TabIndex = 17;
            // 
            // txtMileage
            // 
            txtMileage.BackColor = Color.FromArgb(45, 45, 65);
            txtMileage.Font = new Font("Segoe UI", 9F);
            txtMileage.ForeColor = Color.White;
            txtMileage.Location = new Point(265, 327);
            txtMileage.Name = "txtMileage";
            txtMileage.Size = new Size(450, 39);
            txtMileage.TabIndex = 16;
            txtMileage.TextChanged += txtMileage_TextChanged;
            // 
            // txtDescription
            // 
            txtDescription.BackColor = Color.FromArgb(45, 45, 65);
            txtDescription.Font = new Font("Segoe UI", 9F);
            txtDescription.ForeColor = Color.White;
            txtDescription.Location = new Point(265, 266);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(450, 39);
            txtDescription.TabIndex = 15;
            // 
            // cmbMaintenanceType
            // 
            cmbMaintenanceType.BackColor = Color.FromArgb(45, 45, 65);
            cmbMaintenanceType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMaintenanceType.FlatStyle = FlatStyle.Flat;
            cmbMaintenanceType.Font = new Font("Segoe UI", 9F);
            cmbMaintenanceType.ForeColor = Color.White;
            cmbMaintenanceType.FormattingEnabled = true;
            cmbMaintenanceType.Items.AddRange(new object[] { "Regular Service", "Oil Change", "Brake Service", "Tire Replacement", "Engine Repair", "Accident Repair", "Electrical Repair", "Body Work", "Other" });
            cmbMaintenanceType.Location = new Point(265, 73);
            cmbMaintenanceType.Name = "cmbMaintenanceType";
            cmbMaintenanceType.Size = new Size(450, 40);
            cmbMaintenanceType.TabIndex = 14;
            // 
            // dtpScheduledDate
            // 
            dtpScheduledDate.CalendarMonthBackground = Color.FromArgb(45, 45, 65);
            dtpScheduledDate.CalendarTitleBackColor = Color.FromArgb(30, 30, 47);
            dtpScheduledDate.CalendarTitleForeColor = Color.White;
            dtpScheduledDate.CalendarTrailingForeColor = Color.Gray;
            dtpScheduledDate.Font = new Font("Segoe UI", 9F);
            dtpScheduledDate.Format = DateTimePickerFormat.Short;
            dtpScheduledDate.Location = new Point(265, 137);
            dtpScheduledDate.Name = "dtpScheduledDate";
            dtpScheduledDate.Size = new Size(450, 39);
            dtpScheduledDate.TabIndex = 13;
            // 
            // cmbVehicle
            // 
            cmbVehicle.BackColor = Color.FromArgb(45, 45, 65);
            cmbVehicle.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbVehicle.FlatStyle = FlatStyle.Flat;
            cmbVehicle.Font = new Font("Segoe UI", 9F);
            cmbVehicle.ForeColor = Color.White;
            cmbVehicle.FormattingEnabled = true;
            cmbVehicle.Location = new Point(265, 201);
            cmbVehicle.Name = "cmbVehicle";
            cmbVehicle.Size = new Size(450, 40);
            cmbVehicle.TabIndex = 12;
            // 
            // lblNotes
            // 
            lblNotes.AutoSize = true;
            lblNotes.Font = new Font("Segoe UI", 9F);
            lblNotes.ForeColor = Color.White;
            lblNotes.Location = new Point(50, 565);
            lblNotes.Name = "lblNotes";
            lblNotes.Size = new Size(82, 32);
            lblNotes.TabIndex = 10;
            lblNotes.Text = "Notes:";
            // 
            // lblMechanicPhone
            // 
            lblMechanicPhone.AutoSize = true;
            lblMechanicPhone.Font = new Font("Segoe UI", 9F);
            lblMechanicPhone.ForeColor = Color.White;
            lblMechanicPhone.Location = new Point(50, 505);
            lblMechanicPhone.Name = "lblMechanicPhone";
            lblMechanicPhone.Size = new Size(197, 32);
            lblMechanicPhone.TabIndex = 9;
            lblMechanicPhone.Text = "Mechanic Phone:";
            // 
            // lblMechanicName
            // 
            lblMechanicName.AutoSize = true;
            lblMechanicName.Font = new Font("Segoe UI", 9F);
            lblMechanicName.ForeColor = Color.White;
            lblMechanicName.Location = new Point(50, 446);
            lblMechanicName.Name = "lblMechanicName";
            lblMechanicName.Size = new Size(193, 32);
            lblMechanicName.TabIndex = 8;
            lblMechanicName.Text = "Mechanic Name:";
            // 
            // lblCost
            // 
            lblCost.AutoSize = true;
            lblCost.Font = new Font("Segoe UI", 9F);
            lblCost.ForeColor = Color.White;
            lblCost.Location = new Point(50, 385);
            lblCost.Name = "lblCost";
            lblCost.Size = new Size(66, 32);
            lblCost.TabIndex = 7;
            lblCost.Text = "Cost:";
            // 
            // lblMileage
            // 
            lblMileage.AutoSize = true;
            lblMileage.Font = new Font("Segoe UI", 9F);
            lblMileage.ForeColor = Color.White;
            lblMileage.Location = new Point(50, 327);
            lblMileage.Name = "lblMileage";
            lblMileage.Size = new Size(105, 32);
            lblMileage.TabIndex = 6;
            lblMileage.Text = "Mileage:";
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI", 9F);
            lblDescription.ForeColor = Color.White;
            lblDescription.Location = new Point(50, 266);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(140, 32);
            lblDescription.TabIndex = 5;
            lblDescription.Text = "Description:";
            // 
            // lblMaintenanceType
            // 
            lblMaintenanceType.AutoSize = true;
            lblMaintenanceType.Font = new Font("Segoe UI", 9F);
            lblMaintenanceType.ForeColor = Color.White;
            lblMaintenanceType.Location = new Point(50, 73);
            lblMaintenanceType.Name = "lblMaintenanceType";
            lblMaintenanceType.Size = new Size(70, 32);
            lblMaintenanceType.TabIndex = 4;
            lblMaintenanceType.Text = "Type:";
            // 
            // lblScheduledDate
            // 
            lblScheduledDate.AutoSize = true;
            lblScheduledDate.Font = new Font("Segoe UI", 9F);
            lblScheduledDate.ForeColor = Color.White;
            lblScheduledDate.Location = new Point(50, 137);
            lblScheduledDate.Name = "lblScheduledDate";
            lblScheduledDate.Size = new Size(131, 32);
            lblScheduledDate.TabIndex = 3;
            lblScheduledDate.Text = "Scheduled:";
            // 
            // lblVehicle
            // 
            lblVehicle.AutoSize = true;
            lblVehicle.Font = new Font("Segoe UI", 9F);
            lblVehicle.ForeColor = Color.White;
            lblVehicle.Location = new Point(50, 204);
            lblVehicle.Name = "lblVehicle";
            lblVehicle.Size = new Size(102, 32);
            lblVehicle.TabIndex = 2;
            lblVehicle.Text = "Vehicle: ";
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.MediumSeaGreen;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 9F);
            btnSave.ForeColor = Color.White;
            btnSave.IconChar = FontAwesome.Sharp.IconChar.Save;
            btnSave.IconColor = Color.White;
            btnSave.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnSave.IconSize = 25;
            btnSave.Location = new Point(565, 911);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(150, 50);
            btnSave.TabIndex = 1;
            btnSave.Text = "Save";
            btnSave.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click_1;
            // 
            // MaintenanceForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 47);
            ClientSize = new Size(2179, 1180);
            Controls.Add(panelDetails);
            Controls.Add(statusStrip);
            Controls.Add(lblStatusFilter);
            Controls.Add(cmbStatusFilter);
            Controls.Add(txtSearch);
            Controls.Add(btnCancelMaintenance);
            Controls.Add(btnStartMaintenance);
            Controls.Add(btnCompleteMaintenance);
            Controls.Add(btnDeleteMaintenance);
            Controls.Add(btnEditMaintenance);
            Controls.Add(btnAddMaintenance);
            Controls.Add(dgvMaintenances);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MaintenanceForm";
            Text = "MaintenanceForm";
            Load += MaintenanceForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvMaintenances).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            panelDetails.ResumeLayout(false);
            panelDetails.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvMaintenances;
        private FontAwesome.Sharp.IconButton btnAddMaintenance;
        private FontAwesome.Sharp.IconButton btnEditMaintenance;
        private FontAwesome.Sharp.IconButton btnDeleteMaintenance;
        private FontAwesome.Sharp.IconButton btnCompleteMaintenance;
        private FontAwesome.Sharp.IconButton btnStartMaintenance;
        private FontAwesome.Sharp.IconButton btnCancelMaintenance;
        private TextBox txtSearch;
        private ComboBox cmbStatusFilter;
        private Label lblStatusFilter;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblStatusBar;
        private Panel panelDetails;
        private RichTextBox txtNotes;
        private TextBox txtMechanicPhone;
        private TextBox txtMechanicName;
        private TextBox txtCost;
        private TextBox txtMileage;
        private TextBox txtDescription;
        private ComboBox cmbMaintenanceType;
        private DateTimePicker dtpScheduledDate;
        private ComboBox cmbVehicle;
        private Label lblNotes;
        private Label lblMechanicPhone;
        private Label lblMechanicName;
        private Label lblCost;
        private Label lblMileage;
        private Label lblDescription;
        private Label lblMaintenanceType;
        private Label lblScheduledDate;
        private Label lblVehicle;
        private FontAwesome.Sharp.IconButton btnSave;
    }
}