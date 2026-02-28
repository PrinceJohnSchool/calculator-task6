using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CalculatorApp
{
    public partial class HistoryForm : Form
    {
        private ListBox historyListBox;
        private Button clearHistoryButton;
        private Button closeButton;
        private Label titleLabel;
        private List<string> calculationHistory;

        public HistoryForm(List<string> history)
        {
            calculationHistory = history ?? new List<string>();
            InitializeComponent();
            LoadHistory();
        }

        private void InitializeComponent()
        {
            this.Text = "Calculation History";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.LightGray;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            titleLabel = new Label();
            titleLabel.Text = "Calculation History";
            titleLabel.Location = new Point(50, 10);
            titleLabel.Size = new Size(400, 30);
            titleLabel.Font = new Font("Arial", 16, FontStyle.Bold);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(titleLabel);

            historyListBox = new ListBox();
            historyListBox.Location = new Point(50, 50);
            historyListBox.Size = new Size(400, 250);
            historyListBox.Font = new Font("Arial", 10);
            this.Controls.Add(historyListBox);

            clearHistoryButton = new Button();
            clearHistoryButton.Text = "Clear History";
            clearHistoryButton.Location = new Point(50, 310);
            clearHistoryButton.Size = new Size(150, 35);
            clearHistoryButton.Font = new Font("Arial", 11, FontStyle.Bold);
            clearHistoryButton.BackColor = Color.LightCoral;
            clearHistoryButton.ForeColor = Color.DarkRed;
            clearHistoryButton.Click += ClearHistoryButton_Click;
            this.Controls.Add(clearHistoryButton);

            closeButton = new Button();
            closeButton.Text = "Close";
            closeButton.Location = new Point(300, 310);
            closeButton.Size = new Size(150, 35);
            closeButton.Font = new Font("Arial", 11, FontStyle.Bold);
            closeButton.BackColor = Color.LightBlue;
            closeButton.ForeColor = Color.DarkBlue;
            closeButton.Click += CloseButton_Click;
            this.Controls.Add(closeButton);
        }

        private void LoadHistory()
        {
            RefreshHistoryDisplay();
        }

        private void ClearHistoryButton_Click(object sender, EventArgs e)
        {
            ClearHistoryList();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            CloseHistoryForm();
        }

        private void ClearHistoryList()
        {
            calculationHistory.Clear();
            RefreshHistoryDisplay();
        }

        private void CloseHistoryForm()
        {
            this.Close();
        }

        private void RefreshHistoryDisplay()
        {
            historyListBox.Items.Clear();
            for (int i = 0; i < calculationHistory.Count; i++)
            {
                historyListBox.Items.Add($"{i + 1}. {calculationHistory[i]}");
            }
        }
    }
}
