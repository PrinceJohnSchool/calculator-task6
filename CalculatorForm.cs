using System;
using System.Drawing;
using System.Windows.Forms;

namespace CalculatorApp
{
    public partial class CalculatorForm : Form
    {
        private TextBox number1TextBox;
        private TextBox number2TextBox;
        private Button addButton;
        private Button subtractButton;
        private Button multiplyButton;
        private Button divideButton;
        private Label resultLabel;

        private double firstNumber;
        private double secondNumber;
        private double calculationResult;

        public CalculatorForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Simple Calculator";
            this.Size = new Size(400, 300);
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
            this.Controls.Add(number1TextBox);

            number2TextBox = new TextBox();
            number2TextBox.Location = new Point(200, 30);
            number2TextBox.Size = new Size(150, 25);
            number2TextBox.Font = new Font("Arial", 12);
            number2TextBox.TextAlign = HorizontalAlignment.Center;
            number2TextBox.PlaceholderText = "Enter second number";
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

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                calculationResult = firstNumber + secondNumber;
                resultLabel.Text = $"Result: {firstNumber} + {secondNumber} = {calculationResult}";
                resultLabel.ForeColor = Color.Black;
            }
        }

        private void SubtractButton_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                calculationResult = firstNumber - secondNumber;
                resultLabel.Text = $"Result: {firstNumber} − {secondNumber} = {calculationResult}";
                resultLabel.ForeColor = Color.Black;
            }
        }

        private void MultiplyButton_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                calculationResult = firstNumber * secondNumber;
                resultLabel.Text = $"Result: {firstNumber} × {secondNumber} = {calculationResult}";
                resultLabel.ForeColor = Color.Black;
            }
        }

        private void DivideButton_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                if (secondNumber == 0)
                {
                    resultLabel.Text = "Error: Cannot divide by zero. Please enter a non-zero second number.";
                    resultLabel.ForeColor = Color.Red;
                }
                else
                {
                    calculationResult = firstNumber / secondNumber;
                    resultLabel.Text = $"Result: {firstNumber} ÷ {secondNumber} = {calculationResult}";
                    resultLabel.ForeColor = Color.Black;
                }
            }
        }
    }
}
