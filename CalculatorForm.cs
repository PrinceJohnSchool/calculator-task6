using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CalculatorApp
{
    /// <summary>
    /// Main calculator form that provides a graphical interface for performing basic arithmetic operations.
    /// Supports addition, subtraction, multiplication, and division with history tracking and data persistence.
    /// </summary>
    public partial class CalculatorForm : Form
    {
        // Constants
        private const int MaxHistoryEntries = 50; // Maximum number of history entries that can be stored

        // UI Controls - Input fields and buttons
        private TextBox number1TextBox;        // Text box for entering the first number
        private TextBox number2TextBox;        // Text box for entering the second number
        private Button addButton;              // Button to perform addition
        private Button subtractButton;         // Button to perform subtraction
        private Button multiplyButton;         // Button to perform multiplication
        private Button divideButton;           // Button to perform division
        private Button clearButton;            // Button to clear input fields and result
        private Button viewHistoryButton;      // Button to open the history form
        private Button saveArraysButton;       // Button to save array data to file
        private Button loadArraysButton;       // Button to load array data from file
        private Button saveSettingsButton;     // Button to save settings to file
        private Button loadSettingsButton;     // Button to load settings from file
        private Label resultLabel;             // Label displaying the calculation result

        // Calculation data
        private double firstNumber;            // The first number entered by the user
        private double secondNumber;           // The second number entered by the user
        private double calculationResult;     // The result of the current calculation
        private List<string> calculationHistory; // List storing calculation history as strings

        // Arrays for the arrays assignment requirements
        // These arrays store calculation history in a fixed-size array format
        private double[] resultArray;          // Array storing just the numeric result values
        private string[] operationArray;       // Array storing the text of the calculation (e.g. "5 + 3 = 8")
        private int historyEntryCount;         // Keeps track of how many entries are currently stored in the arrays

        /// <summary>
        /// Constructor for CalculatorForm.
        /// Initializes the form, creates data structures, and loads saved data from files.
        /// </summary>
        public CalculatorForm()
        {
            // Initialize data structures
            calculationHistory = new List<string>(); // Initialize the history list
            resultArray = new double[MaxHistoryEntries]; // Initialize result array with max capacity
            operationArray = new string[MaxHistoryEntries]; // Initialize operation array with max capacity
            historyEntryCount = 0; // Start with no history entries
            
            // Set up the form UI
            InitializeComponent();
            
            // Load previously saved data silently (without showing dialogs)
            LoadArrayDataFromFileSilent(); // Load array data from file if it exists
            LoadSettingsFromFileSilent();  // Load settings from file if they exist
        }

        /// <summary>
        /// Initializes all UI components for the calculator form.
        /// Sets up text boxes, buttons, labels, and their properties and event handlers.
        /// </summary>
        private void InitializeComponent()
        {
            // Configure form properties
            this.Text = "Simple Calculator";
            this.Size = new Size(400, 490);
            this.StartPosition = FormStartPosition.CenterScreen; // Center the form on screen
            this.BackColor = Color.LightGray;
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // Fixed size dialog
            this.MaximizeBox = false; // Cannot maximize
            this.MinimizeBox = true;  // Can minimize

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

        /// <summary>
        /// Validates that both input text boxes contain valid numeric values.
        /// Parses the text box values and stores them in firstNumber and secondNumber if valid.
        /// </summary>
        /// <returns>True if both inputs are valid numbers, false otherwise</returns>
        private bool ValidateInputs()
        {
            // Try to parse the first number
            if (double.TryParse(number1TextBox.Text, out firstNumber))
            {
                // First number is valid, try to parse the second number
                if (double.TryParse(number2TextBox.Text, out secondNumber))
                {
                    return true; // Both numbers are valid
                }
                else
                {
                    // Second number is invalid - show error message
                    resultLabel.Text = "Error: Second number is invalid. Please enter a valid number.";
                    resultLabel.ForeColor = Color.Red;
                    return false;
                }
            }
            else
            {
                // First number is invalid - show error message
                resultLabel.Text = "Error: First number is invalid. Please enter a valid number.";
                resultLabel.ForeColor = Color.Red;
                return false;
            }
        }

        // Arithmetic operation methods - perform basic mathematical operations
        
        /// <summary>
        /// Performs addition of two numbers.
        /// </summary>
        /// <param name="num1">First number</param>
        /// <param name="num2">Second number</param>
        /// <returns>The sum of num1 and num2</returns>
        private double PerformAddition(double num1, double num2)
        {
            return num1 + num2;
        }

        /// <summary>
        /// Performs subtraction of two numbers.
        /// </summary>
        /// <param name="num1">First number (minuend)</param>
        /// <param name="num2">Second number (subtrahend)</param>
        /// <returns>The difference of num1 and num2 (num1 - num2)</returns>
        private double PerformSubtraction(double num1, double num2)
        {
            return num1 - num2;
        }

        /// <summary>
        /// Performs multiplication of two numbers.
        /// </summary>
        /// <param name="num1">First number</param>
        /// <param name="num2">Second number</param>
        /// <returns>The product of num1 and num2</returns>
        private double PerformMultiplication(double num1, double num2)
        {
            return num1 * num2;
        }

        /// <summary>
        /// Performs division of two numbers.
        /// Note: Does not check for division by zero - caller should validate.
        /// </summary>
        /// <param name="num1">First number (dividend)</param>
        /// <param name="num2">Second number (divisor)</param>
        /// <returns>The quotient of num1 and num2 (num1 / num2)</returns>
        private double PerformDivision(double num1, double num2)
        {
            return num1 / num2;
        }

        /// <summary>
        /// Event handler for the Add button click.
        /// Performs addition operation, displays result, and adds to history.
        /// </summary>
        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs before performing calculation
                if (ValidateInputs())
                {
                    // Perform addition and format the result
                    calculationResult = PerformAddition(firstNumber, secondNumber);
                    string calculationText = $"{firstNumber} + {secondNumber} = {calculationResult}";
                    resultLabel.Text = $"Result: {calculationText}";
                    resultLabel.ForeColor = Color.Black;
                    // Add to history and enable controls
                    AddToHistory(calculationText);
                    EnableCalculatorControls(true);
                }
            }
            catch (OverflowException ex)
            {
                // Handle overflow errors (result too large)
                resultLabel.Text = $"Error: Calculation result is too large. {ex.Message}";
                resultLabel.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                // Handle any other unexpected errors
                resultLabel.Text = $"Error: An unexpected error occurred. {ex.Message}";
                resultLabel.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// Event handler for the Subtract button click.
        /// Performs subtraction operation, displays result, and adds to history.
        /// </summary>
        private void SubtractButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs before performing calculation
                if (ValidateInputs())
                {
                    // Perform subtraction and format the result
                    calculationResult = PerformSubtraction(firstNumber, secondNumber);
                    string calculationText = $"{firstNumber} − {secondNumber} = {calculationResult}";
                    resultLabel.Text = $"Result: {calculationText}";
                    resultLabel.ForeColor = Color.Black;
                    // Add to history and enable controls
                    AddToHistory(calculationText);
                    EnableCalculatorControls(true);
                }
            }
            catch (OverflowException ex)
            {
                // Handle overflow errors (result too large)
                resultLabel.Text = $"Error: Calculation result is too large. {ex.Message}";
                resultLabel.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                // Handle any other unexpected errors
                resultLabel.Text = $"Error: An unexpected error occurred. {ex.Message}";
                resultLabel.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// Event handler for the Multiply button click.
        /// Performs multiplication operation, displays result, and adds to history.
        /// </summary>
        private void MultiplyButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs before performing calculation
                if (ValidateInputs())
                {
                    // Perform multiplication and format the result
                    calculationResult = PerformMultiplication(firstNumber, secondNumber);
                    string calculationText = $"{firstNumber} × {secondNumber} = {calculationResult}";
                    resultLabel.Text = $"Result: {calculationText}";
                    resultLabel.ForeColor = Color.Black;
                    // Add to history and enable controls
                    AddToHistory(calculationText);
                    EnableCalculatorControls(true);
                }
            }
            catch (OverflowException ex)
            {
                // Handle overflow errors (result too large)
                resultLabel.Text = $"Error: Calculation result is too large. {ex.Message}";
                resultLabel.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                // Handle any other unexpected errors
                resultLabel.Text = $"Error: An unexpected error occurred. {ex.Message}";
                resultLabel.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// Event handler for the Divide button click.
        /// Performs division operation, displays result, and adds to history.
        /// Checks for division by zero before performing the operation.
        /// </summary>
        private void DivideButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs before performing calculation
                if (ValidateInputs())
                {
                    // Check for division by zero
                    if (secondNumber == 0)
                    {
                        throw new DivideByZeroException("Cannot divide by zero. Please enter a non-zero second number.");
                    }
                    // Perform division and format the result
                    calculationResult = PerformDivision(firstNumber, secondNumber);
                    string calculationText = $"{firstNumber} ÷ {secondNumber} = {calculationResult}";
                    resultLabel.Text = $"Result: {calculationText}";
                    resultLabel.ForeColor = Color.Black;
                    // Add to history and enable controls
                    AddToHistory(calculationText);
                    EnableCalculatorControls(true);
                }
            }
            catch (DivideByZeroException ex)
            {
                // Handle division by zero error
                resultLabel.Text = $"Error: {ex.Message}";
                resultLabel.ForeColor = Color.Red;
            }
            catch (OverflowException ex)
            {
                // Handle overflow errors (result too large)
                resultLabel.Text = $"Error: Calculation result is too large. {ex.Message}";
                resultLabel.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                // Handle any other unexpected errors
                resultLabel.Text = $"Error: An unexpected error occurred. {ex.Message}";
                resultLabel.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// Event handler for the Clear button click.
        /// Clears all input fields and resets the calculator.
        /// </summary>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            ClearCalculator();
        }

        /// <summary>
        /// Event handler for the View History button click.
        /// Opens the history form to display calculation history.
        /// </summary>
        private void ViewHistoryButton_Click(object sender, EventArgs e)
        {
            OpenHistoryForm();
        }

        /// <summary>
        /// Clears all input fields and resets the result label.
        /// Also disables the operation buttons.
        /// </summary>
        private void ClearCalculator()
        {
            number1TextBox.Clear();
            number2TextBox.Clear();
            resultLabel.Text = "Result will appear here";
            resultLabel.ForeColor = Color.Black;
            EnableCalculatorControls(false); // Disable operation buttons when cleared
        }

        /// <summary>
        /// Enables or disables the calculator operation buttons based on input state.
        /// </summary>
        /// <param name="enabled">If true, enables buttons when inputs are valid. If false, disables all buttons.</param>
        private void EnableCalculatorControls(bool enabled)
        {
            if (enabled)
            {
                // Enable buttons only if both text boxes have non-empty content
                bool hasValidInputs = !string.IsNullOrWhiteSpace(number1TextBox.Text) && !string.IsNullOrWhiteSpace(number2TextBox.Text);
                addButton.Enabled = hasValidInputs;
                subtractButton.Enabled = hasValidInputs;
                multiplyButton.Enabled = hasValidInputs;
                divideButton.Enabled = hasValidInputs;
            }
            else
            {
                // Disable all operation buttons
                addButton.Enabled = false;
                subtractButton.Enabled = false;
                multiplyButton.Enabled = false;
                divideButton.Enabled = false;
            }
        }

        /// <summary>
        /// Adds a calculation to the history list and arrays.
        /// Uses circular buffer approach - wraps around when max entries reached.
        /// </summary>
        /// <param name="calculation">The calculation string to add (e.g., "5 + 3 = 8")</param>
        private void AddToHistory(string calculation)
        {
            try
            {
                // If we've reached max entries, wrap around to the beginning (circular buffer)
                if (historyEntryCount >= MaxHistoryEntries)
                {
                    historyEntryCount = 0;
                }

                // Store the result and operation in the arrays
                resultArray[historyEntryCount] = calculationResult;
                operationArray[historyEntryCount] = calculation;

                historyEntryCount++; // Increment for next entry

                // Also add to the list with timestamp
                calculationHistory.Add($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {calculation}");
            }
            catch (IndexOutOfRangeException ex)
            {
                // Handle array index errors
                resultLabel.Text = $"Error: History array index out of range. {ex.Message}";
                resultLabel.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// Event handler for text changes in the number input text boxes.
        /// Updates the enabled state of operation buttons based on input validity.
        /// </summary>
        private void NumberTextBox_TextChanged(object sender, EventArgs e)
        {
            EnableCalculatorControls(true); // Re-evaluate button states
        }

        /// <summary>
        /// Event handler for the Save Array Data button click.
        /// Saves the current array data to a file.
        /// </summary>
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

        /// <summary>
        /// Opens the history form as a modal dialog to display calculation history.
        /// </summary>
        private void OpenHistoryForm()
        {
            HistoryForm historyForm = new HistoryForm(calculationHistory);
            historyForm.ShowDialog(); // Show as modal dialog
        }

        /// <summary>
        /// Event handler for the Load Array Data button click.
        /// Opens the array data form to view and edit array data.
        /// Updates the arrays if the user saves changes.
        /// </summary>
        private void LoadArraysButton_Click(object sender, EventArgs e)
        {
            // Open array data form with current array data
            ArrayDataForm arrayForm = new ArrayDataForm(resultArray, operationArray, historyEntryCount, MaxHistoryEntries);
            if (arrayForm.ShowDialog() == DialogResult.OK)
            {
                // User saved changes - update arrays with edited data
                resultArray = arrayForm.GetResultArray();
                operationArray = arrayForm.GetOperationArray();
                historyEntryCount = arrayForm.GetHistoryEntryCount();
            }
        }

        /// <summary>
        /// Event handler for the Save Settings button click.
        /// Saves the current calculator settings to a file.
        /// </summary>
        private void SaveSettingsButton_Click(object sender, EventArgs e)
        {
            SaveSettingsToFile();
        }

        /// <summary>
        /// Event handler for the Load Settings button click.
        /// Opens the settings form to view and edit calculator settings.
        /// Updates the calculator state if the user saves changes.
        /// </summary>
        private void LoadSettingsButton_Click(object sender, EventArgs e)
        {
            // Prepare current settings values
            DateTime lastDate = DateTime.Now;
            int totalCalc = historyEntryCount;
            int maxEntries = MaxHistoryEntries;
            double firstNum = firstNumber;
            double secondNum = secondNumber;
            double lastRes = calculationResult;

            // Open settings form with current values
            SettingsForm settingsForm = new SettingsForm(lastDate, totalCalc, maxEntries, firstNum, secondNum, lastRes);
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                // User saved changes - update calculator state with edited settings
                firstNumber = settingsForm.GetFirstNumber();
                secondNumber = settingsForm.GetSecondNumber();
                calculationResult = settingsForm.GetLastResult();
                historyEntryCount = settingsForm.GetTotalCalculations();
                
                // Update UI to reflect loaded settings
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
        /// <summary>
        /// Reads array data from the first file (CalculatorArrayData.txt) silently without showing modals.
        /// This method is used for automatic loading on startup.
        /// Fails silently if file doesn't exist or has errors.
        /// </summary>
        private void LoadArrayDataFromFileSilent()
        {
            try
            {
                // Get the file path in the user's Documents folder
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string filePath = Path.Combine(documentsPath, "CalculatorArrayData.txt");

                // If file doesn't exist, just return (silent failure)
                if (!File.Exists(filePath))
                {
                    return;
                }

                // Read and parse the array data file
                using (StreamReader reader = new StreamReader(filePath))
                {
                    // Read and validate header line
                    string headerLine = reader.ReadLine();
                    if (headerLine == null || !headerLine.Contains("Index"))
                    {
                        return; // Invalid file format
                    }

                    // Reset entry count and read data lines
                    historyEntryCount = 0;
                    string line;
                    while ((line = reader.ReadLine()) != null && historyEntryCount < MaxHistoryEntries)
                    {
                        // Parse CSV format: Index, Operation, Result
                        string[] parts = line.Split(',');
                        if (parts.Length >= 3)
                        {
                            // Parse result value and store in arrays
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
                // Silently ignore any errors during silent load
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
        /// <summary>
        /// Reads calculator settings from the second file (CalculatorSettings.txt) silently without showing modals.
        /// This method is used for automatic loading on startup.
        /// Fails silently if file doesn't exist or has errors.
        /// </summary>
        private void LoadSettingsFromFileSilent()
        {
            try
            {
                // Get the file path in the user's Documents folder
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string filePath = Path.Combine(documentsPath, "CalculatorSettings.txt");

                // If file doesn't exist, just return (silent failure)
                if (!File.Exists(filePath))
                {
                    return;
                }

                // Read and parse the settings file
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    // Read each line and parse key-value pairs
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains("="))
                        {
                            string[] parts = line.Split('=');
                            if (parts.Length == 2)
                            {
                                string key = parts[0].Trim();
                                string value = parts[1].Trim();

                                // Parse and set LastResult if found
                                if (key == "LastResult" && double.TryParse(value, out double lastResult))
                                {
                                    calculationResult = lastResult;
                                }
                                // Parse and set TotalCalculations if found (with validation)
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
                // Silently ignore any errors during silent load
            }
        }

        /// <summary>
        /// Reads calculator settings from the second file (CalculatorSettings.txt).
        /// This method demonstrates reading from a file and parsing key-value pairs.
        /// It restores calculator state including last result and calculation count.
        /// Handles FileNotFoundException, IOException, and other exceptions for file read operations.
        /// </summary>
        /// <summary>
        /// Reads calculator settings from the second file (CalculatorSettings.txt).
        /// This method demonstrates reading from a file and parsing key-value pairs.
        /// It handles file I/O exceptions including FileNotFoundException and IOException.
        /// Shows user-friendly messages for file operations.
        /// </summary>
        private void LoadSettingsFromFile()
        {
            try
            {
                // Get the file path in the user's Documents folder
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string filePath = Path.Combine(documentsPath, "CalculatorSettings.txt");

                // Check if file exists
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("Settings file not found.",
                        "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Read and parse the settings file
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    // Read each line and parse key-value pairs
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains("="))
                        {
                            string[] parts = line.Split('=');
                            if (parts.Length == 2)
                            {
                                string key = parts[0].Trim();
                                string value = parts[1].Trim();

                                // Parse and set LastResult if found
                                if (key == "LastResult" && double.TryParse(value, out double lastResult))
                                {
                                    calculationResult = lastResult;
                                }
                                // Parse and set TotalCalculations if found (with validation)
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

                // Show success message
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
