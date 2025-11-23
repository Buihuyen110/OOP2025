using Inventory.Core.Items;

namespace Inventory.Core.Patterns.State
{
    public class UnequippedState : IItemState
    {
        public void Handle(Weapon weapon) => weapon.Equip();
    }
}
