using System;
using System.Windows;
using System.Windows.Controls;

namespace _3isip_423_Kalachev_Zhalyuk
{
    /// <summary>
    /// Логика взаимодействия для Page2.xaml
    /// Содержит вычисления по второй формуле из ПР4 с выбором функции f(x)
    /// </summary>
    public partial class Page2 : Page
    {
        /// <summary>
        /// Инициализирует новый экземпляр страницы Page2
        /// </summary>
        public Page2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Перечисление доступных функций для вычисления f(x).
        /// </summary>
        public enum FunctionType
        {
            /// <summary>Гиперболический синус: sinh(x)</summary>
            Sinh,
            /// <summary>Квадрат числа: x²</summary>
            Square,
            /// <summary>Экспонента: e^x</summary>
            Exp
        }

        /// <summary>
        /// Вычисляет значение b по формуле из ПР4 на основе входных параметров.
        /// Формула зависит от соотношения x/y и выбранной функции f(x)
        /// </summary>
        /// <param name="x">Параметр x (действительное число).</param>
        /// <param name="y">Параметр y (действительное число).</param>
        /// <param name="function">Тип функции f(x) из перечисления FunctionType.</param>
        /// <returns>Результат вычисления b.</returns>
        /// <exception cref="ArgumentException">Выбрасывается при невозможности вычисления 
        /// (например, логарифм от неположительного числа).</exception>
        /// <exception cref="ArgumentOutOfRangeException">Выбрасывается при неизвестном типе функции.</exception>
        public double CalculateB(double x, double y, FunctionType function)
        {
            // Вычисляем f(x) в зависимости от выбранной функции
            double fx;
            switch (function)
            {
                case FunctionType.Sinh:
                    fx = Math.Sinh(x);
                    break;
                case FunctionType.Square:
                    fx = x * x;
                    break;
                case FunctionType.Exp:
                    fx = Math.Exp(x);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(function),
                        $"Неизвестный тип функции: {function}");
            }

            // Случай 1: y = 0
            if (Math.Abs(y) < double.Epsilon)
            {
                return 0;
            }

            // Случай 2: x = 0
            if (Math.Abs(x) < double.Epsilon)
            {
                return Math.Pow(fx * fx + y, 3);
            }

            // Случай 3: x/y > 0
            double ratio = x / y;
            if (ratio > 0)
            {
                if (fx <= 0)
                    throw new ArgumentException(
                        $"Логарифм от неположительного числа! f(x) = {fx} при x/y = {ratio} > 0");
                return Math.Log(fx) + Math.Pow(fx * fx + y, 3);
            }
            // Случай 4: x/y < 0
            else
            {
                double abs_fx_y = Math.Abs(fx / y);
                if (abs_fx_y <= 0)
                    throw new ArgumentException(
                        $"Логарифм от неположительного числа! |f(x)/y| = {abs_fx_y} при x/y = {ratio} < 0");
                return Math.Log(abs_fx_y) + Math.Pow(fx + y, 3);
            }
        }

        /// <summary>
        /// Пытается распарсить значения X и Y из текстовых полей ввода.
        /// </summary>
        /// <param name="x">Переменная для сохранения значения X.</param>
        /// <param name="y">Переменная для сохранения значения Y.</param>
        /// <returns>True, если оба значения успешно распаршены; иначе False.</returns>
        private bool TryParseInputs(out double x, out double y)
        {
            x = 0;
            y = 0;

            bool isXValid = double.TryParse(tbX.Text, out x);
            bool isYValid = double.TryParse(tbY.Text, out y);

            if (!isXValid || !isYValid)
            {
                MessageBox.Show("Введите корректные числовые значения в поля X и Y!",
                    "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Определяет выбранную пользователем функцию по состоянию RadioButton.
        /// </summary>
        /// <param name="selectedFunction">Выбранная функция, если успешно определено.</param>
        /// <returns>True, если функция выбрана; иначе False.</returns>
        private bool TryGetSelectedFunction(out FunctionType selectedFunction)
        {
            selectedFunction = FunctionType.Sinh; // Значение по умолчанию

            if (rbSinh.IsChecked == true)
            {
                selectedFunction = FunctionType.Sinh;
                return true;
            }
            else if (rbX2.IsChecked == true)
            {
                selectedFunction = FunctionType.Square;
                return true;
            }
            else if (rbExp.IsChecked == true)
            {
                selectedFunction = FunctionType.Exp;
                return true;
            }

            MessageBox.Show("Выберите функцию f(x)!", "Ошибка выбора",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Вычислить". Получает данные из полей ввода,
        /// определяет выбранную функцию, вызывает метод CalculateB и отображает результат.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            // Парсинг входных данных
            if (!TryParseInputs(out double x, out double y))
            {
                return;
            }

            // Определение выбранной функции
            if (!TryGetSelectedFunction(out FunctionType selectedFunction))
            {
                return;
            }

            // Вычисление результата с обработкой исключений
            try
            {
                double result = CalculateB(x, y, selectedFunction);
                tbResult.Text = result.ToString("G"); // G - общий формат чисел
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Ошибка в вычислениях: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неожиданная ошибка: {ex.Message}",
                    "Критическая ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Очистить". Очищает все поля ввода, результата
        /// и сбрасывает выбор функции.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            tbX.Clear();
            tbY.Clear();
            tbResult.Clear();

            // Сброс выбора RadioButton
            rbSinh.IsChecked = false;
            rbX2.IsChecked = false;
            rbExp.IsChecked = false;
        }
    }
}