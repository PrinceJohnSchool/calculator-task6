using System;
using System.Drawing;
using System.Windows.Forms;

namespace CalculatorApp
{
    public partial class SettingsForm : Form
    {
        private TextBox lastCalculationDateTextBox;
        private TextBox totalCalculationsTextBox;
        private TextBox maxHistoryEntriesTextBox;
        private TextBox firstNumberTextBox;
        private TextBox secondNumberTextBox;
        private TextBox lastResultTextBox;
        private Button saveButton;
        private Button cancelButton;
        private Label titleLabel;

        private DateTime lastCalculationDate;
        private int totalCalculations;
        private int maxHistoryEntries;
        private double firstNumber;
        private double secondNumber;
        private double lastResult;

        public SettingsForm(DateTime lastDate, int totalCalc, int maxEntries, double firstNum, double secondNum, double lastRes)
        {
            lastCalculationDate = lastDate;
            totalCalculations = totalCalc;
            maxHistoryEntries = maxEntries;
            firstNumber = firstNum;
            secondNumber = secondNum;
            lastResult = lastRes;
            InitializeComponent();
            LoadSettings();
        }

        private void InitializeComponent()
        {
            this.Text = "Edit Settings";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.LightGray;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            titleLabel = new Label();
            titleLabel.Text = "Calculator Settings Editor";
            titleLabel.Location = new Point(20, 10);
            titleLabel.Size = new Size(460, 30);
            titleLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(titleLabel);

            int yPos = 50;
            int labelWidth = 180;
            int textBoxWidth = 250;
            int spacing = 35;

            Label lastDateLabel = new Label();
            lastDateLabel.Text = "Last Calculation Date:";
            lastDateLabel.Location = new Point(20, yPos);
            lastDateLabel.Size = new Size(labelWidth, 25);
            lastDateLabel.Font = new Font("Arial", 10);
            this.Controls.Add(lastDateLabel);

            lastCalculationDateTextBox = new TextBox();
            lastCalculationDateTextBox.Location = new Point(210, yPos);
            lastCalculationDateTextBox.Size = new Size(textBoxWidth, 25);
            lastCalculationDateTextBox.Font = new Font("Arial", 10);
            this.Controls.Add(lastCalculationDateTextBox);
            yPos += spacing;

            Label totalCalcLabel = new Label();
            totalCalcLabel.Text = "Total Calculations:";
            totalCalcLabel.Location = new Point(20, yPos);
            totalCalcLabel.Size = new Size(labelWidth, 25);
            totalCalcLabel.Font = new Font("Arial", 10);
            this.Controls.Add(totalCalcLabel);

            totalCalculationsTextBox = new TextBox();
            totalCalculationsTextBox.Location = new Point(210, yPos);
            totalCalculationsTextBox.Size = new Size(textBoxWidth, 25);
            totalCalculationsTextBox.Font = new Font("Arial", 10);
            this.Controls.Add(totalCalculationsTextBox);
            yPos += spacing;

            Label maxEntriesLabel = new Label();
            maxEntriesLabel.Text = "Max History Entries:";
            maxEntriesLabel.Location = new Point(20, yPos);
            maxEntriesLabel.Size = new Size(labelWidth, 25);
            maxEntriesLabel.Font = new Font("Arial", 10);
            this.Controls.Add(maxEntriesLabel);

            maxHistoryEntriesTextBox = new TextBox();
            maxHistoryEntriesTextBox.Location = new Point(210, yPos);
            maxHistoryEntriesTextBox.Size = new Size(textBoxWidth, 25);
            maxHistoryEntriesTextBox.Font = new Font("Arial", 10);
            this.Controls.Add(maxHistoryEntriesTextBox);
            yPos += spacing;

            Label firstNumLabel = new Label();
            firstNumLabel.Text = "First Number:";
            firstNumLabel.Location = new Point(20, yPos);
            firstNumLabel.Size = new Size(labelWidth, 25);
            firstNumLabel.Font = new Font("Arial", 10);
            this.Controls.Add(firstNumLabel);

            firstNumberTextBox = new TextBox();
            firstNumberTextBox.Location = new Point(210, yPos);
            firstNumberTextBox.Size = new Size(textBoxWidth, 25);
            firstNumberTextBox.Font = new Font("Arial", 10);
            this.Controls.Add(firstNumberTextBox);
            yPos += spacing;

            Label secondNumLabel = new Label();
            secondNumLabel.Text = "Second Number:";
            secondNumLabel.Location = new Point(20, yPos);
            secondNumLabel.Size = new Size(labelWidth, 25);
            secondNumLabel.Font = new Font("Arial", 10);
            this.Controls.Add(secondNumLabel);

            secondNumberTextBox = new TextBox();
            secondNumberTextBox.Location = new Point(210, yPos);
            secondNumberTextBox.Size = new Size(textBoxWidth, 25);
            secondNumberTextBox.Font = new Font("Arial", 10);
            this.Controls.Add(secondNumberTextBox);
            yPos += spacing;

            Label lastResultLabel = new Label();
            lastResultLabel.Text = "Last Result:";
            lastResultLabel.Location = new Point(20, yPos);
            lastResultLabel.Size = new Size(labelWidth, 25);
            lastResultLabel.Font = new Font("Arial", 10);
            this.Controls.Add(lastResultLabel);

            lastResultTextBox = new TextBox();
            lastResultTextBox.Location = new Point(210, yPos);
            lastResultTextBox.Size = new Size(textBoxWidth, 25);
            lastResultTextBox.Font = new Font("Arial", 10);
            this.Controls.Add(lastResultTextBox);

            saveButton = new Button();
            saveButton.Text = "Save Changes";
            saveButton.Location = new Point(20, 320);
            saveButton.Size = new Size(200, 40);
            saveButton.Font = new Font("Arial", 11, FontStyle.Bold);
            saveButton.BackColor = Color.LightGreen;
            saveButton.ForeColor = Color.DarkGreen;
            saveButton.Click += SaveButton_Click;
            this.Controls.Add(saveButton);

            cancelButton = new Button();
            cancelButton.Text = "Cancel";
            cancelButton.Location = new Point(280, 320);
            cancelButton.Size = new Size(200, 40);
            cancelButton.Font = new Font("Arial", 11, FontStyle.Bold);
            cancelButton.BackColor = Color.LightCoral;
            cancelButton.ForeColor = Color.DarkRed;
            cancelButton.Click += CancelButton_Click;
            this.Controls.Add(cancelButton);
        }

