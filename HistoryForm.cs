using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CalculatorApp
{
    /// <summary>
    /// Form that displays the calculation history to the user.
    /// Shows all previous calculations in a list format with options to clear history.
    /// </summary>
    public partial class HistoryForm : Form
    {
        // UI Controls
        private ListBox historyListBox;        // Displays the list of calculation history entries
        private Button clearHistoryButton;     // Button to clear all history entries
        private Button closeButton;            // Button to close this form
        private Label titleLabel;              // Title label at the top of the form
        
        // Data
        private List<string> calculationHistory; // List storing all calculation history strings

        /// <summary>
        /// Constructor for HistoryForm.
        /// Initializes the form with the provided calculation history.
        /// </summary>
        /// <param name="history">List of calculation history strings to display. If null, creates an empty list.</param>
        public HistoryForm(List<string> history)
        {
            // Initialize history list (use provided list or create empty one if null)
            calculationHistory = history ?? new List<string>();
            // Set up the form's UI components
            InitializeComponent();
            // Load and display the history entries
            LoadHistory();
        }

        /// <summary>
        /// Initializes all UI components for the history form.
        /// Sets up the form properties, labels, list box, and buttons.
        /// </summary>
        private void InitializeComponent()
        {
            // Configure form properties
            this.Text = "Calculation History";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen; // Center the form on screen
            this.BackColor = Color.LightGray;
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // Fixed size dialog
            this.MaximizeBox = false; // Cannot maximize this form

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

        /// <summary>
        /// Loads the calculation history into the display.
        /// Calls RefreshHistoryDisplay to update the list box.
        /// </summary>
        private void LoadHistory()
        {
            RefreshHistoryDisplay();
        }

        /// <summary>
        /// Event handler for the Clear History button click.
        /// Clears all history entries when the user clicks the button.
        /// </summary>
        private void ClearHistoryButton_Click(object sender, EventArgs e)
        {
            ClearHistoryList();
        }

        /// <summary>
        /// Event handler for the Close button click.
        /// Closes the history form when the user clicks the button.
        /// </summary>
        private void CloseButton_Click(object sender, EventArgs e)
        {
            CloseHistoryForm();
        }

        /// <summary>
        /// Clears all entries from the calculation history list.
        /// Also refreshes the display to show the empty list.
        /// </summary>
        private void ClearHistoryList()
        {
            calculationHistory.Clear();
            RefreshHistoryDisplay();
        }

        /// <summary>
        /// Closes the history form.
        /// </summary>
        private void CloseHistoryForm()
        {
            this.Close();
        }

        /// <summary>
        /// Refreshes the history list box display.
        /// Clears existing items and repopulates with numbered history entries.
        /// </summary>
        private void RefreshHistoryDisplay()
        {
            historyListBox.Items.Clear();
            // Add each history entry with a number prefix (1., 2., etc.)
            for (int i = 0; i < calculationHistory.Count; i++)
            {
                historyListBox.Items.Add($"{i + 1}. {calculationHistory[i]}");
            }
        }
    }
}
