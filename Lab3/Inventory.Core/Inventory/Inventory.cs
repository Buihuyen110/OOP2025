using System.Collections.Generic;
using Inventory.Core.Items;
using Inventory.Core.Patterns.Strategy;

namespace Inventory.Core.Inventory
{
    public class InventoryManager
    {
        private List<Item> items = new();

        public void AddItem(Item item) => items.Add(item);
        public void RemoveItem(Item item) => items.Remove(item);

        public void UseItem(Item item, IUseStrategy strategy)
        {
            strategy.Use(item);
        }

        public void ShowInventory()
        {
            System.Console.WriteLine("Inventory:");
            foreach(var item in items)
                System.Console.WriteLine($"- {item.Name}");
        }

        public List<Item> GetItems() => new List<Item>(items);

    }
}
