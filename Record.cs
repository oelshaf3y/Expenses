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
    public class Record
    {
        public string Info { get; set; }
        public double Value { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public cashFlow Transaction { get; set; }
        public Person User { get; set; }
        public ShoppingList GroceryList { get; set; }

        [JsonConstructor]
        public Record(string info, double value, string category, DateTime date, cashFlow transaction, Person user,
        ShoppingList? GroceryList = null)
        {
            this.Info = info;
            this.Value = value;
            this.Category = category;
            this.Date = date;
            this.Transaction = transaction;
            this.User = user;
            this.GroceryList = GroceryList;
        }
    }
}
