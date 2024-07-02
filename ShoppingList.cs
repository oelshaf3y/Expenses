using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Expenses
{
    public class ShoppingList
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public List<ShopingItem> items { get; set; } = new List<ShopingItem>();

        [JsonConstructor]
        public ShoppingList(string name, double value = 0, List<ShopingItem> items = null)
        {
            this.Name = name;
            this.Value = value;
            if (items == null)
            {
                this.items = new List<ShopingItem>();
            }
            else
            {

                this.items = items;
            }
        }
        public void addItem(ShopingItem item)
        {
            items.Add(item);
            Value += item.Value;
        }
        public void removeItem(ShopingItem item)
        {
            Value -= item.Value;
            items.Remove(item);
        }
        public override string ToString()
        {
            return this.Name;
        }
    }

    public class ShopingItem
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public int Count { get; set; }
        [JsonConstructor]
        public ShopingItem(string name, double value = 0, int count = 1)
        {
            this.Name = name;
            this.Value = value;
            this.Count = count;
        }
    }
}
