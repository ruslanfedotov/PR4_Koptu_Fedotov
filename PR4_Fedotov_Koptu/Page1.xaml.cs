// Page1.xaml.cs - правильный вариант 8
using System;
using System.Windows;
using System.Windows.Controls;

namespace PR4_Fedotov_Koptu
{
    public partial class Page1 : Page
    {
        public Page1() => InitializeComponent();

        private void BtnCalc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double x = double.Parse(txtX.Text);
                double y = double.Parse(txtY.Text);
                double z = double.Parse(txtZ.Text);

                if (y <= 0) throw new Exception("y должен быть > 0 для ln(y)");

                double absDiff = Math.Abs(x - y);
                double expPart = Math.Exp(absDiff);
                double powerPart = Math.Pow(absDiff, x + y);

                double denominator = Math.Atan(x) + Math.Atan(z);
                if (Math.Abs(denominator) < 1e-10) throw new Exception("arctg(x) + arctg(z) = 0");

                double firstPart = (expPart * powerPart) / denominator;
                double secondPart = Math.Pow(x * x + Math.Pow(Math.Log(y), 2), 1.0 / 3.0);

                double result = firstPart + secondPart;
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
            txtY.Clear();
            txtZ.Clear();
            txtResult.Clear();
        }
    }
}