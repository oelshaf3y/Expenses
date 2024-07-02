using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Expenses
{
    class User
    {
        public string name { get; set; }
        private string password { get; set; }
        private Balance Balance { get; set; }
        [JsonConstructor]
        public User(string name)
        {
            this.name = name;
        }
        public void adjustBalance(int index,double value)
        {
            if (index == 1) Balance.addBalance(value);
            else Balance.pay(value);
        }
        public override string ToString()
        {
            return name;
        }
    }
}
