using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Expenses
{

    public enum cashFlow { Expense, Income }
    class Record
    {
        public string Info { get; set; }
        public double Value { get; set; }
        public cashFlow Transaction { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }
        public ShoppingList GroceryList { get; set; }

        [JsonConstructor]
        public Record(string info, double value, string parent, DateTime date, cashFlow transaction, User user,
            ShoppingList? GroceryList = null)
        {
            this.Info = info;
            this.Value = value;
            this.Transaction = transaction;
            this.Category = parent;
            this.Date = date;
            this.User = user;
            this.GroceryList = GroceryList;
        }

    }
}
