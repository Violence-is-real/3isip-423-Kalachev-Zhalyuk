using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.DataVisualization.Charting;

namespace _3isip_423_Kalachev_Zhalyuk
{
    /// <summary>
    /// Логика взаимодействия для Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();
            ChartFunction.Titles.Add("График функции");
            ChartFunction.ChartAreas.Add(new ChartArea("Main"));
            var currentSeries = new Series("y")
            {
                IsValueShownAsLabel = false
            };
            ChartFunction.Series.Add(currentSeries);
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            double x0, xk, dx, b;
            if (!double.TryParse(tbX0.Text, out x0) ||
                !double.TryParse(tbXk.Text, out xk) ||
                !double.TryParse(tbDx.Text, out dx) ||
                !double.TryParse(tbB.Text, out b) ||
                dx == 0)
            {
                MessageBox.Show("Введите корректные числовые значения! dx != 0");
                return;
            }

            if (x0 > xk && dx > 0 || x0 < xk && dx < 0)
            {
                MessageBox.Show("Неправильное направление шага!");
                return;
            }

            List<KeyValuePair<double, double>> data = new List<KeyValuePair<double, double>>();
            string results = "";
            for (double x = x0; dx > 0 ? x <= xk : x >= xk; x += dx)
            {
                double y;
                try
                {
                    y = 9 * (Math.Pow(x, 3) + Math.Pow(b, 3)) * Math.Tan(x);
                }
                catch
                {
                    MessageBox.Show($"Ошибка вычисления при x={x}, возможно тангенс неопределен!");
                    return;
                }
                results += $"x={x}, y={y}\n";
                data.Add(new KeyValuePair<double, double>(x, y));
            }
            tbResults.Text = results;

            Series currentSeries = ChartFunction.Series[0];
            currentSeries.ChartType = SeriesChartType.Line;
            currentSeries.Points.Clear();
            foreach (var point in data)
            {
                currentSeries.Points.AddXY(point.Key, point.Value);
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            tbX0.Text = "";
            tbXk.Text = "";
            tbDx.Text = "";
            tbB.Text = "";
            tbResults.Text = "";
            ChartFunction.Series[0].Points.Clear();
        }
    }
}
