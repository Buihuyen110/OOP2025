using Inventory.Core.Items;

namespace Inventory.Core.Patterns.State
{
    public class WeaponStateContext
    {
        private IItemState state;
        private Weapon weapon;

        public WeaponStateContext(Weapon weapon)
        {
            this.weapon = weapon;
            state = new UnequippedState();
        }

        public void ToggleEquip()
        {
            state.Handle(weapon);
            state = state is UnequippedState ? (IItemState)new EquippedState() : new UnequippedState();
        }
    }
}
