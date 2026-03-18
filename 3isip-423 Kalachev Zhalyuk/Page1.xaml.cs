using System;
using System.Windows;
using System.Windows.Controls;

namespace _3isip_423_Kalachev_Zhalyuk
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml. Содержит вычисления по первой формуле.
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Вычислить". Получает данные из полей ввода,
        /// вызывает метод вычисления и отображает результат.
        /// </summary>
        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            if (!TryParseInputs(out double x, out double y, out double z))
            {
                return; // Сообщение об ошибке показывается внутри TryParseInputs
            }

            if (!TryCalculateResult(x, y, z, out double result, out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Ошибка вычисления", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            tbResult.Text = result.ToString();
        }

        /// <summary>
        /// Пытается распарсить значения из текстовых полей X, Y, Z.
        /// </summary>
        /// <param name="x">Переменная для сохранения значения X.</param>
        /// <param name="y">Переменная для сохранения значения Y.</param>
        /// <param name="z">Переменная для сохранения значения Z.</param>
        /// <returns>True, если все значения распаршены успешно; иначе False.</returns>
        private bool TryParseInputs(out double x, out double y, out double z)
        {
            x = y = z = 0;
            bool isXValid = double.TryParse(tbX.Text, out x);
            bool isYValid = double.TryParse(tbY.Text, out y);
            bool isZValid = double.TryParse(tbZ.Text, out z);

            if (!isXValid || !isYValid || !isZValid)
            {
                MessageBox.Show("Введите корректные числовые значения во все поля!", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Вычисляет значение по формуле: c = 2*y^3 + 3*x*y - (y * (atan(z) - pi/2)) / (|x| + 1/(y^2 + 1))
        /// </summary>
        /// <param name="x">Параметр x.</param>
        /// <param name="y">Параметр y.</param>
        /// <param name="z">Параметр z.</param>
        /// <param name="result">Результат вычисления.</param>
        /// <param name="errorMessage">Сообщение об ошибке, если вычисление невозможно.</param>
        /// <returns>True, если вычисление успешно; иначе False.</returns>
        private bool TryCalculateResult(double x, double y, double z, out double result, out string errorMessage)
        {
            result = 0;
            errorMessage = string.Empty;

            // Вычисляем знаменатель
            double denominator = Math.Abs(x) + 1.0 / (y * y + 1.0);

            // Знаменатель всегда положителен, но оставим проверку для надежности и примера
            if (Math.Abs(denominator) < double.Epsilon)
            {
                errorMessage = "Знаменатель дроби равен нулю.";
                return false;
            }

            try
            {
                result = 2 * Math.Pow(y, 3) + 3 * x * y - (y * (Math.Atan(z) - Math.PI / 2)) / denominator;
            }
            catch (Exception ex)
            {
                errorMessage = $"Ошибка при вычислении: {ex.Message}";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Очистить". Очищает все поля ввода и результата.
        /// </summary>
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            tbX.Clear();
            tbY.Clear();
            tbZ.Clear();
            tbResult.Clear();
        }
    }
}