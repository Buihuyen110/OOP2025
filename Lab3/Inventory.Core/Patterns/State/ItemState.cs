using Inventory.Core.Items;

namespace Inventory.Core.Patterns.State
{
    public interface IItemState
    {
        void Handle(Weapon weapon);
    }
}
