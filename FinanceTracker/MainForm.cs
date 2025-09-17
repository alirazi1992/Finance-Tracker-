using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FinanceTracker
{
    public partial class MainForm : Form
    {
        private readonly FinanceDb _db;

        public MainForm()
        {
            InitializeComponent();

            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string dir = Path.Combine(appData, "FinanceTracker");
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            string dbPath = Path.Combine(dir, "finance.db");

            _db = new FinanceDb(dbPath);
            _db.EnsureCreated();

            cmbCategory.Items.AddRange(new object[] { "Food", "Rent", "Transport", "Shopping", "Bills", "Other" });
            if (cmbCategory.Items.Count > 0) cmbCategory.SelectedIndex = 0;

            dtFrom.Value = DateTime.Today.AddMonths(-1);
            dtTo.Value = DateTime.Today.AddDays(1);

            RefreshHistory();
            RefreshDashboard();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DateTime date = dtDate.Value.Date;
            string cat = cmbCategory.SelectedItem.ToString();
            double amount = (double)numAmount.Value;
            string note = txtNote.Text.Trim();

            _db.AddTransaction(date, cat, amount, note);

            MessageBox.Show("Transaction added!", "Finance", MessageBoxButtons.OK, MessageBoxIcon.Information);

            numAmount.Value = 0;
            txtNote.Text = "";

            RefreshHistory();
            RefreshDashboard();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            RefreshHistory();
            RefreshDashboard();
        }

        private void RefreshHistory()
        {
            DataTable table = _db.GetTransactions(dtFrom.Value, dtTo.Value);
            grid.DataSource = table;
        }

        private void RefreshDashboard()
        {
            var totals = _db.GetCategoryTotals(dtFrom.Value, dtTo.Value);

            chartPie.Series.Clear();
            chartPie.ChartAreas.Clear();
            var pieArea = new ChartArea();
            chartPie.ChartAreas.Add(pieArea);

            var seriesPie = new Series("Spending");
            seriesPie.ChartType = SeriesChartType.Pie;
            chartPie.Series.Add(seriesPie);

            double totalSpent = 0;
            foreach (var t in totals)
            {
                totalSpent += t.Total;
                seriesPie.Points.AddXY(t.Category, t.Total);
            }

            lblSummary.Text = $"Total Spent: {totalSpent:C}";

            // Line chart (spending over time)
            chartLine.Series.Clear();
            chartLine.ChartAreas.Clear();
            var lineArea = new ChartArea();
            lineArea.AxisX.LabelStyle.Format = "MM-dd";
            chartLine.ChartAreas.Add(lineArea);

            var seriesLine = new Series("Spending Over Time");
            seriesLine.ChartType = SeriesChartType.Line;
            seriesLine.XValueType = ChartValueType.Date;
            chartLine.Series.Add(seriesLine);

            DataTable tx = _db.GetTransactions(dtFrom.Value, dtTo.Value);
            foreach (DataRow row in tx.Rows)
            {
                DateTime d = DateTime.Parse(row["date"].ToString());
                double amt = Convert.ToDouble(row["amount"]);
                seriesLine.Points.AddXY(d, amt);
            }
        }
    }
}
