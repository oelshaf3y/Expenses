using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Expenses
{
    class Record
    {
        public string name { get; set; }
        public double value { get; set; }
        public int index { get; set; }
        public string parent { get; set; }
        public DateTime date { get; set; } 
        public User user { get; set; }

        [JsonConstructor]
        public Record(string name, double value, string parent,DateTime date, int index = -1)
        {
            this.name = name;
            this.value = value;
            this.index = index;
            this.parent = parent;
            this.date = date;
            this.user = DB.Instance.currentUser;
        }
    }
}