        private void LoadSettings()
        {
            lastCalculationDateTextBox.Text = lastCalculationDate.ToString("yyyy-MM-dd HH:mm:ss");
            totalCalculationsTextBox.Text = totalCalculations.ToString();
            maxHistoryEntriesTextBox.Text = maxHistoryEntries.ToString();
            firstNumberTextBox.Text = firstNumber.ToString();
            secondNumberTextBox.Text = secondNumber.ToString();
            lastResultTextBox.Text = lastResult.ToString();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (DateTime.TryParse(lastCalculationDateTextBox.Text, out DateTime lastDate))
                {
                    lastCalculationDate = lastDate;
                }
                else
                {
                    MessageBox.Show("Invalid date format. Please use yyyy-MM-dd HH:mm:ss",
                        "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (int.TryParse(totalCalculationsTextBox.Text, out int totalCalc))
                {
                    totalCalculations = totalCalc;
                }
                else
                {
                    MessageBox.Show("Total Calculations must be a valid integer.",
                        "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (int.TryParse(maxHistoryEntriesTextBox.Text, out int maxEntries))
                {
                    maxHistoryEntries = maxEntries;
                }
                else
                {
                    MessageBox.Show("Max History Entries must be a valid integer.",
                        "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (double.TryParse(firstNumberTextBox.Text, out double firstNum))
                {
                    firstNumber = firstNum;
                }
                else
                {
                    MessageBox.Show("First Number must be a valid number.",
                        "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (double.TryParse(secondNumberTextBox.Text, out double secondNum))
                {
                    secondNumber = secondNum;
                }
                else
                {
                    MessageBox.Show("Second Number must be a valid number.",
                        "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (double.TryParse(lastResultTextBox.Text, out double lastRes))
                {
                    lastResult = lastRes;
                }
                else
                {
                    MessageBox.Show("Last Result must be a valid number.",
                        "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Settings saved successfully.",
                    "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings: {ex.Message}",
                    "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public DateTime GetLastCalculationDate() => lastCalculationDate;
        public int GetTotalCalculations() => totalCalculations;
        public int GetMaxHistoryEntries() => maxHistoryEntries;
        public double GetFirstNumber() => firstNumber;
        public double GetSecondNumber() => secondNumber;
        public double GetLastResult() => lastResult;
    }
}
