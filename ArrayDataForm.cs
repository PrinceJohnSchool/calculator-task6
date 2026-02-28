using System;
using System.Drawing;
using System.Windows.Forms;

namespace CalculatorApp
{
    /// <summary>
    /// Form that allows users to view and edit the calculator's array data.
    /// Displays and allows modification of the result array and operation array used for history storage.
    /// </summary>
    public partial class ArrayDataForm : Form
    {
        // UI Controls
        private DataGridView arrayDataGridView;  // Grid view displaying the array data in a table format
        private Button saveButton;                // Button to save changes to the arrays
        private Button cancelButton;              // Button to cancel and close without saving
        private Label titleLabel;                 // Title label at the top of the form
        
        // Data arrays - These store the calculation history
        private double[] resultArray;      // Array storing numeric results of calculations
        private string[] operationArray;   // Array storing operation strings (e.g., "5 + 3 = 8")
        private int historyEntryCount;     // Current number of entries stored in the arrays
        private int maxHistoryEntries;     // Maximum capacity of the arrays

        /// <summary>
        /// Constructor for ArrayDataForm.
        /// Initializes the form with the provided array data for editing.
        /// </summary>
        /// <param name="results">Array of calculation result values</param>
        /// <param name="operations">Array of operation strings</param>
        /// <param name="entryCount">Number of valid entries in the arrays</param>
        /// <param name="maxEntries">Maximum capacity of the arrays</param>
        public ArrayDataForm(double[] results, string[] operations, int entryCount, int maxEntries)
        {
            // Initialize arrays with the maximum capacity
            resultArray = new double[maxEntries];
            operationArray = new string[maxEntries];
            maxHistoryEntries = maxEntries;
            historyEntryCount = entryCount;

            // Copy the provided data into the local arrays
            for (int i = 0; i < entryCount; i++)
            {
                resultArray[i] = results[i];
                operationArray[i] = operations[i];
            }

            // Initialize the form UI
            InitializeComponent();
            // Load the array data into the grid view
            LoadArrayData();
        }

        /// <summary>
        /// Initializes all UI components for the array data form.
        /// Sets up the form properties, title label, data grid view, and buttons.
        /// </summary>
        private void InitializeComponent()
        {
            // Configure form properties
            this.Text = "Edit Array Data";
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterScreen; // Center the form on screen
            this.BackColor = Color.LightGray;
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // Fixed size dialog
            this.MaximizeBox = false; // Cannot maximize this form

            titleLabel = new Label();
            titleLabel.Text = "Array Data Editor - Edit Results and Operations";
            titleLabel.Location = new Point(20, 10);
            titleLabel.Size = new Size(560, 30);
            titleLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(titleLabel);

            // Configure the data grid view for displaying and editing array data
            arrayDataGridView = new DataGridView();
            arrayDataGridView.Location = new Point(20, 50);
            arrayDataGridView.Size = new Size(560, 350);
            arrayDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Auto-size columns
            arrayDataGridView.AllowUserToAddRows = true;   // Allow users to add new rows
            arrayDataGridView.AllowUserToDeleteRows = true; // Allow users to delete rows
            arrayDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue; // Header background
            arrayDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold); // Header font
            this.Controls.Add(arrayDataGridView);

            saveButton = new Button();
            saveButton.Text = "Save Changes";
            saveButton.Location = new Point(20, 410);
            saveButton.Size = new Size(200, 40);
            saveButton.Font = new Font("Arial", 11, FontStyle.Bold);
            saveButton.BackColor = Color.LightGreen;
            saveButton.ForeColor = Color.DarkGreen;
            saveButton.Click += SaveButton_Click;
            this.Controls.Add(saveButton);

            cancelButton = new Button();
            cancelButton.Text = "Cancel";
            cancelButton.Location = new Point(380, 410);
            cancelButton.Size = new Size(200, 40);
            cancelButton.Font = new Font("Arial", 11, FontStyle.Bold);
            cancelButton.BackColor = Color.LightCoral;
            cancelButton.ForeColor = Color.DarkRed;
            cancelButton.Click += CancelButton_Click;
            this.Controls.Add(cancelButton);
        }

