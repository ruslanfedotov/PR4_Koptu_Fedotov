using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.DataVisualization.Charting;

namespace PR4_Fedotov_Koptu
{
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();

            // Настройка графика
            ChartFunction.ChartAreas.Add(new ChartArea("Main"));
            ChartFunction.Series.Add(new Series("y = 9x⁴ + sin(57.2 + x)"));
            ChartFunction.Series[0].ChartType = SeriesChartType.Line;
            ChartFunction.Series[0].BorderWidth = 2;

            // Настройка осей
            ChartFunction.ChartAreas["Main"].AxisX.Title = "x";
            ChartFunction.ChartAreas["Main"].AxisY.Title = "y";
        }

        private double CalculateY(double x)
        {
            // y = 9x⁴ + sin(57.2 + x)
            return 9 * Math.Pow(x, 4) + Math.Sin(57.2 + x);
        }

        private void BtnCalc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double x0 = double.Parse(txtX0.Text);
                double xk = double.Parse(txtXk.Text);
                double dx = double.Parse(txtDx.Text);

                if (dx == 0)
                {
                    MessageBox.Show("Шаг dx не может быть равен 0", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Очистка графика и результатов
                ChartFunction.Series[0].Points.Clear();
                string results = "x\t\t\ty\n";
                results += "----------------------\n";

                int count = 0;

                // Цикл для построения графика
                if (dx > 0)
                {
                    for (double x = x0; x <= xk + 0.0001; x += dx)
                    {
                        double y = CalculateY(x);
                        ChartFunction.Series[0].Points.AddXY(x, y);
                        results += $"{x:F4}\t\t{y:F6}\n";
                        count++;
                    }
                }
                else
                {
                    for (double x = x0; x >= xk - 0.0001; x += dx)
                    {
                        double y = CalculateY(x);
                        ChartFunction.Series[0].Points.AddXY(x, y);
                        results += $"{x:F4}\t\t{y:F6}\n";
                        count++;
                    }
                }

                if (count == 0)
                {
                    results = "Нет точек для отображения. Проверьте границы и шаг.";
                }

                txtResults.Text = results;

                // Автомасштабирование графика
                ChartFunction.ChartAreas["Main"].RecalculateAxesScale();
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите корректные числовые значения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            // Сброс значений по умолчанию
            txtX0.Text = "-0.75";
            txtXk.Text = "-2.05";
            txtDx.Text = "-0.2";

            // Очистка результатов
            txtResults.Clear();

            // Очистка графика
            if (ChartFunction.Series.Count > 0)
            {
                ChartFunction.Series[0].Points.Clear();
            }
        }
    }
}