using System;
using System.Windows;
using System.Windows.Controls;

namespace PR4_Fedotov_Koptu
{
    public partial class Page2 : Page
    {
        public Page2() => InitializeComponent();

        private double GetFx(double x)
        {
            if (rbSh.IsChecked == true) return Math.Sinh(x);
            if (rbExp.IsChecked == true) return Math.Exp(x);
            return x * x;
        }

        private void BtnCalc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double x = double.Parse(txtX.Text);
                double m = double.Parse(txtM.Text);
                double fx = GetFx(x);
                double result;

                if (x == m)
                {
                    result = Math.Pow(fx + m, 2);
                }
                else if (x > m)
                {
                    result = Math.Cos(3 * fx + 5 * m * Math.Abs(fx));
                }
                else
                {
                    if (m > -1 && m < x)
                    {
                        result = Math.Sin(5 * fx + 3 * m * Math.Abs(fx));
                    }
                    else
                    {
                        throw new Exception("Условие -1 < m < x не выполняется");
                    }
                }

                txtResult.Text = result.ToString("F6");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            txtX.Clear();
            txtM.Clear();
            txtResult.Clear();
            rbX2.IsChecked = true;
        }
    }
}