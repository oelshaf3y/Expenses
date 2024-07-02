using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenses
{
    class Balance
    {
        private double amount { get; set; }
        public Balance()
        {
            this.amount = 0;
        }
        public void addBalance(double value)
        {
            amount += value;
        }
        public void pay(double value)
        {
            amount -= value;
        }
    }
}
