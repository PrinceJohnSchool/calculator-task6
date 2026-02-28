using System;
using System.Drawing;
using System.Windows.Forms;

namespace CalculatorApp
{
    public partial class ArrayDataForm : Form
    {
        private DataGridView arrayDataGridView;
        private Button saveButton;
        private Button cancelButton;
        private Label titleLabel;
        private double[] resultArray;
        private string[] operationArray;
        private int historyEntryCount;
        private int maxHistoryEntries;

        public ArrayDataForm(double[] results, string[] operations, int entryCount, int maxEntries)
        {
            resultArray = new double[maxEntries];
            operationArray = new string[maxEntries];
            maxHistoryEntries = maxEntries;
            historyEntryCount = entryCount;

            for (int i = 0; i < entryCount; i++)
            {
                resultArray[i] = results[i];
                operationArray[i] = operations[i];
            }

            InitializeComponent();
            LoadArrayData();
        }

        private void InitializeComponent()
        {
            this.Text = "Edit Array Data";
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.LightGray;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            titleLabel = new Label();
            titleLabel.Text = "Array Data Editor - Edit Results and Operations";
            titleLabel.Location = new Point(20, 10);
            titleLabel.Size = new Size(560, 30);
            titleLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(titleLabel);

            arrayDataGridView = new DataGridView();
            arrayDataGridView.Location = new Point(20, 50);
            arrayDataGridView.Size = new Size(560, 350);
            arrayDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            arrayDataGridView.AllowUserToAddRows = true;
            arrayDataGridView.AllowUserToDeleteRows = true;
            arrayDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
            arrayDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
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

        private void LoadArrayData()
        {
            arrayDataGridView.Columns.Clear();
            arrayDataGridView.Rows.Clear();

            DataGridViewTextBoxColumn indexColumn = new DataGridViewTextBoxColumn();
            indexColumn.HeaderText = "Index";
            indexColumn.Name = "Index";
            indexColumn.ReadOnly = true;
            indexColumn.Width = 60;
            arrayDataGridView.Columns.Add(indexColumn);

            DataGridViewTextBoxColumn operationColumn = new DataGridViewTextBoxColumn();
            operationColumn.HeaderText = "Operation (String Array)";
            operationColumn.Name = "Operation";
            operationColumn.Width = 300;
            arrayDataGridView.Columns.Add(operationColumn);

            DataGridViewTextBoxColumn resultColumn = new DataGridViewTextBoxColumn();
            resultColumn.HeaderText = "Result (Double Array)";
            resultColumn.Name = "Result";
            resultColumn.Width = 200;
            arrayDataGridView.Columns.Add(resultColumn);

            for (int i = 0; i < historyEntryCount; i++)
            {
                arrayDataGridView.Rows.Add(i + 1, operationArray[i], resultArray[i].ToString());
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                int newEntryCount = 0;

                for (int i = 0; i < arrayDataGridView.Rows.Count - 1; i++)
                {
                    if (newEntryCount >= maxHistoryEntries)
                    {
                        break;
                    }

                    DataGridViewRow row = arrayDataGridView.Rows[i];
                    if (row.Cells["Operation"].Value != null && row.Cells["Result"].Value != null)
                    {
                        string operation = row.Cells["Operation"].Value.ToString();
                        if (double.TryParse(row.Cells["Result"].Value.ToString(), out double result))
                        {
                            operationArray[newEntryCount] = operation;
                            resultArray[newEntryCount] = result;
                            newEntryCount++;
                        }
                        else
                        {
                            MessageBox.Show($"Invalid number format in row {i + 1}. Please enter a valid number.",
                                "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                historyEntryCount = newEntryCount;
                MessageBox.Show($"Array data saved successfully. {historyEntryCount} entries updated.",
                    "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving array data: {ex.Message}",
                    "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public double[] GetResultArray()
        {
            return resultArray;
        }

        public string[] GetOperationArray()
        {
            return operationArray;
        }

        public int GetHistoryEntryCount()
        {
            return historyEntryCount;
        }
    }
}
