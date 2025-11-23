using Inventory.Core.Interfaces;
using Inventory.Core.Items;

namespace Inventory.Core.Patterns.Strategy
{
    public class ConsumeStrategy : IUseStrategy
    {
        public void Use(Item item)
        {
            if(item is IConsumable consumable)
                consumable.Consume();
        }
    }
}
