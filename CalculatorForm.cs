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
        private Button loadArraysButton;
        private Button saveSettingsButton;
        private Button loadSettingsButton;
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
            LoadArrayDataFromFileSilent();
            LoadSettingsFromFileSilent();
        }

        private void InitializeComponent()
        {
            this.Text = "Simple Calculator";
            this.Size = new Size(400, 490);
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
            saveArraysButton.Size = new Size(145, 35);
            saveArraysButton.Font = new Font("Arial", 11, FontStyle.Bold);
            saveArraysButton.BackColor = Color.LightBlue;
            saveArraysButton.ForeColor = Color.DarkBlue;
            saveArraysButton.Click += SaveArraysButton_Click;
            this.Controls.Add(saveArraysButton);

            loadArraysButton = new Button();
            loadArraysButton.Text = "Load Array Data";
            loadArraysButton.Location = new Point(205, 245);
            loadArraysButton.Size = new Size(145, 35);
            loadArraysButton.Font = new Font("Arial", 11, FontStyle.Bold);
            loadArraysButton.BackColor = Color.LightCyan;
            loadArraysButton.ForeColor = Color.DarkCyan;
            loadArraysButton.Click += LoadArraysButton_Click;
            this.Controls.Add(loadArraysButton);

            saveSettingsButton = new Button();
            saveSettingsButton.Text = "Save Settings";
            saveSettingsButton.Location = new Point(50, 290);
            saveSettingsButton.Size = new Size(145, 35);
            saveSettingsButton.Font = new Font("Arial", 11, FontStyle.Bold);
            saveSettingsButton.BackColor = Color.LightPink;
            saveSettingsButton.ForeColor = Color.DarkRed;
            saveSettingsButton.Click += SaveSettingsButton_Click;
            this.Controls.Add(saveSettingsButton);

            loadSettingsButton = new Button();
            loadSettingsButton.Text = "Load Settings";
            loadSettingsButton.Location = new Point(205, 290);
            loadSettingsButton.Size = new Size(145, 35);
            loadSettingsButton.Font = new Font("Arial", 11, FontStyle.Bold);
            loadSettingsButton.BackColor = Color.LightSalmon;
            loadSettingsButton.ForeColor = Color.DarkRed;
            loadSettingsButton.Click += LoadSettingsButton_Click;
            this.Controls.Add(loadSettingsButton);

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

        /// <summary>
        /// Writes array data to the first file (CalculatorArrayData.txt).
        /// This method demonstrates writing array contents to a file in CSV format.
        /// It writes both the resultArray (double[]) and operationArray (string[]) data.
        /// Handles UnauthorizedAccessException and IOException for file write operations.
        /// </summary>
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

        private void LoadArraysButton_Click(object sender, EventArgs e)
        {
            ArrayDataForm arrayForm = new ArrayDataForm(resultArray, operationArray, historyEntryCount, MaxHistoryEntries);
            if (arrayForm.ShowDialog() == DialogResult.OK)
            {
                resultArray = arrayForm.GetResultArray();
                operationArray = arrayForm.GetOperationArray();
                historyEntryCount = arrayForm.GetHistoryEntryCount();
            }
        }

        private void SaveSettingsButton_Click(object sender, EventArgs e)
        {
            SaveSettingsToFile();
        }

        private void LoadSettingsButton_Click(object sender, EventArgs e)
        {
            DateTime lastDate = DateTime.Now;
            int totalCalc = historyEntryCount;
            int maxEntries = MaxHistoryEntries;
            double firstNum = firstNumber;
            double secondNum = secondNumber;
            double lastRes = calculationResult;

            SettingsForm settingsForm = new SettingsForm(lastDate, totalCalc, maxEntries, firstNum, secondNum, lastRes);
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                firstNumber = settingsForm.GetFirstNumber();
                secondNumber = settingsForm.GetSecondNumber();
                calculationResult = settingsForm.GetLastResult();
                historyEntryCount = settingsForm.GetTotalCalculations();
                
                number1TextBox.Text = firstNumber.ToString();
                number2TextBox.Text = secondNumber.ToString();
                resultLabel.Text = $"Last Result: {calculationResult}";
                resultLabel.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Reads array data from the first file (CalculatorArrayData.txt) silently without showing modals.
        /// This method is used for automatic loading on startup.
        /// </summary>
        private void LoadArrayDataFromFileSilent()
        {
            try
            {
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string filePath = Path.Combine(documentsPath, "CalculatorArrayData.txt");

                if (!File.Exists(filePath))
                {
                    return;
                }

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string headerLine = reader.ReadLine();
                    if (headerLine == null || !headerLine.Contains("Index"))
                    {
                        return;
                    }

                    historyEntryCount = 0;
                    string line;
                    while ((line = reader.ReadLine()) != null && historyEntryCount < MaxHistoryEntries)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length >= 3)
                        {
                            if (double.TryParse(parts[2].Trim(), out double result))
                            {
                                resultArray[historyEntryCount] = result;
                                operationArray[historyEntryCount] = parts[1].Trim();
                                historyEntryCount++;
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Reads array data from the first file (CalculatorArrayData.txt).
        /// This method demonstrates reading from a file and populating arrays.
        /// It handles file I/O exceptions including FileNotFoundException and IOException.
        /// </summary>
        private void LoadArrayDataFromFile()
        {
            try
            {
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string filePath = Path.Combine(documentsPath, "CalculatorArrayData.txt");

                if (!File.Exists(filePath))
                {
                    MessageBox.Show("Array data file not found.",
                        "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string headerLine = reader.ReadLine();
                    if (headerLine == null || !headerLine.Contains("Index"))
                    {
                        MessageBox.Show("Invalid file format.",
                            "File Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    historyEntryCount = 0;
                    string line;
                    while ((line = reader.ReadLine()) != null && historyEntryCount < MaxHistoryEntries)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length >= 3)
                        {
                            if (double.TryParse(parts[2].Trim(), out double result))
                            {
                                resultArray[historyEntryCount] = result;
                                operationArray[historyEntryCount] = parts[1].Trim();
                                historyEntryCount++;
                            }
                        }
                    }
                }

                if (historyEntryCount > 0)
                {
                    MessageBox.Show($"Loaded {historyEntryCount} calculation entries from array data file.",
                        "Array Data Loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show($"Array data file not found.{Environment.NewLine}{ex.Message}",
                    "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Error reading array data file.{Environment.NewLine}{ex.Message}",
                    "File Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error loading array data.{Environment.NewLine}{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Writes calculator settings to the second file (CalculatorSettings.txt).
        /// This method demonstrates writing to a file with key-value pairs.
        /// It saves current calculator state including last result, calculation count, and other settings.
        /// Handles UnauthorizedAccessException and IOException for file write operations.
        /// </summary>
        private void SaveSettingsToFile()
        {
            try
            {
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string filePath = Path.Combine(documentsPath, "CalculatorSettings.txt");

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine($"LastCalculationDate={DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    writer.WriteLine($"TotalCalculations={historyEntryCount}");
                    writer.WriteLine($"MaxHistoryEntries={MaxHistoryEntries}");
                    writer.WriteLine($"FirstNumber={firstNumber}");
                    writer.WriteLine($"SecondNumber={secondNumber}");
                    writer.WriteLine($"LastResult={calculationResult}");
                }

                MessageBox.Show($"Settings saved to:{Environment.NewLine}{filePath}",
                    "Settings Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show($"Error: Access denied when writing settings file.{Environment.NewLine}{ex.Message}",
                    "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Error: Problem writing settings file.{Environment.NewLine}{ex.Message}",
                    "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error saving settings.{Environment.NewLine}{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Reads calculator settings from the second file (CalculatorSettings.txt) silently without showing modals.
        /// This method is used for automatic loading on startup.
        /// </summary>
        private void LoadSettingsFromFileSilent()
        {
            try
            {
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string filePath = Path.Combine(documentsPath, "CalculatorSettings.txt");

                if (!File.Exists(filePath))
                {
                    return;
                }

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains("="))
                        {
                            string[] parts = line.Split('=');
                            if (parts.Length == 2)
                            {
                                string key = parts[0].Trim();
                                string value = parts[1].Trim();

                                if (key == "LastResult" && double.TryParse(value, out double lastResult))
                                {
                                    calculationResult = lastResult;
                                }
                                else if (key == "TotalCalculations" && int.TryParse(value, out int total))
                                {
                                    if (total > 0 && total <= MaxHistoryEntries)
                                    {
                                        historyEntryCount = total;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Reads calculator settings from the second file (CalculatorSettings.txt).
        /// This method demonstrates reading from a file and parsing key-value pairs.
        /// It restores calculator state including last result and calculation count.
        /// Handles FileNotFoundException, IOException, and other exceptions for file read operations.
        /// </summary>
        private void LoadSettingsFromFile()
        {
            try
            {
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string filePath = Path.Combine(documentsPath, "CalculatorSettings.txt");

                if (!File.Exists(filePath))
                {
                    MessageBox.Show("Settings file not found.",
                        "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains("="))
                        {
                            string[] parts = line.Split('=');
                            if (parts.Length == 2)
                            {
                                string key = parts[0].Trim();
                                string value = parts[1].Trim();

                                if (key == "LastResult" && double.TryParse(value, out double lastResult))
                                {
                                    calculationResult = lastResult;
                                }
                                else if (key == "TotalCalculations" && int.TryParse(value, out int total))
                                {
                                    if (total > 0 && total <= MaxHistoryEntries)
                                    {
                                        historyEntryCount = total;
                                    }
                                }
                            }
                        }
                    }
                }

                MessageBox.Show("Settings loaded successfully from file.",
                    "Settings Loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show($"Settings file not found.{Environment.NewLine}{ex.Message}",
                    "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Error reading settings file.{Environment.NewLine}{ex.Message}",
                    "File Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error loading settings.{Environment.NewLine}{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