        /// <summary>
        /// Loads the array data into the data grid view for display and editing.
        /// Creates columns for Index, Operation, and Result, then populates rows with array data.
        /// </summary>
        private void LoadArrayData()
        {
            // Clear any existing columns and rows
            arrayDataGridView.Columns.Clear();
            arrayDataGridView.Rows.Clear();

            // Create and configure the Index column (read-only, shows array index)
            DataGridViewTextBoxColumn indexColumn = new DataGridViewTextBoxColumn();
            indexColumn.HeaderText = "Index";
            indexColumn.Name = "Index";
            indexColumn.ReadOnly = true; // Index cannot be edited
            indexColumn.Width = 60;
            arrayDataGridView.Columns.Add(indexColumn);

            // Create and configure the Operation column (editable, shows operation strings)
            DataGridViewTextBoxColumn operationColumn = new DataGridViewTextBoxColumn();
            operationColumn.HeaderText = "Operation (String Array)";
            operationColumn.Name = "Operation";
            operationColumn.Width = 300;
            arrayDataGridView.Columns.Add(operationColumn);

            // Create and configure the Result column (editable, shows numeric results)
            DataGridViewTextBoxColumn resultColumn = new DataGridViewTextBoxColumn();
            resultColumn.HeaderText = "Result (Double Array)";
            resultColumn.Name = "Result";
            resultColumn.Width = 200;
            arrayDataGridView.Columns.Add(resultColumn);

            // Populate rows with data from the arrays
            for (int i = 0; i < historyEntryCount; i++)
            {
                // Add row with index (1-based for display), operation string, and result value
                arrayDataGridView.Rows.Add(i + 1, operationArray[i], resultArray[i].ToString());
            }
        }

        /// <summary>
        /// Event handler for the Save button click.
        /// Reads data from the grid view, validates it, and updates the arrays.
        /// Validates that result values are valid numbers before saving.
        /// </summary>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                int newEntryCount = 0;

                // Iterate through all rows in the grid (excluding the empty new row at the end)
                for (int i = 0; i < arrayDataGridView.Rows.Count - 1; i++)
                {
                    // Stop if we've reached the maximum capacity
                    if (newEntryCount >= maxHistoryEntries)
                    {
                        break;
                    }

                    DataGridViewRow row = arrayDataGridView.Rows[i];
                    // Check if both Operation and Result cells have values
                    if (row.Cells["Operation"].Value != null && row.Cells["Result"].Value != null)
                    {
                        string operation = row.Cells["Operation"].Value.ToString();
                        // Validate that the Result value is a valid number
                        if (double.TryParse(row.Cells["Result"].Value.ToString(), out double result))
                        {
                            // Save the validated data to the arrays
                            operationArray[newEntryCount] = operation;
                            resultArray[newEntryCount] = result;
                            newEntryCount++;
                        }
                        else
                        {
                            // Show error if result is not a valid number
                            MessageBox.Show($"Invalid number format in row {i + 1}. Please enter a valid number.",
                                "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return; // Stop saving if validation fails
                        }
                    }
                }

                // Update the entry count and show success message
                historyEntryCount = newEntryCount;
                MessageBox.Show($"Array data saved successfully. {historyEntryCount} entries updated.",
                    "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Indicate successful save
                this.Close();
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors during save
                MessageBox.Show($"Error saving array data: {ex.Message}",
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

        // Public getter methods to retrieve the array data after form closes
        /// <summary>Gets the result array containing calculation results.</summary>
        public double[] GetResultArray()
        {
            return resultArray;
        }

        /// <summary>Gets the operation array containing operation strings.</summary>
        public string[] GetOperationArray()
        {
            return operationArray;
        }

        /// <summary>Gets the current number of entries stored in the arrays.</summary>
        public int GetHistoryEntryCount()
        {
            return historyEntryCount;
        }
    }
}
