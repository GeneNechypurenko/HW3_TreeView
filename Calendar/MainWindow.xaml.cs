using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calendar
{
    public partial class MainWindow : Window
    {
        private int currentMonth = 0;
        public MainWindow()
        {
            InitializeComponent();
            DisplayCalendar(DateTime.Now);
            previousMonthButton.Click += (s, e) => DisplayCalendar(DateTime.Now.AddMonths(--currentMonth));
            nextMonthButton.Click += (s, e) => DisplayCalendar(DateTime.Now.AddMonths(++currentMonth));
        }
        private void DisplayCalendar(DateTime monthToShow)
        {
            calendarGrid.Children.Clear();

            for (int i = 1; i <= DateTime.DaysInMonth(monthToShow.Year, monthToShow.Month); i++)
            {
                var dayButton = new Button
                {
                    Content = i.ToString(),
                    Tag = new DateTime(monthToShow.Year, monthToShow.Month, i)
                };
                dayButton.FontSize = 30;
                dayButton.FontWeight = FontWeights.Bold;

                dayButton.Click += (s, e) =>
                {
                    var selectedDate = (DateTime)dayButton.Tag;
                    MessageBox.Show($"date selected: {selectedDate.ToShortDateString()}");
                };

                calendarGrid.Children.Add(dayButton);
            }
            monthTextBlock.Text = monthToShow.ToString("MMMM yyyy");
        }
    }
}