using Inventory.Core.Items;

namespace Inventory.Core.Patterns.Builder
{
    public class WeaponBuilder
    {
        private string? name;
        private int damage = 0;

        public WeaponBuilder SetName(string? name) { this.name = name; return this; }
        public WeaponBuilder SetDamage(int damage) { this.damage = damage; return this; }

        public Weapon Build()
        {
            if (string.IsNullOrEmpty(name))
                throw new System.InvalidOperationException("Weapon name is required");
            
            return new Weapon(name, damage);
        }
    }
}
