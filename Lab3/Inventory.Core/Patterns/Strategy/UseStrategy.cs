using Inventory.Core.Items;

namespace Inventory.Core.Patterns.Strategy
{
    public interface IUseStrategy
    {
        void Use(Item item);
    }
}
