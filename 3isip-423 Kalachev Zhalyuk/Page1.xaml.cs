using System;
using System.Windows;
using System.Windows.Controls;

namespace _3isip_423_Kalachev_Zhalyuk
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            double x, y, z, c;
            if (!double.TryParse(tbX.Text, out x) ||
                !double.TryParse(tbY.Text, out y) ||
                !double.TryParse(tbZ.Text, out z))
            {
                MessageBox.Show("Введите корректные числовые значения!");
                return;
            }

            if (y == 0) // Проверка на возможные проблемы, но в формуле y в числителе
            {
                MessageBox.Show("y не может быть нулем в некоторых частях!");
                return;
            }

            double denominator = Math.Abs(x) + 1 / (y * y + 1);
            if (denominator == 0)
            {
                MessageBox.Show("Деление на ноль!");
                return;
            }

            c = 2 * Math.Pow(y, 3) + 3 * x * y - (y * (Math.Atan(z) - Math.PI / 2)) / denominator;
            tbResult.Text = c.ToString();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            tbX.Text = "";
            tbY.Text = "";
            tbZ.Text = "";
            tbResult.Text = "";
        }
    }
}

