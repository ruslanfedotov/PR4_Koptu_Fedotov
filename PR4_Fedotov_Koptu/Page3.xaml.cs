using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using System.Windows.Forms.DataVisualization.Charting;

namespace PR4_Fedotov_Koptu
{
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();
        }

        private double GetFx(double x)
        {
            return Math.Pow(x, 3) + Math.Pow(x, 2) - 8 * x - 12;
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
                    throw new Exception("Шаг dx не может быть равен 0");
                }

                if ((dx > 0 && x0 > xk) || (dx < 0 && x0 < xk))
                {
                    throw new Exception("Неверное направление шага. При положительном dx x0 должно быть меньше xk, при отрицательном - x0 больше xk");
                }

                string results = "";
                int count = 0;

                ChartFunction.Series.Clear();
                Series series = new Series("Функция");
                series.ChartType = SeriesChartType.Line;

                for (double x = x0; (dx > 0 && x <= xk) || (dx < 0 && x >= xk); x += dx)
                {
                    double y = GetFx(x);
                    results += $"x = {x:F6}, y = {y:F6}\r\n";
                    series.Points.AddXY(x, y);
                    count++;

                    if (count > 1000)
                    {
                        throw new Exception("Слишком много точек. Увеличьте шаг dx.");
                    }
                }

                if (count == 0)
                {
                    throw new Exception("Нет точек для отображения");
                }

                ChartFunction.Series.Add(series);
                ChartFunction.ChartAreas.Clear();
                ChartArea chartArea = new ChartArea();
                ChartFunction.ChartAreas.Add(chartArea);

                txtResults.Text = results;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txtResults.Clear();
                ChartFunction.Series.Clear();
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            txtX0.Clear();
            txtXk.Clear();
            txtDx.Clear();
            txtResults.Clear();
            ChartFunction.Series.Clear();

            txtX0.Text = "-0.75";
            txtXk.Text = "-2.05";
            txtDx.Text = "-0.2";
        }
    }
}