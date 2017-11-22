using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;

namespace LineChartTest
{
    /// <summary>
    /// Interaction logic for LoanCalculator.xaml
    /// </summary>
    public partial class LoanCalculator : UserControl
    {
        public LoanCalculator()
        {
            InitializeComponent();
            DataContext = this;

            YFormatter = value => value.ToString("C");
            LoanList = new List<Loan>();
            SeriesCollection = new SeriesCollection();
        }

        public decimal AddLoanAndCalculate(decimal principleBalance, decimal unpaidInterest, decimal rate, decimal payment)
        {
            var loan1 = new Loan(principleBalance, unpaidInterest, rate, payment);
            LoanList.Add(loan1);
            
            return CalculateLoanSeries(loan1);
        }

        public decimal CalculateLoanSeries(Loan loan1)
        {
            var startDate = DateTime.Today;
            var endDate = new DateTime(2025, 12, 31);

            var currentPrincipleBalance = loan1.InitialPrincipleBalance;
            var currentAccruedInterest = loan1.InitialAccruedInterest;
            var payment = loan1.MontlyPayment;

            var totalPayment = 0m;
            
            var ls = new LineSeries
            {
                Title = "Series 1",
                Values = new ChartValues<decimal>(),
                PointGeometry = DefaultGeometries.Square,
                PointGeometrySize = 10
            };

            //plot the starting balances
            ls.Values.Add(currentPrincipleBalance + currentAccruedInterest);

            while (startDate < endDate)
            {
                //apply monthly payment
                if (startDate.Day == 11)
                {
                    var monthlyPayment = payment;
                    //compare accrued interest to the montly payment amount
                    if (currentAccruedInterest <= monthlyPayment)
                    {
                        monthlyPayment -= currentAccruedInterest;
                        totalPayment += currentAccruedInterest;
                        currentAccruedInterest -= currentAccruedInterest;

                        //compare remaining loan balance to the payment amount
                        if (currentPrincipleBalance >= monthlyPayment)
                        {
                            currentPrincipleBalance -= monthlyPayment;
                            totalPayment += monthlyPayment;
                            monthlyPayment -= monthlyPayment;
                        }
                        //special logic for if you're paying off more than the remaining balance
                        else
                        {
                            monthlyPayment -= currentPrincipleBalance;
                            totalPayment += currentPrincipleBalance;
                            currentPrincipleBalance -= currentPrincipleBalance;
                        }
                    }
                    //special logic for if you're paying off less then the accrued interest
                    else
                    {
                        currentPrincipleBalance -= monthlyPayment;
                        totalPayment += monthlyPayment;
                        monthlyPayment -= monthlyPayment;
                    }
                    
                    //plot the point
                    ls.Values.Add(currentPrincipleBalance + currentAccruedInterest);
                }

                //ls.Values.Add(currentPrincipleBalance + currentAccruedInterest);

                currentPrincipleBalance += currentPrincipleBalance * (loan1.InterestRate / 365);
                startDate = startDate.AddDays(1);
            }

            SeriesCollection.Add(ls);
            
            return totalPayment;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public Func<double, string> YFormatter { get; set; }

        private List<Loan> LoanList;
    }
}
