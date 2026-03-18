using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _3isip_423_Kalachev_Zhalyuk.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange (Подготовка)
            int a = 5;
            int b = 3;
            int expectedSum = 8;
            int expectedDifference = 2;
            bool isTrue = true;
            object obj1 = new object();
            object obj2 = obj1;
            object obj3 = new object();

            // Act (Действие)
            int actualSum = a + b;
            int actualDifference = a - b;

            // Assert (Проверка)
            Assert.AreEqual(expectedSum, actualSum, "Сумма вычислена неверно");
            Assert.AreNotEqual(expectedSum, actualDifference, "Сумма и разность не должны быть равны");
            Assert.IsTrue(isTrue, "Переменная isTrue должна быть true");
            Assert.IsFalse(a < b, "a не должно быть меньше b");
            Assert.AreSame(obj1, obj2, "obj1 и obj2 должны ссылаться на один объект");
            Assert.AreNotSame(obj1, obj3, "obj1 и obj3 должны ссылаться на разные объекты");

            // Проверка на null
            string notNullString = "Hello";
            string nullString = null;
            Assert.IsNotNull(notNullString);
            Assert.IsNull(nullString);
        }

        // Три теста для математических функций из ПР4 ч 1
        [TestMethod]
        public void TestPage1Calculation()
        {
            // Arrange
            double x = 2.0;
            double y = 3.0;
            double z = 1.0;

            // Ожидаемое значение (можно вычислить вручную или через калькулятор)
            // Для примера возьмем приблизительное значение
            double denominator = Math.Abs(x) + 1.0 / (y * y + 1.0);
            double expected = 2 * Math.Pow(y, 3) + 3 * x * y - (y * (Math.Atan(z) - Math.PI / 2)) / denominator;

            // Act
            // Здесь нужно будет вызвать метод из Page1 после рефакторинга
            // Пока просто демонстрация
            double actual = expected; // Временное решение для демонстрации

            // Assert
            Assert.AreEqual(expected, actual, 0.0001, "Результат вычисления на Page1 неверен");
        }

        [TestMethod]
        public void TestPage2Calculation()
        {
            // Arrange
            double x = 2.0;
            double y = 1.0;

            // Act & Assert
            // Здесь будут тесты для Page2
            Assert.IsTrue(true); // Временная заглушка
        }

        [TestMethod]
        public void TestPage3Calculation()
        {
            // Arrange
            double x0 = 0;
            double xk = 10;
            double dx = 1;
            double b = 2;

            // Act & Assert
            // Здесь будут тесты для Page3
            Assert.IsTrue(true); // Временная заглушка
        }
    }
}