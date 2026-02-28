using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CalculatorApp
{
    public partial class CalculatorForm : Form
    {
        private const int MaxHistoryEntries = 50;

        private TextBox number1TextBox;
        private TextBox number2TextBox;
        private Button addButton;
        private Button subtractButton;
        private Button multiplyButton;
        private Button divideButton;
        private Button clearButton;
        private Button viewHistoryButton;
        private Button saveArraysButton;
        private Label resultLabel;

        private double firstNumber;
        private double secondNumber;
        private double calculationResult;
        private List<string> calculationHistory;

        // Arrays for the arrays assignment requirements
        // Stores just the numeric result values
        private double[] resultArray;
        // Stores the text of the calculation (e.g. "5 + 3 = 8")
        private string[] operationArray;
        // Keeps track of how many entries are currently stored
        private int historyEntryCount;

        public CalculatorForm()
        {
            calculationHistory = new List<string>();
            resultArray = new double[MaxHistoryEntries];
            operationArray = new string[MaxHistoryEntries];
            historyEntryCount = 0;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Simple Calculator";
            this.Size = new Size(400, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.LightGray;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = true;

            number1TextBox = new TextBox();
            number1TextBox.Location = new Point(50, 30);
            number1TextBox.Size = new Size(150, 25);
            number1TextBox.Font = new Font("Arial", 12);
            number1TextBox.TextAlign = HorizontalAlignment.Center;
            number1TextBox.PlaceholderText = "Enter first number";
            number1TextBox.TextChanged += NumberTextBox_TextChanged;
            this.Controls.Add(number1TextBox);

            number2TextBox = new TextBox();
            number2TextBox.Location = new Point(200, 30);
            number2TextBox.Size = new Size(150, 25);
            number2TextBox.Font = new Font("Arial", 12);
            number2TextBox.TextAlign = HorizontalAlignment.Center;
            number2TextBox.PlaceholderText = "Enter second number";
            number2TextBox.TextChanged += NumberTextBox_TextChanged;
            this.Controls.Add(number2TextBox);

            addButton = new Button();
            addButton.Text = "+";
            addButton.Location = new Point(50, 80);
            addButton.Size = new Size(70, 40);
            addButton.Font = new Font("Arial", 16, FontStyle.Bold);
            addButton.BackColor = Color.LightBlue;
            addButton.ForeColor = Color.DarkBlue;
            addButton.Click += AddButton_Click;
            this.Controls.Add(addButton);

            subtractButton = new Button();
            subtractButton.Text = "−";
            subtractButton.Location = new Point(130, 80);
            subtractButton.Size = new Size(70, 40);
            subtractButton.Font = new Font("Arial", 16, FontStyle.Bold);
            subtractButton.BackColor = Color.LightGreen;
            subtractButton.ForeColor = Color.DarkGreen;
            subtractButton.Click += SubtractButton_Click;
            this.Controls.Add(subtractButton);

            multiplyButton = new Button();
            multiplyButton.Text = "×";
            multiplyButton.Location = new Point(210, 80);
            multiplyButton.Size = new Size(70, 40);
            multiplyButton.Font = new Font("Arial", 16, FontStyle.Bold);
            multiplyButton.BackColor = Color.LightYellow;
            multiplyButton.ForeColor = Color.DarkOrange;
            multiplyButton.Click += MultiplyButton_Click;
            this.Controls.Add(multiplyButton);

            divideButton = new Button();
            divideButton.Text = "÷";
            divideButton.Location = new Point(290, 80);
            divideButton.Size = new Size(70, 40);
            divideButton.Font = new Font("Arial", 16, FontStyle.Bold);
            divideButton.BackColor = Color.LightCoral;
            divideButton.ForeColor = Color.DarkRed;
            divideButton.Click += DivideButton_Click;
            this.Controls.Add(divideButton);

            resultLabel = new Label();
            resultLabel.Text = "Result will appear here";
            resultLabel.Location = new Point(50, 150);
            resultLabel.Size = new Size(300, 30);
            resultLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            resultLabel.TextAlign = ContentAlignment.MiddleCenter;
            resultLabel.BackColor = Color.White;
            resultLabel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(resultLabel);

            clearButton = new Button();
            clearButton.Text = "Clear";
            clearButton.Location = new Point(50, 200);
            clearButton.Size = new Size(120, 35);
            clearButton.Font = new Font("Arial", 11, FontStyle.Bold);
            clearButton.BackColor = Color.LightYellow;
            clearButton.ForeColor = Color.DarkOrange;
            clearButton.Click += ClearButton_Click;
            this.Controls.Add(clearButton);

            viewHistoryButton = new Button();
            viewHistoryButton.Text = "View History";
            viewHistoryButton.Location = new Point(230, 200);
            viewHistoryButton.Size = new Size(120, 35);
            viewHistoryButton.Font = new Font("Arial", 11, FontStyle.Bold);
            viewHistoryButton.BackColor = Color.LightGreen;
            viewHistoryButton.ForeColor = Color.DarkGreen;
            viewHistoryButton.Click += ViewHistoryButton_Click;
            this.Controls.Add(viewHistoryButton);

            saveArraysButton = new Button();
            saveArraysButton.Text = "Save Array Data";
            saveArraysButton.Location = new Point(50, 245);
            saveArraysButton.Size = new Size(300, 35);
            saveArraysButton.Font = new Font("Arial", 11, FontStyle.Bold);
            saveArraysButton.BackColor = Color.LightBlue;
            saveArraysButton.ForeColor = Color.DarkBlue;
            saveArraysButton.Click += SaveArraysButton_Click;
            this.Controls.Add(saveArraysButton);

            Label titleLabel = new Label();
            titleLabel.Text = "Simple Calculator";
            titleLabel.Location = new Point(50, 5);
            titleLabel.Size = new Size(300, 20);
            titleLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(titleLabel);
        }

        private bool ValidateInputs()
        {
            if (double.TryParse(number1TextBox.Text, out firstNumber))
            {
                if (double.TryParse(number2TextBox.Text, out secondNumber))
                {
                    return true;
                }
                else
                {
                    resultLabel.Text = "Error: Second number is invalid. Please enter a valid number.";
                    resultLabel.ForeColor = Color.Red;
                    return false;
                }
            }
            else
            {
                resultLabel.Text = "Error: First number is invalid. Please enter a valid number.";
                resultLabel.ForeColor = Color.Red;
                return false;
            }
        }

        private double PerformAddition(double num1, double num2)
        {
            return num1 + num2;
        }

        private double PerformSubtraction(double num1, double num2)
        {
            return num1 - num2;
        }

        private double PerformMultiplication(double num1, double num2)
        {
            return num1 * num2;
        }

        private double PerformDivision(double num1, double num2)
        {
            return num1 / num2;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInputs())
                {
                    calculationResult = PerformAddition(firstNumber, secondNumber);
                    string calculationText = $"{firstNumber} + {secondNumber} = {calculationResult}";
                    resultLabel.Text = $"Result: {calculationText}";
                    resultLabel.ForeColor = Color.Black;
                    AddToHistory(calculationText);
                    EnableCalculatorControls(true);
                }
            }
            catch (OverflowException ex)
            {
                resultLabel.Text = $"Error: Calculation result is too large. {ex.Message}";
                resultLabel.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                resultLabel.Text = $"Error: An unexpected error occurred. {ex.Message}";
                resultLabel.ForeColor = Color.Red;
            }
        }

        private void SubtractButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInputs())
                {
                    calculationResult = PerformSubtraction(firstNumber, secondNumber);
                    string calculationText = $"{firstNumber} − {secondNumber} = {calculationResult}";
                    resultLabel.Text = $"Result: {calculationText}";
                    resultLabel.ForeColor = Color.Black;
                    AddToHistory(calculationText);
                    EnableCalculatorControls(true);
                }
            }
            catch (OverflowException ex)
            {
                resultLabel.Text = $"Error: Calculation result is too large. {ex.Message}";
                resultLabel.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                resultLabel.Text = $"Error: An unexpected error occurred. {ex.Message}";
                resultLabel.ForeColor = Color.Red;
            }
        }

        private void MultiplyButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInputs())
                {
                    calculationResult = PerformMultiplication(firstNumber, secondNumber);
                    string calculationText = $"{firstNumber} × {secondNumber} = {calculationResult}";
                    resultLabel.Text = $"Result: {calculationText}";
                    resultLabel.ForeColor = Color.Black;
                    AddToHistory(calculationText);
                    EnableCalculatorControls(true);
                }
            }
            catch (OverflowException ex)
            {
                resultLabel.Text = $"Error: Calculation result is too large. {ex.Message}";
                resultLabel.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                resultLabel.Text = $"Error: An unexpected error occurred. {ex.Message}";
                resultLabel.ForeColor = Color.Red;
            }
        }

        private void DivideButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInputs())
                {
                    if (secondNumber == 0)
                    {
                        throw new DivideByZeroException("Cannot divide by zero. Please enter a non-zero second number.");
                    }
                    calculationResult = PerformDivision(firstNumber, secondNumber);
                    string calculationText = $"{firstNumber} ÷ {secondNumber} = {calculationResult}";
                    resultLabel.Text = $"Result: {calculationText}";
                    resultLabel.ForeColor = Color.Black;
                    AddToHistory(calculationText);
                    EnableCalculatorControls(true);
                }
            }
            catch (DivideByZeroException ex)
            {
                resultLabel.Text = $"Error: {ex.Message}";
                resultLabel.ForeColor = Color.Red;
            }
            catch (OverflowException ex)
            {
                resultLabel.Text = $"Error: Calculation result is too large. {ex.Message}";
                resultLabel.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                resultLabel.Text = $"Error: An unexpected error occurred. {ex.Message}";
                resultLabel.ForeColor = Color.Red;
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            ClearCalculator();
        }

        private void ViewHistoryButton_Click(object sender, EventArgs e)
        {
            OpenHistoryForm();
        }

        private void ClearCalculator()
        {
            number1TextBox.Clear();
            number2TextBox.Clear();
            resultLabel.Text = "Result will appear here";
            resultLabel.ForeColor = Color.Black;
            EnableCalculatorControls(false);
        }

        private void EnableCalculatorControls(bool enabled)
        {
            if (enabled)
            {
                addButton.Enabled = !string.IsNullOrWhiteSpace(number1TextBox.Text) && !string.IsNullOrWhiteSpace(number2TextBox.Text);
                subtractButton.Enabled = !string.IsNullOrWhiteSpace(number1TextBox.Text) && !string.IsNullOrWhiteSpace(number2TextBox.Text);
                multiplyButton.Enabled = !string.IsNullOrWhiteSpace(number1TextBox.Text) && !string.IsNullOrWhiteSpace(number2TextBox.Text);
                divideButton.Enabled = !string.IsNullOrWhiteSpace(number1TextBox.Text) && !string.IsNullOrWhiteSpace(number2TextBox.Text);
            }
            else
            {
                addButton.Enabled = false;
                subtractButton.Enabled = false;
                multiplyButton.Enabled = false;
                divideButton.Enabled = false;
            }
        }

        private void AddToHistory(string calculation)
        {
            try
            {
                if (historyEntryCount >= MaxHistoryEntries)
                {
                    historyEntryCount = 0;
                }

                resultArray[historyEntryCount] = calculationResult;
                operationArray[historyEntryCount] = calculation;

                historyEntryCount++;

                calculationHistory.Add($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {calculation}");
            }
            catch (IndexOutOfRangeException ex)
            {
                resultLabel.Text = $"Error: History array index out of range. {ex.Message}";
                resultLabel.ForeColor = Color.Red;
            }
        }

        private void NumberTextBox_TextChanged(object sender, EventArgs e)
        {
            EnableCalculatorControls(true);
        }

        private void SaveArraysButton_Click(object sender, EventArgs e)
        {
            SaveArraysToFile();
        }

        private void SaveArraysToFile()
        {
            try
            {
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string filePath = Path.Combine(documentsPath, "CalculatorArrayData.txt");

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("Index, Operation, Result");

                    for (int i = 0; i < historyEntryCount; i++)
                    {
                        writer.WriteLine($"{i + 1}, {operationArray[i]}, {resultArray[i]}");
                    }
                }

                int previewCount = Math.Min(historyEntryCount, 5);
                string previewText = string.Empty;

                for (int i = 0; i < previewCount; i++)
                {
                    previewText += $"{i + 1}. {operationArray[i]} (Result: {resultArray[i]}){Environment.NewLine}";
                }

                string message;

                if (historyEntryCount > 0)
                {
                    message = $"Array data saved to:{Environment.NewLine}{filePath}{Environment.NewLine}{Environment.NewLine}" +
                              $"First {previewCount} entries:{Environment.NewLine}{previewText}";
                }
                else
                {
                    message = $"Array data saved to:{Environment.NewLine}{filePath}{Environment.NewLine}{Environment.NewLine}" +
                              "No calculations stored in the arrays yet.";
                }

                MessageBox.Show(message, "Array Data Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show($"Error: Access denied when writing array data to file.{Environment.NewLine}{ex.Message}",
                    "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Error: Problem writing array data to file.{Environment.NewLine}{ex.Message}",
                    "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenHistoryForm()
        {
            HistoryForm historyForm = new HistoryForm(calculationHistory);
            historyForm.ShowDialog();
        }
    }
}
