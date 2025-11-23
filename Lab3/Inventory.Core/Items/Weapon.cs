using Inventory.Core.Interfaces;

namespace Inventory.Core.Items
{
    public class Weapon : Item, IEquipable, IUpgradable
    {
        public int Damage { get; private set; }
        private bool isEquipped = false;

        public Weapon(string name, int damage)
        {
            Name = name;
            Damage = damage;
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
            Damage += 5;
            System.Console.WriteLine($"{Name} upgraded! Damage is now {Damage}");
        }
    }
}
