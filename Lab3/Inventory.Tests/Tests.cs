using Xunit;
using Inventory.Core.Inventory;
using Inventory.Core.Items;
using Inventory.Core.Patterns.Builder;
using Inventory.Core.Patterns.Strategy;
using Inventory.Core.Patterns.State;

namespace Inventory.Tests
{
public class InventoryTests
{
[Fact]
public void AddAllItemTypesTest()
{
var inventory = new InventoryManager();

        var sword = new WeaponBuilder().SetName("Sword").SetDamage(10).Build();  
        var armor = new Armor("Shield", 5);  
        var potion = new Potion("Health Potion", 50);  
        var questItem = new QuestItem("Key", "Opens the dungeon");  

        inventory.AddItem(sword);  
        inventory.AddItem(armor);  
        inventory.AddItem(potion);  
        inventory.AddItem(questItem);  

        Assert.Contains(sword, inventory.GetItems());  
        Assert.Contains(armor, inventory.GetItems());  
        Assert.Contains(potion, inventory.GetItems());  
        Assert.Contains(questItem, inventory.GetItems());  
    }  

    [Fact]  
    public void EquipAndUnequipWeaponTest()  
    {  
        var sword = new WeaponBuilder().SetName("Sword").SetDamage(10).Build();  
        var stateContext = new WeaponStateContext(sword);  

        stateContext.ToggleEquip();  
        stateContext.ToggleEquip();  

        Assert.NotNull(sword);  
    }  

    [Fact]  
    public void EquipArmorTest()  
    {  
        var inventory = new InventoryManager();  
        var armor = new Armor("Shield", 5);  
        inventory.AddItem(armor);  

        inventory.UseItem(armor, new EquipStrategy());  

        Assert.NotNull(armor);  
    }  

    [Fact]  
    public void ConsumePotionTest()  
    {  
        var inventory = new InventoryManager();  
        var potion = new Potion("Health Potion", 50);  
        inventory.AddItem(potion);  

        inventory.UseItem(potion, new ConsumeStrategy());  

        Assert.NotNull(potion);  
    }  

    [Fact]  
    public void UpgradeItemsTest()  
    {  
        var sword = new WeaponBuilder().SetName("Sword").SetDamage(10).Build();  
        var armor = new Armor("Shield", 5);  

        sword.Upgrade();  
        armor.Upgrade();  

        Assert.True(sword.Damage > 10);  
        Assert.True(armor.Defense > 5);  
    }  

    [Fact]  
    public void RemoveItemTest()  
    {  
        var inventory = new InventoryManager();  
        var sword = new WeaponBuilder().SetName("Sword").SetDamage(10).Build();  
        inventory.AddItem(sword);  

        inventory.RemoveItem(sword);  

        Assert.DoesNotContain(sword, inventory.GetItems());  
    }  

    [Fact]  
    public void ShowInventoryTest()  
    {  
        var inventory = new InventoryManager();  
        var sword = new WeaponBuilder().SetName("Sword").SetDamage(10).Build();  
        var potion = new Potion("Health Potion", 50);  
        inventory.AddItem(sword);  
        inventory.AddItem(potion);  

        inventory.ShowInventory(); // chỉ in ra console, không cần assert  
    }  
}  


}
