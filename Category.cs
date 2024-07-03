using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Expenses
{
    class Category
    {
        public string name { get; set; }
        public string description { get; set; }
        public List<Category> children { get; set; } = new List<Category>();
        public List<Record> items { get; set; } = new List<Record>();
        public string parent { get; set; } = null;
        [JsonConstructor]
        public Category(string name, string description = null, List<Category> children = null, List<Record> items = null, string parent = null)
        {
            this.name = name;
            this.description = description;

            if (children == null) children = new List<Category>();
            else this.children = children;
            if (items == null) this.items = new List<Record>();
            else this.items = items;
            this.parent = parent;
        }
        public string getFullName()
        {
            if (parent == null) return name;
            Category parentCat = DB.flatten(DB.Instance.categories).Where(x => x.name == parent).First();
            return parentCat.getFullName() + "." + name;
        }
        public void AddSubCategory(Category subCategory)
        {
            children.Add(subCategory);
        }
        public void RemoveSubCategory(Category subCategory)
        {
            children.Remove(subCategory);
        }
        public void AddItem(Record item)
        {
            items.Add(item);
        }
        public void RemoveItem(Record item)
        {
            items.Remove(item);
        }
    }
}
