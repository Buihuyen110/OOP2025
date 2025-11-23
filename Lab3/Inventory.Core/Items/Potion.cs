using Inventory.Core.Interfaces;

namespace Inventory.Core.Items
{
    public class Potion : Item, IConsumable
    {
        public int HealAmount { get; private set; }

        public Potion(string name, int heal)
        {
            Name = name;
            HealAmount = heal;
        }

        public void Consume()
        {
            System.Console.WriteLine($"{Name} consumed! Healed {HealAmount} HP.");
        }
    }
}
