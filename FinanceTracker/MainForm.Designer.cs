namespace FinanceTracker
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabAdd;
        private System.Windows.Forms.TabPage tabHistory;
        private System.Windows.Forms.TabPage tabDashboard;

        // Add tab
        private System.Windows.Forms.DateTimePicker dtDate;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.NumericUpDown numAmount;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Button btnAdd;

        // History tab
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.Button btnFilter;

        // Dashboard tab
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPie;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartLine;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabAdd = new System.Windows.Forms.TabPage();
            this.tabHistory = new System.Windows.Forms.TabPage();
            this.tabDashboard = new System.Windows.Forms.TabPage();

            // Add tab
            this.dtDate = new System.Windows.Forms.DateTimePicker();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.numAmount = new System.Windows.Forms.NumericUpDown();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();

            // History tab
            this.grid = new System.Windows.Forms.DataGridView();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.btnFilter = new System.Windows.Forms.Button();

            // Dashboard tab
            this.lblSummary = new System.Windows.Forms.Label();
            this.chartPie = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartLine = new System.Windows.Forms.DataVisualization.Charting.Chart();

            // ==== Form ====
            this.SuspendLayout();
            this.Text = "💰 Finance Tracker";
            this.ClientSize = new System.Drawing.Size(900, 600);

            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Controls.Add(this.tabAdd);
            this.tabs.Controls.Add(this.tabHistory);
            this.tabs.Controls.Add(this.tabDashboard);

            // ==== Add tab ====
            this.tabAdd.Text = "Add Transaction";
            this.tabAdd.Padding = new System.Windows.Forms.Padding(10);

            this.dtDate.Location = new System.Drawing.Point(24, 24);
            this.cmbCategory.Location = new System.Drawing.Point(24, 64);
            this.cmbCategory.Width = 200;
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.numAmount.Location = new System.Drawing.Point(24, 104);
            this.numAmount.Maximum = 1000000;
            this.numAmount.DecimalPlaces = 2;

            this.txtNote.Location = new System.Drawing.Point(24, 144);
            this.txtNote.Width = 300;

            this.btnAdd.Location = new System.Drawing.Point(24, 184);
            this.btnAdd.Text = "Add Transaction";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.tabAdd.Controls.Add(this.dtDate);
            this.tabAdd.Controls.Add(this.cmbCategory);
            this.tabAdd.Controls.Add(this.numAmount);
            this.tabAdd.Controls.Add(this.txtNote);
            this.tabAdd.Controls.Add(this.btnAdd);

            // ==== History tab ====
            this.tabHistory.Text = "History";
            this.tabHistory.Padding = new System.Windows.Forms.Padding(10);

            this.dtFrom.Location = new System.Drawing.Point(24, 16);
            this.dtTo.Location = new System.Drawing.Point(200, 16);
            this.btnFilter.Location = new System.Drawing.Point(400, 16);
            this.btnFilter.Text = "Apply";
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);

            this.grid.Location = new System.Drawing.Point(24, 56);
            this.grid.Size = new System.Drawing.Size(820, 480);
            this.grid.Anchor = System.Windows.Forms.AnchorStyles.Top |
                               System.Windows.Forms.AnchorStyles.Bottom |
                               System.Windows.Forms.AnchorStyles.Left |
                               System.Windows.Forms.AnchorStyles.Right;
            this.grid.ReadOnly = true;
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;

            this.tabHistory.Controls.Add(this.dtFrom);
            this.tabHistory.Controls.Add(this.dtTo);
            this.tabHistory.Controls.Add(this.btnFilter);
            this.tabHistory.Controls.Add(this.grid);

            // ==== Dashboard tab ====
            this.tabDashboard.Text = "Dashboard";
            this.tabDashboard.Padding = new System.Windows.Forms.Padding(10);

            this.lblSummary.Location = new System.Drawing.Point(24, 16);
            this.lblSummary.AutoSize = true;
            this.lblSummary.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);

            this.chartPie.Location = new System.Drawing.Point(24, 48);
            this.chartPie.Size = new System.Drawing.Size(350, 300);

            this.chartLine.Location = new System.Drawing.Point(400, 48);
            this.chartLine.Size = new System.Drawing.Size(450, 300);

            this.tabDashboard.Controls.Add(this.lblSummary);
            this.tabDashboard.Controls.Add(this.chartPie);
            this.tabDashboard.Controls.Add(this.chartLine);

            // ==== Add everything ====
            this.Controls.Add(this.tabs);
            this.ResumeLayout(false);
        }
    }
}
