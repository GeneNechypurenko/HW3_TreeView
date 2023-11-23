using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace TipsCalculator
{
    public partial class MainWindow : Window
    {
        private decimal tipPercentage = 1;
        private readonly Regex numericRegex = new Regex(@"^\d+(\.\d+)?$");

        public MainWindow()
        {
            InitializeComponent();
            calculateButton.IsEnabled = false;

            calculateButton.Click += (s, e) =>
            {
                if (numericRegex.IsMatch(inputTextBox.Text))
                {
                    decimal dinnerPrice = decimal.Parse(inputTextBox.Text);
                    outputTextBlock.Text = (dinnerPrice * tipPercentage).ToString("C");
                    calculateButton.IsEnabled = false;
                    inputTextBox.Clear();
                }
                else { MessageBox.Show("Invalid input. Enter valid dinner's prise."); }
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button tipButton)
            {
                tipPercentage *= 1 / decimal.Parse(tipButton.Tag.ToString());
                calculateButton.IsEnabled = true;
            }
        }
    }
}
