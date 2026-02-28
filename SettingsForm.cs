using System;
using System.Drawing;
using System.Windows.Forms;

namespace CalculatorApp
{
    /// <summary>
    /// Form that allows users to view and edit calculator settings.
    /// Displays and allows modification of settings like calculation date, totals, and last values.
    /// </summary>
    public partial class SettingsForm : Form
    {
        // UI Controls - Text boxes for editing settings
        private TextBox lastCalculationDateTextBox;  // Displays/edits the last calculation date
        private TextBox totalCalculationsTextBox;    // Displays/edits total number of calculations
        private TextBox maxHistoryEntriesTextBox;    // Displays/edits maximum history entries allowed
        private TextBox firstNumberTextBox;          // Displays/edits the first number value
        private TextBox secondNumberTextBox;         // Displays/edits the second number value
        private TextBox lastResultTextBox;           // Displays/edits the last calculation result
        private Button saveButton;                    // Button to save changes
        private Button cancelButton;                  // Button to cancel and close without saving
        private Label titleLabel;                     // Title label at the top of the form

        // Data fields - Store the actual setting values
        private DateTime lastCalculationDate;  // Date and time of the last calculation
        private int totalCalculations;          // Total count of calculations performed
        private int maxHistoryEntries;          // Maximum number of history entries to store
        private double firstNumber;             // The first number from last calculation
        private double secondNumber;            // The second number from last calculation
        private double lastResult;              // The result from the last calculation

        /// <summary>
        /// Constructor for SettingsForm.
        /// Initializes the form with the provided calculator settings values.
        /// </summary>
        /// <param name="lastDate">The date and time of the last calculation</param>
        /// <param name="totalCalc">Total number of calculations performed</param>
        /// <param name="maxEntries">Maximum number of history entries allowed</param>
        /// <param name="firstNum">The first number from the last calculation</param>
        /// <param name="secondNum">The second number from the last calculation</param>
        /// <param name="lastRes">The result from the last calculation</param>
        public SettingsForm(DateTime lastDate, int totalCalc, int maxEntries, double firstNum, double secondNum, double lastRes)
        {
            // Store the provided settings values
            lastCalculationDate = lastDate;
            totalCalculations = totalCalc;
            maxHistoryEntries = maxEntries;
            firstNumber = firstNum;
            secondNumber = secondNum;
            lastResult = lastRes;
            // Initialize the form UI
            InitializeComponent();
            // Load the settings into the text boxes
            LoadSettings();
        }

        /// <summary>
        /// Initializes all UI components for the settings form.
        /// Creates labels, text boxes, and buttons for editing calculator settings.
        /// </summary>
        private void InitializeComponent()
        {
            // Configure form properties
            this.Text = "Edit Settings";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen; // Center the form on screen
            this.BackColor = Color.LightGray;
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // Fixed size dialog
            this.MaximizeBox = false; // Cannot maximize this form

            titleLabel = new Label();
            titleLabel.Text = "Calculator Settings Editor";
            titleLabel.Location = new Point(20, 10);
            titleLabel.Size = new Size(460, 30);
            titleLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(titleLabel);

            // Layout constants for positioning controls
            int yPos = 50;           // Starting Y position for the first control
            int labelWidth = 180;    // Width of label controls
            int textBoxWidth = 250;  // Width of text box controls
            int spacing = 35;        // Vertical spacing between controls

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

        /// <summary>
        /// Loads the current settings values into the text boxes for display and editing.
        /// Converts the stored values to strings and populates the UI controls.
        /// </summary>
        private void LoadSettings()
        {
            // Convert each setting value to string and display in corresponding text box
            lastCalculationDateTextBox.Text = lastCalculationDate.ToString("yyyy-MM-dd HH:mm:ss");
            totalCalculationsTextBox.Text = totalCalculations.ToString();
            maxHistoryEntriesTextBox.Text = maxHistoryEntries.ToString();
            firstNumberTextBox.Text = firstNumber.ToString();
            secondNumberTextBox.Text = secondNumber.ToString();
            lastResultTextBox.Text = lastResult.ToString();
        }

        /// <summary>
        /// Event handler for the Save button click.
        /// Validates and saves all settings from the text boxes.
        /// Shows error messages if any input is invalid.
        /// </summary>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate and parse the last calculation date
                if (DateTime.TryParse(lastCalculationDateTextBox.Text, out DateTime lastDate))
                {
                    lastCalculationDate = lastDate;
                }
                else
                {
                    MessageBox.Show("Invalid date format. Please use yyyy-MM-dd HH:mm:ss",
                        "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Stop saving if validation fails
                }

                // Validate and parse total calculations (must be an integer)
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

                // Validate and parse max history entries (must be an integer)
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

                // Validate and parse first number (must be a double)
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

                // Validate and parse second number (must be a double)
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

                // Validate and parse last result (must be a double)
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

                // All validations passed - show success message and close form
                MessageBox.Show("Settings saved successfully.",
                    "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Indicate successful save
                this.Close();
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors during save
                MessageBox.Show($"Error saving settings: {ex.Message}",
                    "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event handler for the Cancel button click.
        /// Closes the form without saving any changes.
        /// </summary>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Indicate cancellation
            this.Close();
        }

        // Public getter methods to retrieve the settings values after form closes
        /// <summary>Gets the last calculation date.</summary>
        public DateTime GetLastCalculationDate() => lastCalculationDate;
        /// <summary>Gets the total number of calculations.</summary>
        public int GetTotalCalculations() => totalCalculations;
        /// <summary>Gets the maximum history entries allowed.</summary>
        public int GetMaxHistoryEntries() => maxHistoryEntries;
        /// <summary>Gets the first number value.</summary>
        public double GetFirstNumber() => firstNumber;
        /// <summary>Gets the second number value.</summary>
        public double GetSecondNumber() => secondNumber;
        /// <summary>Gets the last result value.</summary>
        public double GetLastResult() => lastResult;
    }
}
