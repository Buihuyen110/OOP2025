using System;
using Xunit;

namespace VendingMachineTests
{
    public class VendingMachineUnitTests
    {
        // Тест 1: проверка, что вставка монеты увеличивает баланс
        [Fact]
        public void InsertCoin_test()
        {
            var vm = new VendingMachine();
            vm.InsertCoin(100); // вставляем 100 единиц
            Assert.Equal(100, vm.GetBalance()); // проверяем баланс
        }

        // Тест 2: покупка товара при достаточном балансе
        [Fact]
        public void SelectProduct_test1()
        {
            var vm = new VendingMachine();
            vm.InsertCoin(100); 
            int initialQty = vm.GetProductQuantity(2); 
            vm.SelectProduct(2); 
            Assert.Equal(initialQty - 1, vm.GetProductQuantity(2)); // количество должно уменьшиться на 1
            Assert.Equal(0, vm.GetBalance()); // баланс после покупки должен быть 0
        }

        // Тест 3: попытка покупки при недостаточном балансе не изменяет количество товара
        [Fact]
        public void SelectProduct_test2()
        {
            var vm = new VendingMachine();
            int initialQty = vm.GetProductQuantity(1);
            vm.SelectProduct(1); // пытаемся купить без денег
            Assert.Equal(initialQty, vm.GetProductQuantity(1)); // количество не должно измениться
        }

        
        // Тест 4: отмена операции обнуляет баланс
        [Fact]
        public void CancelOperation_test()
        {
            var vm = new VendingMachine();
            vm.InsertCoin(30);
            vm.CancelOperation(); // отмена
            Assert.Equal(0, vm.GetBalance());
        }

        // Тест 5: администратор добавляет товар, количество увеличивается
        [Fact]
        public void Admin_AddProduct_test()
        {
            var vm = new VendingMachine();
            int initialQty = vm.GetProductQuantity(1);
            vm.AdminAddProduct(1, 5); // добавляем 5 единиц товара №1
            Assert.Equal(initialQty + 5, vm.GetProductQuantity(1));
        }

        // Тест 6: администратор собирает деньги, собранная сумма сбрасывается
        [Fact]
        public void Admin_CollectMoney_test()
        {
            var vm = new VendingMachine();
            vm.InsertCoin(50);
            vm.SelectProduct(1); 
            int collected = vm.AdminCollectMoney(); // собираем деньги
            Assert.Equal(50, collected); // проверяем собранную сумму
            Assert.Equal(0, vm.GetCollectedMoney()); // проверяем, что собранная сумма сброшена
        }

        // Тест 7: попытка купить товар с количеством 0 не разрешается
        [Fact]
        public void SelectProduct_test1()
        {
            var vm = new VendingMachine();
            int index = 2;
            int qty = vm.GetProductQuantity(index);
            vm.AdminAddProduct(index, -qty); // устанавливаем количество в 0
            vm.InsertCoin(vm.GetProductPrice(index)); 
            vm.SelectProduct(index); // 
            Assert.Equal(0, vm.GetProductQuantity(index)); // количество остаётся 0
            Assert.Equal(vm.GetProductPrice(index), vm.GetBalance()); // баланс не уменьшается
        }


        // Тест 8: покупка товара с остатком денег
        [Fact]
        public void SelectProduct_test2()
        {
            var vm = new VendingMachine();
            vm.InsertCoin(100); // вставляем больше, чем цена товара
            int price = vm.GetProductPrice(5); 
            vm.SelectProduct(5); 
            Assert.Equal(100 - price, vm.GetBalance()); // баланс должен быть равен остатку
        }
    }
}
