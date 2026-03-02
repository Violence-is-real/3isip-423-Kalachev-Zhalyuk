using System;
using System.Windows;
using System.Windows.Controls;

namespace _3isip_423_Kalachev_Zhalyuk
{
    /// <summary>
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            double x, y, fx, b;
            if (!double.TryParse(tbX.Text, out x) ||
                !double.TryParse(tbY.Text, out y))
            {
                MessageBox.Show("Введите корректные числовые значения!");
                return;
            }

            if (!rbSinh.IsChecked.Value && !rbX2.IsChecked.Value && !rbExp.IsChecked.Value)
            {
                MessageBox.Show("Выберите функцию f(x)!");
                return;
            }

            if (rbSinh.IsChecked.Value)
                fx = Math.Sinh(x);
            else if (rbX2.IsChecked.Value)
                fx = x * x;
            else
                fx = Math.Exp(x);

            if (y == 0)
            {
                b = 0;
            }
            else if (x == 0)
            {
                b = Math.Pow(fx * fx + y, 3);
            }
            else
            {
                double ratio = x / y;
                if (ratio > 0)
                {
                    if (fx <= 0)
                    {
                        MessageBox.Show("Логарифм от неположительного!");
                        return;
                    }
                    b = Math.Log(fx) + Math.Pow(fx * fx + y, 3);
                }
                else
                {
                    double abs_fx_y = Math.Abs(fx / y);
                    if (abs_fx_y <= 0)
                    {
                        MessageBox.Show("Логарифм от неположительного!");
                        return;
                    }
                    b = Math.Log(abs_fx_y) + Math.Pow(fx + y, 3);
                }
            }

            tbResult.Text = b.ToString();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            tbX.Text = "";
            tbY.Text = "";
            tbResult.Text = "";
            rbSinh.IsChecked = false;
            rbX2.IsChecked = false;
            rbExp.IsChecked = false;
        }
    }
}
