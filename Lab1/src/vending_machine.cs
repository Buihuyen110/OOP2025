using System;
using System.Collections.Generic;

class Product
{
    public string Name { get; set; } = "";
    public int Price { get; set; }
    public int Quantity { get; set; }
}

public class VendingMachine
{
    private List<Product> products = new List<Product>();
    private int balance = 0;
    private int collectedMoney = 0;
    private const string adminPassword = "1234";

    public VendingMachine()
    {
        products.Add(new Product { Name = "Сырок", Price = 50, Quantity = 5 });
        products.Add(new Product { Name = "Чипсы", Price = 100, Quantity = 5 });
        products.Add(new Product { Name = "Вода", Price = 75, Quantity = 10 });
        products.Add(new Product { Name = "Сэндвич", Price = 180, Quantity = 5 });
        products.Add(new Product { Name = "Вафли", Price = 80, Quantity = 5 });
        products.Add(new Product { Name = "Сухарики", Price = 80, Quantity = 5 });
    }

    public void ShowProducts()
    {
        Console.WriteLine("\nСписок товаров:");
        for (int i = 0; i < products.Count; i++)
        {
            Console.WriteLine($"{i+1}. {products[i].Name} - {products[i].Price} руб. (осталось {products[i].Quantity})");
        }
    }

    public void InsertCoin(int coin)
    {
        balance += coin;
        Console.WriteLine($"Внесено: {coin} руб. Текущий баланс: {balance} руб.");
    }

    public void SelectProduct(int index)
    {
        var product = products[index - 1];
        if (product.Quantity <= 0)
        {
            Console.WriteLine("Товар закончился!");
            return;
        }
    
        if (balance >= product.Price)
        {
            balance -= product.Price;      
            collectedMoney += product.Price;
            product.Quantity--;
            Console.WriteLine($"Вы купили: {product.Name}");
        }
        else
        {
        Console.WriteLine($"Недостаточно средств. Требуется {product.Price} рублей.");
        }
    }

    public void CancelOperation()
    {
        if (balance > 0)
        {
            Console.WriteLine($"Операция отменена. Возврат {balance} руб.");
            balance = 0;
        }
        else
        {
            Console.WriteLine("Баланс пуст.");
        }
    }

    public void AdminMode()
    {
        Console.Write("Введите пароль администратора: ");
        string pass = Console.ReadLine();

        if (pass != adminPassword)
        {
            Console.WriteLine("Неверный пароль!");
            return;
        }

        Console.WriteLine("=== Админ-меню ===");
        Console.WriteLine("1. Пополнить товары");
        Console.WriteLine("2. Собрать деньги");
        string choice = Console.ReadLine();

        if (choice == "1")
        {
            ShowProducts();
            Console.Write("Введите номер товара: ");
            string? inputId = Console.ReadLine();
            if (inputId != null && int.TryParse(inputId, out int id))
            {
                Console.Write("Введите количество для добавления: ");
                string? inputQty = Console.ReadLine();
                if (inputQty != null && int.TryParse(inputQty, out int qty))
                {
                    products[id - 1].Quantity += qty;
                    Console.WriteLine("Товар пополнен!");
                }
                else
                {
                    Console.WriteLine("Ошибка ввода количества!");
                }
            }
            else
            {
                Console.WriteLine("Ошибка ввода номера товара!");
            }

        }
        else if (choice == "2")
        {
            Console.WriteLine($"Собрано {collectedMoney} руб.");
            collectedMoney = 0;
        }
    }
        // Trả về số tiền hiện tại
    public int GetBalance() => balance;

        // Trả về tổng số tiền máy đã thu
    public int GetCollectedMoney() => collectedMoney;

        // Trả về số lượng sản phẩm theo index
    public int GetProductQuantity(int index) => products[index - 1].Quantity;

        // Trả về giá sản phẩm theo index
    public int GetProductPrice(int index) => products[index - 1].Price;

        // Admin helper: thêm sản phẩm
    public void AdminAddProduct(int index, int qty)
    {
        products[index - 1].Quantity += qty;
    }

        // Admin helper: thu tiền
    public int AdminCollectMoney()
    {
        int money = collectedMoney;
        collectedMoney = 0;
        return money;
    }
}

class Program
{
    static void Main()
    {
        VendingMachine vm = new VendingMachine();
        while (true)
        {
            Console.WriteLine("\n=== Вендинговый автомат ===");
            Console.WriteLine("1. Показать товары");
            Console.WriteLine("2. Внести монету");
            Console.WriteLine("3. Выбрать товар");
            Console.WriteLine("4. Отменить операцию");
            Console.WriteLine("5. Админ-режим");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    vm.ShowProducts();
                    break;
                case "2":
                    Console.Write("Введите номинал монеты: ");
                    int coin = int.Parse(Console.ReadLine());
                    vm.InsertCoin(coin);
                    break;
                case "3":
                    Console.Write("Введите номер товара: ");
                    int index = int.Parse(Console.ReadLine());
                    vm.SelectProduct(index);
                    break;
                case "4":
                    vm.CancelOperation();
                    break;
                case "5":
                    vm.AdminMode();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный ввод!");
                    break;
            }
        }
    }
}

