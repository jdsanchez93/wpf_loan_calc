using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;


namespace LineChartTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var testTotal = LoanChart.AddLoanAndCalculate(6743.46m, 31.46m, .0655m, 90.26m + 50m);
            TestTextBox.Text = $"Total Payments: {testTotal}";

            testTotal = LoanChart.AddLoanAndCalculate(6743.46m, 31.46m, .0655m, 90.26m);
            TestTextBox.Text += $"\r\nTotal Payments: {testTotal}";

            testTotal = LoanChart.AddLoanAndCalculate(6743.46m, 31.46m, .0655m, 6743.46m);
            TestTextBox.Text += $"\r\nTotal Payments: {testTotal}";
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var principle = Decimal.Parse(PrincipleTextBox.Text);
                var interest = Decimal.Parse(InterestTextBox.Text);
                var rate = Decimal.Parse(RateTextBox.Text);
                var payment = Decimal.Parse(MonthlyPaymentTextBox.Text);

                var testTotal = LoanChart.AddLoanAndCalculate(principle, interest, rate, payment);

                TestTextBox.Text += $"\r\nTotal Payments: {testTotal}";
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
