using Inventory.Core.Items;

namespace Inventory.Core.Patterns.State
{
    public class EquippedState : IItemState
    {
        public void Handle(Weapon weapon) => weapon.Unequip();
    }
}
