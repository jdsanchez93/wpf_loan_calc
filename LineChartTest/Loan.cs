using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineChartTest
{
    public class Loan
    {
        public decimal InitialPrincipleBalance { get; }
        public decimal InitialAccruedInterest;
        public decimal InterestRate;

        public decimal MontlyPayment;

        /*
         * public int TotalDays;
         * public decimal TotalPayment;
         * public decimal TotalPaymentTowardsInterest;
         */

        public Loan(decimal principleBalance, decimal unpaidInterest, decimal rate, decimal payment)
        {
            InitialPrincipleBalance = principleBalance;
            InitialAccruedInterest = unpaidInterest;
            InterestRate = rate;

            MontlyPayment = payment;
        }
    }
}
