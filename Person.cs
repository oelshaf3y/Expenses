using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Expenses
{
    public class Person
    {
        public string initials { get; set; }
        public string name { get; set; }
        //private Balance Balance { get; set; }
        [JsonConstructor]
        public Person(string name)
        {
            this.name = name;
            this.initials =string.Join("",name.Split(' ').Select(x=>x.First()));
        }
        //public void adjustBalance(int index,double value)
        //{
        //    if (index == 1) Balance.addBalance(value);
        //    else Balance.pay(value);
        //}
        public override string ToString()
        {
            return name;
        }
    }
}
