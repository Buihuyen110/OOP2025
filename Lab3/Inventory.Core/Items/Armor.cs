using Inventory.Core.Interfaces;

namespace Inventory.Core.Items
{
    public class Armor : Item, IEquipable, IUpgradable
    {
    public int Defense { get; private set; }
    private bool isEquipped = false;

        public Armor(string name, int defense)
        {
            Name = name;
            Defense = defense;
        }

        public void Equip()
        {
            if (!isEquipped)
            {
                isEquipped = true;
                System.Console.WriteLine($"{Name} equipped!");
            }
        }

        public void Unequip()
        {
            if (isEquipped)
            {
                isEquipped = false;
                System.Console.WriteLine($"{Name} unequipped!");
            }
        }

        public void Upgrade()
        {
            Defense += 3;
            System.Console.WriteLine($"{Name} upgraded! Defense is now {Defense}");
        }
    }

}
