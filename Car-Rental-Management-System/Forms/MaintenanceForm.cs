using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRM_API.Models;
using System.Net.Http;

namespace Car_Rental_Management_System.Forms
{
    public partial class MaintenanceForm : Form
    {
        private ApiClient _apiClient;
        private List<MaintenanceVM> _maintenances = new List<MaintenanceVM>();
        private List<VehicleVM> _vehicles = new List<VehicleVM>();
        private bool _isEditMode = false;
        private int _currentMaintenanceId = 0;
      

        public MaintenanceForm(ApiClient apiClient)
        {
            InitializeComponent();
            _apiClient = apiClient;
            InitializeForm();
            ApplyTheme();
        }

        private void InitializeForm()
        {
            // Load maintenance types
            LoadMaintenanceTypes();

            // Load status filters
            LoadStatusFilters();

            // Set default date to tomorrow
            dtpScheduledDate.Value = DateTime.Today.AddDays(1);
            dtpScheduledDate.MinDate = DateTime.Today;

            // Setup DataGridView
            SetupDataGridView();

            // Load data
            _ = LoadMaintenancesAsync();
            _ = LoadVehiclesAsync();
        }

        private void ApplyTheme()
        {
            var bg = Color.FromArgb(26, 27, 39);
            var panelBg = Color.FromArgb(39, 40, 55);
            var primary = Color.FromArgb(124, 77, 255);
            var secondary = Color.FromArgb(83, 109, 254);
            var lightText = Color.FromArgb(230, 230, 235);

            this.BackColor = bg;

            // Search TextBox
            txtSearch.BackColor = panelBg;
            txtSearch.ForeColor = lightText;
            txtSearch.BorderStyle = BorderStyle.FixedSingle;

            // Buttons
            btnAddMaintenance.BackColor = primary;
            btnAddMaintenance.ForeColor = Color.White;
            btnAddMaintenance.FlatStyle = FlatStyle.Flat;
            btnAddMaintenance.FlatAppearance.BorderSize = 0;

            btnEditMaintenance.BackColor = secondary;
            btnEditMaintenance.ForeColor = Color.White;
            btnEditMaintenance.FlatStyle = FlatStyle.Flat;
            btnEditMaintenance.FlatAppearance.BorderSize = 0;

            btnDeleteMaintenance.BackColor = Color.FromArgb(255, 98, 70);
            btnDeleteMaintenance.ForeColor = Color.White;
            btnDeleteMaintenance.FlatStyle = FlatStyle.Flat;
            btnDeleteMaintenance.FlatAppearance.BorderSize = 0;

            btnStartMaintenance.BackColor = Color.FromArgb(46, 204, 113);
            btnStartMaintenance.ForeColor = Color.White;
            btnStartMaintenance.FlatStyle = FlatStyle.Flat;
            btnStartMaintenance.FlatAppearance.BorderSize = 0;

            btnCompleteMaintenance.BackColor = Color.FromArgb(52, 152, 219);
            btnCompleteMaintenance.ForeColor = Color.White;
            btnCompleteMaintenance.FlatStyle = FlatStyle.Flat;
            btnCompleteMaintenance.FlatAppearance.BorderSize = 0;

            btnCancelMaintenance.BackColor = Color.FromArgb(241, 196, 15);
            btnCancelMaintenance.ForeColor = Color.Black;
            btnCancelMaintenance.FlatStyle = FlatStyle.Flat;
            btnCancelMaintenance.FlatAppearance.BorderSize = 0;

            // DataGridView
            dgvMaintenances.BackgroundColor = Color.FromArgb(30, 30, 44);
            dgvMaintenances.DefaultCellStyle.BackColor = Color.FromArgb(30, 30, 44);
            dgvMaintenances.DefaultCellStyle.ForeColor = lightText;
            dgvMaintenances.DefaultCellStyle.SelectionBackColor = primary;
            dgvMaintenances.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvMaintenances.ColumnHeadersDefaultCellStyle.BackColor = primary;
            dgvMaintenances.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMaintenances.EnableHeadersVisualStyles = false;
            dgvMaintenances.GridColor = Color.FromArgb(55, 55, 70);
            dgvMaintenances.RowTemplate.Height = 36;
            dgvMaintenances.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(35, 35, 50);
            dgvMaintenances.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvMaintenances.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvMaintenances.ColumnHeadersHeight = 40;

            // Form buttons
            btnSave.BackColor = primary;
            btnSave.ForeColor = Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.FlatAppearance.BorderSize = 0;

        }

