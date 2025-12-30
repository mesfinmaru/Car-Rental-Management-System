using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Rental_Management_System.Forms
{
    public partial class ReportsForm : Form
    {
        private ApiClient _apiClient;
        private ReportParameters _currentParameters;
        private List<object> _currentReportData;

        public class ReportParameters
        {
            public string ReportType { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public string Status { get; set; }
            public string VehicleType { get; set; }
            public bool IncludeAllDates { get; set; }
        }

        public ReportsForm(ApiClient apiClient)
        {
            InitializeComponent();
            _apiClient = apiClient;
            _currentReportData = new List<object>();
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Set default dates
            dtpFrom.Value = DateTime.Now.AddMonths(-1);
            dtpTo.Value = DateTime.Now;

            // Set default values
            cmbReportType.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 0;
            cmbVehicleType.SelectedIndex = 0;

            // Set form colors
            SetFormColors();
        }

        private void SetFormColors()
        {
            // Set control colors to match dark theme
            rtbReport.BackColor = Color.FromArgb(45, 45, 65);
            rtbReport.ForeColor = Color.White;

            cmbReportType.BackColor = Color.FromArgb(45, 45, 65);
            cmbReportType.ForeColor = Color.White;

            cmbStatus.BackColor = Color.FromArgb(45, 45, 65);
            cmbStatus.ForeColor = Color.White;

            cmbVehicleType.BackColor = Color.FromArgb(45, 45, 65);
            cmbVehicleType.ForeColor = Color.White;

            dtpFrom.CalendarMonthBackground = Color.FromArgb(45, 45, 65);
            dtpFrom.CalendarTitleBackColor = Color.FromArgb(30, 30, 47);
            dtpFrom.CalendarTitleForeColor = Color.White;
            dtpFrom.CalendarTrailingForeColor = Color.Gray;

            dtpTo.CalendarMonthBackground = Color.FromArgb(45, 45, 65);
            dtpTo.CalendarTitleBackColor = Color.FromArgb(30, 30, 47);
            dtpTo.CalendarTitleForeColor = Color.White;
            dtpTo.CalendarTrailingForeColor = Color.Gray;
        }

        private void ReportsForm_Load(object sender, EventArgs e)
        {
            lblStatusBar.Text = "Ready to generate reports";
        }

        private async void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                // Get report parameters
                _currentParameters = new ReportParameters
                {
                    ReportType = cmbReportType.SelectedItem?.ToString() ?? "Customer Reports",
                    FromDate = cbAllDates.Checked ? DateTime.MinValue : dtpFrom.Value,
                    ToDate = cbAllDates.Checked ? DateTime.MaxValue : dtpTo.Value,
                    Status = cmbStatus.SelectedItem?.ToString() ?? "All",
                    VehicleType = cmbVehicleType.SelectedItem?.ToString() ?? "All",
                    IncludeAllDates = cbAllDates.Checked
                };

                await GenerateReportAsync(_currentParameters);

                lblStatusBar.Text = $"Report generated successfully: {_currentParameters.ReportType}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatusBar.Text = "Error generating report";
            }
        }

        private async Task GenerateReportAsync(ReportParameters parameters)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                rtbReport.Clear();

                // Add report header
                AppendReportHeader(parameters);

                // Generate report based on type
                switch (parameters.ReportType)
                {
                    case "Customer Reports":
                        await GenerateCustomerReportAsync(parameters);
                        break;
                    case "Rental Reports":
                        await GenerateRentalReportAsync(parameters);
                        break;
                    case "Vehicle Reports":
                        await GenerateVehicleReportAsync(parameters);
                        break;
                    case "Financial Reports":
                        await GenerateFinancialReportAsync(parameters);
                        break;
                    case "Overdue Rentals":
                        await GenerateOverdueReportAsync();
                        break;
                }

                // Add summary
                await AddReportSummary(parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void AppendReportHeader(ReportParameters parameters)
        {
            StringBuilder header = new StringBuilder();
            header.AppendLine($"=== {parameters.ReportType.ToUpper()} ===");

            if (!parameters.IncludeAllDates)
            {
                header.AppendLine($"Date Range: {parameters.FromDate:yyyy-MM-dd} to {parameters.ToDate:yyyy-MM-dd}");
            }
            else
            {
                header.AppendLine("Date Range: All Dates");
            }

            if (parameters.Status != "All")
                header.AppendLine($"Status Filter: {parameters.Status}");

            if (parameters.VehicleType != "All")
                header.AppendLine($"Vehicle Type: {parameters.VehicleType}");

            header.AppendLine($"Generated On: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            if (_apiClient.CurrentUser != null)
            {
                header.AppendLine($"Generated By: {_apiClient.CurrentUser.FullName} ({_apiClient.CurrentUser.Role})");
            }
            header.AppendLine(new string('=', 80));
            header.AppendLine();

            rtbReport.AppendText(header.ToString());
        }

        private async Task GenerateCustomerReportAsync(ReportParameters parameters)
        {
            try
            {
                // Use the API client method
                var customers = await _apiClient.GetCustomerReportAsync(
                    parameters.FromDate,
                    parameters.ToDate,
                    parameters.Status);

                if (customers == null || !customers.Any())
                {
                    rtbReport.AppendText("No customers found for the selected criteria.\n");
                    return;
                }

                _currentReportData = customers.Cast<object>().ToList();

                // Display header
                rtbReport.AppendText("CUSTOMER REPORT\n");
                rtbReport.AppendText(new string('-', 120) + "\n");
                rtbReport.AppendText($"{"ID",-6} {"Name",-25} {"Email",-30} {"Phone",-15} {"Reg Date",-12} {"Rentals",-8} {"Total Spent",-15} {"Status",-10}\n");
                rtbReport.AppendText(new string('-', 120) + "\n");

                // Display data
                foreach (var customer in customers.OrderByDescending(c => c.TotalSpent))
                {
                    string status = customer.IsActive ? "Active" : "Inactive";
                    rtbReport.AppendText($"{customer.CustomerId,-6} " +
                                       $"{customer.FullName,-25} " +
                                       $"{customer.Email,-30} " +
                                       $"{customer.Phone,-15} " +
                                       $"{customer.RegistrationDate:yyyy-MM-dd,-12} " +
                                       $"{customer.TotalRentals,-8} " +
                                       $"{customer.TotalSpent:ETB #,##0.0} " +
                                       $"{status,-10}\n");
                }
            }
            catch (Exception ex)
            {
                rtbReport.AppendText($"Error generating customer report: {ex.Message}\n");
            }
        }

        private async Task GenerateRentalReportAsync(ReportParameters parameters)
        {
            try
            {
                // Use the API client method
                var rentals = await _apiClient.GetRentalReportAsync(
                    parameters.FromDate,
                    parameters.ToDate,
                    parameters.Status,
                    parameters.VehicleType);

                if (rentals == null || !rentals.Any())
                {
                    rtbReport.AppendText("No rentals found for the selected criteria.\n");
                    return;
                }

                _currentReportData = rentals.Cast<object>().ToList();

                // Display header
                rtbReport.AppendText("RENTAL REPORT\n");
                rtbReport.AppendText(new string('-', 140) + "\n");
                rtbReport.AppendText($"{"ID",-6} {"Customer",-20} {"Vehicle",-25} {"Type",-10} {"Start Date",-12} " +
                                   $"{"End Date",-12} {"Days",-5} {"Amount",-12} {"Status",-10} {"Paid",-6}\n");
                rtbReport.AppendText(new string('-', 140) + "\n");

                // Display data
                foreach (var rental in rentals.OrderByDescending(r => r.StartDate))
                {
                    string paidStatus = rental.IsPaid ? "Yes" : "No";
                    rtbReport.AppendText($"{rental.RentalId,-6} " +
                                       $"{rental.CustomerName,-20} " +
                                       $"{rental.VehicleInfo,-25} " +
                                       $"{rental.VehicleType,-10} " +
                                       $"{rental.StartDate:yyyy-MM-dd,-12} " +
                                       $"{rental.EndDate:yyyy-MM-dd,-12} " +
                                       $"{rental.RentalDays,-5} " +
                                       $"{rental.TotalAmount:ETB #,##0.0} " +
                                       $"{rental.Status,-10} " +
                                       $"{paidStatus,-6}\n");
                }
            }
            catch (Exception ex)
            {
                rtbReport.AppendText($"Error generating rental report: {ex.Message}\n");
            }
        }

        private async Task GenerateVehicleReportAsync(ReportParameters parameters)
        {
            try
            {
                // Use the API client method
                var vehicles = await _apiClient.GetVehicleReportAsync(
                    parameters.VehicleType,
                    parameters.Status);

                if (vehicles == null || !vehicles.Any())
                {
                    rtbReport.AppendText("No vehicles found for the selected criteria.\n");
                    return;
                }

                _currentReportData = vehicles.Cast<object>().ToList();

                // Display header
                rtbReport.AppendText("VEHICLE REPORT\n");
                rtbReport.AppendText(new string('-', 120) + "\n");
                rtbReport.AppendText($"{"ID",-6} {"Plate",-12} {"Make/Model",-20} {"Type",-10} {"Year",-6} " +
                                   $"{"Color",-10} {"Rate",-10} {"Status",-12} {"Rentals",-8} {"Revenue",-12}\n");
                rtbReport.AppendText(new string('-', 120) + "\n");

                // Display data
                foreach (var vehicle in vehicles.OrderByDescending(v => v.TotalRevenue))
                {
                    string makeModel = $"{vehicle.Make} {vehicle.Model}";
                    rtbReport.AppendText($"{vehicle.VehicleId,-6} " +
                                       $"{vehicle.PlateNumber,-12} " +
                                       $"{makeModel,-20} " +
                                       $"{vehicle.VehicleType,-10} " +
                                       $"{vehicle.Year,-6} " +
                                       $"{vehicle.Color,-10} " +
                                       $"{vehicle.DailyRate:ETB #,##0.0} " +
                                       $"{vehicle.Status,-12} " +
                                       $"{vehicle.TimesRented,-8} " +
                                       $"{vehicle.TotalRevenue:ETB #,##0.0}\n");
                }
            }
            catch (Exception ex)
            {
                rtbReport.AppendText($"Error generating vehicle report: {ex.Message}\n");
            }
        }

        private async Task GenerateFinancialReportAsync(ReportParameters parameters)
        {
            try
            {
                // Use the API client method
                var financials = await _apiClient.GetFinancialReportAsync(
                    parameters.FromDate,
                    parameters.ToDate);

                if (financials == null || !financials.Any())
                {
                    rtbReport.AppendText("No financial records found for the selected criteria.\n");
                    return;
                }

                _currentReportData = financials.Cast<object>().ToList();

                // Display header
                rtbReport.AppendText("FINANCIAL REPORT (PAID TRANSACTIONS ONLY)\n");
                rtbReport.AppendText(new string('-', 110) + "\n");
                rtbReport.AppendText($"{"ID",-6} {"Date",-12} {"Customer",-20} {"Vehicle",-20} " +
                                   $"{"Amount",-12} {"Payment Method",-15} {"Transaction ID",-20}\n");
                rtbReport.AppendText(new string('-', 110) + "\n");

                // Display data
                decimal totalRevenue = 0;
                foreach (var finance in financials.OrderBy(f => f.RentalDate))
                {
                    totalRevenue += finance.Revenue;
                    rtbReport.AppendText($"{finance.RentalId,-6} " +
                                       $"{finance.RentalDate:yyyy-MM-dd,-12} " +
                                       $"{finance.CustomerName,-20} " +
                                       $"{finance.VehicleInfo,-20} " +
                                       $"{finance.Revenue:ETB #,##0.0} " +
                                       $"{finance.PaymentMethod,-15} " +
                                       $"{finance.TransactionId,-20}\n");
                }

                // Add summary
                rtbReport.AppendText("\n" + new string('=', 80) + "\n");
                rtbReport.AppendText($"TOTAL REVENUE: {totalRevenue:ETB #,##0.0}\n");
                rtbReport.AppendText($"TOTAL TRANSACTIONS: {financials.Count}\n");
                rtbReport.AppendText($"AVERAGE TRANSACTION: {(financials.Count > 0 ? totalRevenue / financials.Count : 0):ETB #,##0.0}\n");
            }
            catch (Exception ex)
            {
                rtbReport.AppendText($"Error generating financial report: {ex.Message}\n");
            }
        }

        private async Task GenerateOverdueReportAsync()
        {
            try
            {
                // Use the API client method
                var overdueRentals = await _apiClient.GetOverdueReportAsync();

                if (overdueRentals == null || !overdueRentals.Any())
                {
                    rtbReport.AppendText("No overdue rentals found.\n");
                    return;
                }

                _currentReportData = overdueRentals.Cast<object>().ToList();

                // Display header
                rtbReport.AppendText("OVERDUE RENTALS REPORT\n");
                rtbReport.AppendText(new string('-', 130) + "\n");
                rtbReport.AppendText($"{"ID",-6} {"Customer",-20} {"Phone",-15} {"Email",-25} " +
                                   $"{"Vehicle",-20} {"Due Date",-12} {"Days",-5} {"Amount",-12} {"Late Fee",-10}\n");
                rtbReport.AppendText(new string('-', 130) + "\n");

                // Display data
                int totalOverdue = 0;
                decimal totalLateFees = 0;
                decimal totalAmount = 0;

                foreach (var overdue in overdueRentals.OrderByDescending(o => o.DaysOverdue))
                {
                    totalOverdue++;
                    totalAmount += overdue.TotalAmount;
                    totalLateFees += overdue.LateFee ?? 0;

                    rtbReport.AppendText($"{overdue.RentalId,-6} " +
                                       $"{overdue.CustomerName,-20} " +
                                       $"{overdue.Phone,-15} " +
                                       $"{overdue.Email,-25} " +
                                       $"{overdue.VehicleInfo,-20} " +
                                       $"{overdue.EndDate:yyyy-MM-dd,-12} " +
                                       $"{overdue.DaysOverdue,-5} " +
                                       $"{overdue.TotalAmount:ETB #,##0.0} " +
                                       $"{(overdue.LateFee?.ToString("ETB #,##0.0") ?? "N/A"),-10}\n");
                }

                // Add summary
                rtbReport.AppendText("\n" + new string('=', 80) + "\n");
                rtbReport.AppendText($"TOTAL OVERDUE RENTALS: {totalOverdue}\n");
                rtbReport.AppendText($"MAXIMUM DAYS OVERDUE: {overdueRentals.Max(o => o.DaysOverdue)} days\n");
                rtbReport.AppendText($"TOTAL RENTAL AMOUNT: {totalAmount:ETB #,##0.0}\n");
                rtbReport.AppendText($"TOTAL POTENTIAL LATE FEES: {totalLateFees:ETB #,##0.0}\n");
                rtbReport.AppendText($"TOTAL POTENTIAL REVENUE: {(totalAmount + totalLateFees):ETB #,##0.0}\n");
            }
            catch (Exception ex)
            {
                rtbReport.AppendText($"Error generating overdue report: {ex.Message}\n");
            }
        }

        private async Task AddReportSummary(ReportParameters parameters)
        {
            try
            {
                var summary = await _apiClient.GetReportSummaryAsync(
                    parameters.ReportType,
                    parameters.FromDate,
                    parameters.ToDate);

                if (summary != null)
                {
                    rtbReport.AppendText("\n" + new string('=', 80) + "\n");
                    rtbReport.AppendText("REPORT SUMMARY\n");
                    rtbReport.AppendText(new string('-', 40) + "\n");
                    rtbReport.AppendText($"Total Records: {_currentReportData.Count}\n");
                    rtbReport.AppendText($"Total Revenue: {summary.TotalRevenue:ETB #,##0.0}\n");
                    rtbReport.AppendText($"Active Rentals: {summary.ActiveRentals}\n");
                    rtbReport.AppendText($"Overdue Rentals: {summary.OverdueRentals}\n");
                    rtbReport.AppendText($"Total Customers: {summary.TotalCustomers}\n");
                    rtbReport.AppendText($"Total Vehicles: {summary.TotalVehicles}\n");
                    rtbReport.AppendText($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n");
                }
                else if (_currentReportData.Count > 0)
                {
                    rtbReport.AppendText("\n" + new string('=', 80) + "\n");
                    rtbReport.AppendText("REPORT SUMMARY\n");
                    rtbReport.AppendText(new string('-', 40) + "\n");
                    rtbReport.AppendText($"Total Records: {_currentReportData.Count}\n");
                    rtbReport.AppendText($"Report Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n");
                    rtbReport.AppendText($"Generated By: {_apiClient.CurrentUser?.FullName ?? "Unknown"}\n");
                }
                else
                {
                    rtbReport.AppendText("\nNo data found for the selected criteria.\n");
                }
            }
            catch (Exception ex)
            {
                rtbReport.AppendText($"\nNote: Could not load detailed summary: {ex.Message}\n");
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentReportData.Count == 0)
                {
                    MessageBox.Show("No data to export. Please generate a report first.",
                        "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf|Text Files (*.txt)|*.txt|CSV Files (*.csv)|*.csv";
                saveFileDialog.FileName = $"{_currentParameters?.ReportType.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd_HHmmss}";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;

                    switch (saveFileDialog.FilterIndex)
                    {
                        case 1: // PDF
                            ExportToPDF(saveFileDialog.FileName);
                            break;
                        case 2: // TXT
                            ExportToText(saveFileDialog.FileName);
                            break;
                        case 3: // CSV
                            ExportToCSV(saveFileDialog.FileName);
                            break;
                    }

                    MessageBox.Show($"Report exported successfully to:\n{saveFileDialog.FileName}",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    lblStatusBar.Text = $"Report exported to: {Path.GetFileName(saveFileDialog.FileName)}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting report: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ExportToPDF(string filePath)
        {
            try
            {
                Document document = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10);
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));

                document.Open();

                // Add title
                iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.WHITE);
                Paragraph title = new Paragraph($"Car Rental Management System\n{_currentParameters?.ReportType}\n\n", titleFont);
                title.Alignment = Element.ALIGN_CENTER;

                // Create colored title cell
                PdfPTable titleTable = new PdfPTable(1);
                titleTable.WidthPercentage = 100;
                PdfPCell titleCell = new PdfPCell(title);
                titleCell.BackgroundColor = new BaseColor(30, 30, 47);
                titleCell.Border = 0;
                titleCell.Padding = 10;
                titleTable.AddCell(titleCell);
                document.Add(titleTable);

                // Add report details
                iTextSharp.text.Font normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK);
                document.Add(new Paragraph($"Date Range: {_currentParameters?.FromDate:yyyy-MM-dd} to {_currentParameters?.ToDate:yyyy-MM-dd}", normalFont));
                document.Add(new Paragraph($"Generated On: {DateTime.Now:yyyy-MM-dd HH:mm:ss}", normalFont));
                document.Add(new Paragraph($"Generated By: {_apiClient.CurrentUser?.FullName ?? "Unknown"}", normalFont));
                document.Add(new Paragraph("\n"));

                // Create PDF table from report text
                string[] lines = rtbReport.Text.Split('\n');
                PdfPTable table = new PdfPTable(1);
                table.WidthPercentage = 100;

                foreach (string line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(line, normalFont));
                        cell.Border = 0;
                        cell.Padding = 2;
                        table.AddCell(cell);
                    }
                }

                document.Add(table);
                document.Close();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating PDF: {ex.Message}");
            }
        }

        private void ExportToText(string filePath)
        {
            try
            {
                File.WriteAllText(filePath, rtbReport.Text);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving text file: {ex.Message}");
            }
        }

        private void ExportToCSV(string filePath)
        {
            try
            {
                // Extract data from rich text box and format as CSV
                StringBuilder csv = new StringBuilder();
                string[] lines = rtbReport.Text.Split('\n');

                foreach (string line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        // Escape quotes and wrap in quotes if contains comma
                        string escapedLine = line;
                        if (line.Contains(","))
                        {
                            escapedLine = "\"" + line.Replace("\"", "\"\"") + "\"";
                        }
                        csv.AppendLine(escapedLine);
                    }
                }

                File.WriteAllText(filePath, csv.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving CSV file: {ex.Message}");
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(rtbReport.Text))
                {
                    MessageBox.Show("No report to print. Please generate a report first.",
                        "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                PrintDialog printDialog = new PrintDialog();
                PrintDocument printDocument = new PrintDocument();

                printDocument.PrintPage += (s, ev) =>
                {
                    System.Drawing.Font font = new System.Drawing.Font("Consolas", 9);
                    float linesPerPage = ev.MarginBounds.Height / font.GetHeight(ev.Graphics);
                    int lineCount = 0;

                    foreach (string line in rtbReport.Lines)
                    {
                        if (lineCount >= linesPerPage)
                        {
                            ev.HasMorePages = true;
                            return;
                        }

                        ev.Graphics.DrawString(line, font, Brushes.Black,
                            ev.MarginBounds.Left,
                            ev.MarginBounds.Top + (lineCount * font.GetHeight(ev.Graphics)));

                        lineCount++;
                    }

                    ev.HasMorePages = false;
                };

                printDialog.Document = printDocument;

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    printDocument.Print();
                    lblStatusBar.Text = "Report printed successfully";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error printing report: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            rtbReport.Clear();
            _currentReportData.Clear();
            cmbReportType.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 0;
            cmbVehicleType.SelectedIndex = 0;
            dtpFrom.Value = DateTime.Now.AddMonths(-1);
            dtpTo.Value = DateTime.Now;
            cbAllDates.Checked = false;

            lblStatusBar.Text = "Cleared. Ready to generate new report.";
        }

        private void cbAllDates_CheckedChanged(object sender, EventArgs e)
        {
            dtpFrom.Enabled = !cbAllDates.Checked;
            dtpTo.Enabled = !cbAllDates.Checked;
        }

        private void rtbReport_TextChanged(object sender, EventArgs e)
        {
            // Update status bar with character count
            int charCount = rtbReport.Text.Length;
            int lineCount = rtbReport.Lines.Length;
            int wordCount = rtbReport.Text.Split(new[] { ' ', '\n', '\r', '\t' },
                StringSplitOptions.RemoveEmptyEntries).Length;

            lblStatusBar.Text = $"Characters: {charCount} | Words: {wordCount} | Lines: {lineCount}";
        }

        private void lblVehicleType_Click(object sender, EventArgs e)
        {

        }
    }
}