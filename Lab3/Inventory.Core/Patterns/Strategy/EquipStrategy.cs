using Inventory.Core.Interfaces;
using Inventory.Core.Items;

namespace Inventory.Core.Patterns.Strategy
{
    public class EquipStrategy : IUseStrategy
    {
        public void Use(Item item)
        {
            if(item is IEquipable equipable)
                equipable.Equip();
        }
    }
}