        private void SetupDataGridView()
        {
            dgvMaintenances.AutoGenerateColumns = false;
            dgvMaintenances.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMaintenances.MultiSelect = false;
            dgvMaintenances.ReadOnly = true;
            dgvMaintenances.AllowUserToAddRows = false;
            dgvMaintenances.RowHeadersVisible = false;

            dgvMaintenances.Columns.Clear();

            // Add columns
            dgvMaintenances.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colId",
                HeaderText = "ID",
                Width = 50
            });

            dgvMaintenances.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colVehicle",
                HeaderText = "Vehicle",
                Width = 150
            });

            dgvMaintenances.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colType",
                HeaderText = "Type",
                Width = 120
            });

            dgvMaintenances.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colDescription",
                HeaderText = "Description",
                Width = 200
            });

            dgvMaintenances.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colScheduled",
                HeaderText = "Scheduled Date",
                Width = 100
            });

            dgvMaintenances.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colStatus",
                HeaderText = "Status",
                Width = 100
            });

            dgvMaintenances.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colCost",
                HeaderText = "Cost",
                Width = 80
            });

            dgvMaintenances.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colMechanic",
                HeaderText = "Mechanic",
                Width = 120
            });

            dgvMaintenances.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colCreated",
                HeaderText = "Created",
                Width = 100
            });
        }

        private void LoadMaintenanceTypes()
        {
            cmbMaintenanceType.Items.Clear();
            cmbMaintenanceType.Items.AddRange(new string[]
            {
                "Regular Service",
                "Oil Change",
                "Brake Repair",
                "Tire Replacement",
                "Engine Repair",
                "Transmission Repair",
                "Electrical Repair",
                "AC Repair",
                "Body Repair",
                "Accident Repair",
                "Preventive Maintenance",
                "Emergency Repair"
            });
        }

        private void LoadStatusFilters()
        {
            cmbStatusFilter.Items.Clear();
            cmbStatusFilter.Items.Add("All");
            cmbStatusFilter.Items.Add("Scheduled");
            cmbStatusFilter.Items.Add("In Progress");
            cmbStatusFilter.Items.Add("Completed");
            cmbStatusFilter.Items.Add("Cancelled");
            cmbStatusFilter.SelectedIndex = 0;
        }

        private async Task LoadMaintenancesAsync()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                dgvMaintenances.Rows.Clear();
                lblStatusBar.Text = "Loading maintenance records...";

                if (_apiClient == null || !_apiClient.IsAuthenticated)
                {
                    lblStatusBar.Text = "Please login first";
                    ShowInfo("Please login to view maintenance records");
                    return;
                }

                var search = txtSearch.Text.Trim();
                var status = cmbStatusFilter.SelectedItem?.ToString();
                if (status == "All") status = null;

                _maintenances = await _apiClient.GetMaintenancesAsync(search, status);
                RefreshDataGridView();

                UpdateStatusLabel();
            }
            catch (HttpRequestException ex)
            {
                lblStatusBar.Text = "Connection failed";
                ShowError($"Cannot connect to server: {CleanErrorMessage(ex.Message)}");
            }
            catch (Exception ex)
            {
                lblStatusBar.Text = "Error loading";
                ShowError($"Failed to load maintenance records: {CleanErrorMessage(ex.Message)}");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async Task LoadVehiclesAsync()
        {
            try
            {
                if (_apiClient == null || !_apiClient.IsAuthenticated)
                    return;

                _vehicles = await _apiClient.GetVehiclesAsync();

                cmbVehicle.Items.Clear();
                cmbVehicle.Items.Add("-- Select Vehicle --");

                foreach (var vehicle in _vehicles.Where(v => v.IsActive && v.IsAvailable))
                {
                    cmbVehicle.Items.Add($"{vehicle.PlateNumber} - {vehicle.Make} {vehicle.Model}");
                }

                cmbVehicle.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading vehicles: {ex.Message}");
            }
        }

        private void RefreshDataGridView()
        {
            dgvMaintenances.Rows.Clear();

            foreach (var maintenance in _maintenances.OrderByDescending(m => m.ScheduledDate))
            {
                var vehicleInfo = $"{maintenance.VehiclePlateNumber} - {maintenance.VehicleMake} {maintenance.VehicleModel}";
                var rowIndex = dgvMaintenances.Rows.Add(
                    maintenance.Id,
                    vehicleInfo,
                    maintenance.MaintenanceType,
                    maintenance.Description.Length > 50 ? maintenance.Description.Substring(0, 50) + "..." : maintenance.Description,
                    maintenance.ScheduledDate.ToString("yyyy-MM-dd"),
                    maintenance.Status,
                    maintenance.Cost.ToString("ETB #,##0.0"),
                    maintenance.MechanicName,
                    maintenance.CreatedAt.ToString("yyyy-MM-dd")
                );

                // Color code rows based on status
                var row = dgvMaintenances.Rows[rowIndex];
                switch (maintenance.Status)
                {
                    case "Scheduled":
                        if (maintenance.IsOverdue)
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 204, 204); // Light red for overdue
                        else
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 204); // Light yellow
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        break;
                    case "In Progress":
                        row.DefaultCellStyle.BackColor = Color.FromArgb(204, 229, 255); // Light blue
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        break;
                    case "Completed":
                        row.DefaultCellStyle.BackColor = Color.FromArgb(204, 255, 204); // Light green
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        break;
                    case "Cancelled":
                        row.DefaultCellStyle.BackColor = Color.FromArgb(224, 224, 224); // Light gray
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        break;
                }
            }
        }

        private void UpdateStatusLabel()
        {
            int totalCount = _maintenances.Count;
            int scheduledCount = _maintenances.Count(m => m.Status == "Scheduled");
            int inProgressCount = _maintenances.Count(m => m.Status == "In Progress");
            int completedCount = _maintenances.Count(m => m.Status == "Completed");
            int cancelledCount = _maintenances.Count(m => m.Status == "Cancelled");

            lblStatusBar.Text = $"Total: {totalCount} | Scheduled: {scheduledCount} | In Progress: {inProgressCount} | Completed: {completedCount} | Cancelled: {cancelledCount}";
        }

        // ================= BUTTON EVENT HANDLERS =================

        private void btnAddMaintenance_Click(object sender, EventArgs e)
        {
           
        }

        private void btnEditMaintenance_Click(object sender, EventArgs e)
        {
            
        }

        private async void btnDeleteMaintenance_Click(object sender, EventArgs e)
        {
           
            
        }

        private async void btnStartMaintenance_Click(object sender, EventArgs e)
        {
          
        }

        private async void btnCompleteMaintenance_Click(object sender, EventArgs e)
        {
           
            
        }

        private async void btnCancelMaintenance_Click(object sender, EventArgs e)
        {
           
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
          
        }

        private async void txtSearch_TextChanged(object sender, EventArgs e)
        {
           
        }

        private async void cmbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }

        // ================= FORM METHODS =================

        private MaintenanceVM? GetSelectedMaintenance()
        {
            if (dgvMaintenances.SelectedRows.Count > 0)
            {
                var id = Convert.ToInt32(dgvMaintenances.SelectedRows[0].Cells["colId"].Value);
                return _maintenances.FirstOrDefault(m => m.Id == id);
            }
            return null;
        }

        private void ClearForm()
        {
            cmbVehicle.SelectedIndex = 0;
            cmbMaintenanceType.SelectedIndex = -1;
            txtDescription.Clear();
            dtpScheduledDate.Value = DateTime.Today.AddDays(1);
            txtMileage.Clear();
            txtCost.Clear();
            txtMechanicName.Clear();
            txtMechanicPhone.Clear();
            txtNotes.Clear();
        }

        private void LoadMaintenanceIntoForm(MaintenanceVM maintenance)
        {
            // Find vehicle in combobox
            var vehicleText = $"{maintenance.VehiclePlateNumber} - {maintenance.VehicleMake} {maintenance.VehicleModel}";

            for (int i = 0; i < cmbVehicle.Items.Count; i++)
            {
                if (cmbVehicle.Items[i].ToString().Contains(maintenance.VehiclePlateNumber))
                {
                    cmbVehicle.SelectedIndex = i;
                    break;
                }
            }

            cmbMaintenanceType.Text = maintenance.MaintenanceType;
            txtDescription.Text = maintenance.Description;
            dtpScheduledDate.Value = maintenance.ScheduledDate;
            txtMileage.Text = maintenance.CurrentMileage?.ToString() ?? "";
            txtCost.Text = maintenance.Cost.ToString("ETB #,##0.0");
            txtMechanicName.Text = maintenance.MechanicName;
            txtMechanicPhone.Text = maintenance.MechanicPhone ?? "";
            txtNotes.Text = maintenance.Notes ?? "";
        }

        private bool ValidateForm()
        {
            if (cmbVehicle.SelectedIndex <= 0)
            {
                ShowError("Please select a vehicle.");
                cmbVehicle.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cmbMaintenanceType.Text))
            {
                ShowError("Please select a maintenance type.");
                cmbMaintenanceType.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                ShowError("Please enter a description.");
                txtDescription.Focus();
                return false;
            }

            if (!int.TryParse(txtMileage.Text, out int mileage) || mileage < 0)
            {
                ShowError("Please enter a valid mileage (0 or greater).");
                txtMileage.Focus();
                return false;
            }

            if (!decimal.TryParse(txtCost.Text, out decimal cost) || cost < 0)
            {
                ShowError("Please enter a valid cost (0 or greater).");
                txtCost.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtMechanicName.Text))
            {
                ShowError("Please enter mechanic name.");
                txtMechanicName.Focus();
                return false;
            }

            return true;
        }

        private int GetVehicleIdFromText(string vehicleText)
        {
            if (string.IsNullOrEmpty(vehicleText) || vehicleText == "-- Select Vehicle --")
                return 0;

            var plateNumber = vehicleText.Split('-')[0].Trim();
            var vehicle = _vehicles.FirstOrDefault(v => v.PlateNumber == plateNumber);
            return vehicle?.Id ?? 0;
        }

        private async Task CreateMaintenanceAsync()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                lblStatusBar.Text = "Creating maintenance record...";

                var selectedVehicleText = cmbVehicle.SelectedItem?.ToString();
                var vehicleId = GetVehicleIdFromText(selectedVehicleText);

                if (vehicleId == 0)
                {
                    ShowError("Invalid vehicle selection.");
                    return;
                }

                var request = new MaintenanceRequest
                {
                    VehicleId = vehicleId,
                    MaintenanceType = cmbMaintenanceType.Text,
                    Description = txtDescription.Text,
                    ScheduledDate = dtpScheduledDate.Value,
                    CurrentMileage = int.Parse(txtMileage.Text),
                    Cost = decimal.Parse(txtCost.Text),
                    MechanicName = txtMechanicName.Text,
                    MechanicPhone = txtMechanicPhone.Text,
                    Notes = txtNotes.Text
                };

                await _apiClient.CreateMaintenanceAsync(request);

                panelDetails.Visible = false;
                lblStatusBar.Text = "Maintenance record created successfully";
                ShowSuccess("Maintenance record created successfully.");

                await LoadMaintenancesAsync();
                await LoadVehiclesAsync(); // Reload vehicles as availability changes
            }
            catch (Exception ex)
            {
                lblStatusBar.Text = "Creation failed";
                ShowError($"Error creating maintenance: {CleanErrorMessage(ex.Message)}");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async Task UpdateMaintenanceAsync()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                lblStatusBar.Text = "Updating maintenance record...";

                var selectedVehicleText = cmbVehicle.SelectedItem?.ToString();
                var vehicleId = GetVehicleIdFromText(selectedVehicleText);

                if (vehicleId == 0)
                {
                    ShowError("Invalid vehicle selection.");
                    return;
                }

                var maintenance = new MaintenanceVM
                {
                    Id = _currentMaintenanceId,
                    VehicleId = vehicleId,
                    MaintenanceType = cmbMaintenanceType.Text,
                    Description = txtDescription.Text,
                    ScheduledDate = dtpScheduledDate.Value,
                    CurrentMileage = int.Parse(txtMileage.Text),
                    Cost = decimal.Parse(txtCost.Text),
                    MechanicName = txtMechanicName.Text,
                    MechanicPhone = txtMechanicPhone.Text,
                    Notes = txtNotes.Text,
                    Status = "Scheduled"
                };

                await _apiClient.UpdateMaintenanceAsync(maintenance);

                panelDetails.Visible = false;
                lblStatusBar.Text = $"Maintenance record #{_currentMaintenanceId} updated successfully";
                ShowSuccess($"Maintenance record #{_currentMaintenanceId} updated successfully.");

                await LoadMaintenancesAsync();
            }
            catch (Exception ex)
            {
                lblStatusBar.Text = "Update failed";
                ShowError($"Error updating maintenance: {CleanErrorMessage(ex.Message)}");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void dgvMaintenances_SelectionChanged(object sender, EventArgs e)
        {
            var maintenance = GetSelectedMaintenance();
            if (maintenance != null)
            {
                lblStatusBar.Text = $"Selected: {maintenance.VehicleMake} {maintenance.VehicleModel} - {maintenance.MaintenanceType} ({maintenance.Status})";
            }

            UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            var maintenance = GetSelectedMaintenance();
            bool hasSelection = maintenance != null;

            btnEditMaintenance.Enabled = hasSelection;
            btnDeleteMaintenance.Enabled = hasSelection;
            btnStartMaintenance.Enabled = hasSelection && maintenance?.Status == "Scheduled";
            btnCompleteMaintenance.Enabled = hasSelection && maintenance?.Status == "In Progress";
            btnCancelMaintenance.Enabled = hasSelection && maintenance?.Status != "Completed" && maintenance?.Status != "Cancelled";
        }

        // ================= HELPER METHODS =================

        private static void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void ShowSuccess(string message)
        {
            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static void ShowInfo(string message)
        {
            MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string CleanErrorMessage(string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(errorMessage))
                return "Unknown error occurred";

            // Remove HTML tags
            errorMessage = System.Text.RegularExpressions.Regex.Replace(errorMessage, "<[^>]*>", " ");

            // Decode HTML entities
            errorMessage = System.Web.HttpUtility.HtmlDecode(errorMessage);

            // Remove common error prefixes
            string[] prefixes = {
                "API Error:", "HttpRequestException:", "System.Net.Http.HttpRequestException:",
                "BadRequest", "400", "404", "500", "StatusCode:"
            };

            foreach (var prefix in prefixes)
            {
                errorMessage = errorMessage.Replace(prefix, "").Trim();
            }

            // Clean up JSON parsing errors
            if (errorMessage.Contains("JSON tokens") || errorMessage.Contains("isFinalBlock") ||
                errorMessage.Contains("LineNumber: 0") || errorMessage.Contains("BytePositionInLine"))
            {
                return "Server returned an invalid response. Please check if the API endpoint is correct.";
            }

            // Clean up excessive whitespace
            errorMessage = System.Text.RegularExpressions.Regex.Replace(errorMessage, @"\s+", " ");

            // Limit length
            if (errorMessage.Length > 200)
                errorMessage = errorMessage.Substring(0, 197) + "...";

            return errorMessage.Trim();
        }

        private async void MaintenanceForm_Load(object sender, EventArgs e)
        {
            await LoadMaintenancesAsync();
        }

        private void MaintenanceForm_Shown(object sender, EventArgs e)
        {
            txtSearch.Focus();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                txtSearch.Text = "";
                txtSearch.Focus();
            }
        }

        private void txtMileage_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only numbers and control characters
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only numbers, decimal point, and control characters
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // Allow only one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtMechanicPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only numbers, plus sign, parentheses, dash, space, and control characters
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                e.KeyChar != '+' && e.KeyChar != '(' && e.KeyChar != ')' &&
                e.KeyChar != '-' && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            panelDetails.Visible = false;
            lblStatusBar.Text = "Ready";
        }

        private async void btnCancelMaintenance_Click_1(object sender, EventArgs e)
        {
            var maintenance = GetSelectedMaintenance();
            if (maintenance == null)
            {
                ShowInfo("Please select a maintenance record to cancel.");
                return;
            }

            if (maintenance.Status == "Completed")
            {
                ShowError("Cannot cancel completed maintenance records.");
                return;
            }

            // Ask for cancellation reason using a simple input dialog
            var reason = Microsoft.VisualBasic.Interaction.InputBox(
                "Please enter reason for cancellation:",
                "Cancel Maintenance",
                "",
                -1, -1);

            if (!string.IsNullOrEmpty(reason))
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    lblStatusBar.Text = "Cancelling maintenance...";
                    await _apiClient.CancelMaintenanceAsync(maintenance.Id, reason);

                    lblStatusBar.Text = "Maintenance cancelled!";
                    ShowSuccess($"Maintenance record #{maintenance.Id} cancelled successfully.");

                    await LoadMaintenancesAsync();
                }
                catch (Exception ex)
                {
                    lblStatusBar.Text = "Cancellation failed";
                    ShowError($"Failed to cancel maintenance: {CleanErrorMessage(ex.Message)}");
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        private void btnAddMaintenance_Click_1(object sender, EventArgs e)
        {
            if (_apiClient == null || !_apiClient.IsAuthenticated)
            {
                ShowError("Please login first");
                return;
            }

            _isEditMode = false;
            _currentMaintenanceId = 0;
            ClearForm();
            panelDetails.Visible = true;
            lblStatusBar.Text = "Adding new maintenance record";
            btnSave.Text = "Create";
        }

        private void btnEditMaintenance_Click_1(object sender, EventArgs e)
        {
            var maintenance = GetSelectedMaintenance();
            if (maintenance == null)
            {
                ShowInfo("Please select a maintenance record to edit.");
                return;
            }

            if (maintenance.Status == "Completed" || maintenance.Status == "In Progress")
            {
                ShowError($"Cannot edit {maintenance.Status} maintenance records.");
                return;
            }

            _isEditMode = true;
            _currentMaintenanceId = maintenance.Id;
            LoadMaintenanceIntoForm(maintenance);
            panelDetails.Visible = true;
            lblStatusBar.Text = $"Editing maintenance record #{maintenance.Id}";
            btnSave.Text = "Update";
        }

        private async void btnDeleteMaintenance_Click_1(object sender, EventArgs e)
        {
            var maintenance = GetSelectedMaintenance();
            if (maintenance == null)
            {
                ShowInfo("Please select a maintenance record to delete.");
                return;
            }

            if (maintenance.Status == "In Progress" || maintenance.Status == "Completed")
            {
                ShowError($"Cannot delete {maintenance.Status} maintenance records.");
                return;
            }

            var result = MessageBox.Show(
                $"Are you sure you want to delete maintenance record #{maintenance.Id}?\n\n" +
                $"Vehicle: {maintenance.VehicleMake} {maintenance.VehicleModel}\n" +
                $"Type: {maintenance.MaintenanceType}\n" +
                $"This action cannot be undone!",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    lblStatusBar.Text = "Deleting...";
                    await _apiClient.DeleteMaintenanceAsync(maintenance.Id);

                    lblStatusBar.Text = "Maintenance deleted!";
                    ShowSuccess($"Maintenance record #{maintenance.Id} has been deleted.");

                    await LoadMaintenancesAsync();
                }
                catch (Exception ex)
                {
                    lblStatusBar.Text = "Delete failed";
                    ShowError($"Failed to delete maintenance: {CleanErrorMessage(ex.Message)}");

                    await LoadMaintenancesAsync();
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            } }

        private void btnViewHistory_Click(object sender, EventArgs e)
        {
            var maintenance = GetSelectedMaintenance();
        }

        private async void btnCompleteMaintenance_Click_1(object sender, EventArgs e)
        {
            var maintenance = GetSelectedMaintenance();
            if (maintenance == null)
            {
                ShowInfo("Please select a maintenance record to complete.");
                return;
            }

            if (maintenance.Status != "In Progress")
            {
                ShowError("Only maintenance records in progress can be completed.");
                return;
            }

            // Create a simple input form using MessageBox for completion details
            using (var form = new Form()
            {
                Text = "Complete Maintenance",
                Size = new Size(400, 250),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            })
            {
                var lblCompletionDate = new Label { Text = "Completion Date:", Location = new Point(20, 20), Size = new Size(120, 25) };
                var dtpCompletionDate = new DateTimePicker { Location = new Point(150, 20), Size = new Size(200, 25), Value = DateTime.Today };

                var lblActualCost = new Label { Text = "Actual Cost :", Location = new Point(20, 60), Size = new Size(120, 25) };
                var txtActualCost = new TextBox { Location = new Point(150, 60), Size = new Size(200, 25), Text = maintenance.Cost.ToString("F2") };

                var lblNotes = new Label { Text = "Notes (Optional):", Location = new Point(20, 100), Size = new Size(120, 25) };
                var txtNotes = new TextBox { Location = new Point(150, 100), Size = new Size(200, 25) };

                var btnOK = new Button { Text = "OK", Location = new Point(150, 150), Size = new Size(80, 30), DialogResult = DialogResult.OK };
                var btnCancel = new Button { Text = "Cancel", Location = new Point(250, 150), Size = new Size(80, 30), DialogResult = DialogResult.Cancel };

                form.AcceptButton = btnOK;
                form.CancelButton = btnCancel;

                form.Controls.AddRange(new Control[] { lblCompletionDate, dtpCompletionDate, lblActualCost, txtActualCost, lblNotes, txtNotes, btnOK, btnCancel });

                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;
                        lblStatusBar.Text = "Completing maintenance...";

                        decimal? actualCost = null;
                        if (decimal.TryParse(txtActualCost.Text, out decimal cost) && cost > 0)
                        {
                            actualCost = cost;
                        }

                        await _apiClient.CompleteMaintenanceAsync(
                            maintenance.Id,
                            dtpCompletionDate.Value,
                            actualCost,
                            txtNotes.Text);

                        lblStatusBar.Text = "Maintenance completed!";
                        ShowSuccess($"Maintenance record #{maintenance.Id} completed successfully.");

                        await LoadMaintenancesAsync();
                    }
                    catch (Exception ex)
                    {
                        lblStatusBar.Text = "Completion failed";
                        ShowError($"Failed to complete maintenance: {CleanErrorMessage(ex.Message)}");
                    }
                    finally
                    {
                        Cursor = Cursors.Default;
                    }
                }
            }
            }

        private async void btnStartMaintenance_Click_1(object sender, EventArgs e)
        {
            var maintenance = GetSelectedMaintenance();
            if (maintenance == null)
            {
                ShowInfo("Please select a maintenance record to start.");
                return;
            }

            if (maintenance.Status != "Scheduled")
            {
                ShowError("Only scheduled maintenance records can be started.");
                return;
            }

            var result = MessageBox.Show(
                $"Start maintenance for vehicle: {maintenance.VehicleMake} {maintenance.VehicleModel}?\n\n" +
                $"Type: {maintenance.MaintenanceType}\n" +
                $"Description: {maintenance.Description}",
                "Confirm Start Maintenance",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    lblStatusBar.Text = "Starting maintenance...";
                    await _apiClient.StartMaintenanceAsync(maintenance.Id);

                    lblStatusBar.Text = "Maintenance started!";
                    ShowSuccess($"Maintenance record #{maintenance.Id} started successfully.");

                    await LoadMaintenancesAsync();
                }
                catch (Exception ex)
                {
                    lblStatusBar.Text = "Start failed";
                    ShowError($"Failed to start maintenance: {CleanErrorMessage(ex.Message)}");
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        private async void txtSearch_TextChanged_1(object sender, EventArgs e)
        {
            // Debounce search
            await Task.Delay(500);
            if (txtSearch.Focused)
                await LoadMaintenancesAsync();
        }

        private async void cmbStatusFilter_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            await LoadMaintenancesAsync();
        }

        private async void btnSave_Click_1(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            if (_isEditMode)
                await UpdateMaintenanceAsync();
            else
                await CreateMaintenanceAsync();
        }

        private void txtMileage_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvMaintenances_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}