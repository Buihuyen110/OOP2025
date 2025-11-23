namespace Inventory.Core.Items
{
    public abstract class Item
    {
        public string? Name { get; protected set; }
        public string? Description { get; protected set; }
    }
}
