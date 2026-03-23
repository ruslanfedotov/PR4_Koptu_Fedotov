using System;
using System.Windows;
using System.Windows.Controls;

namespace PR4_Fedotov_Koptu
{
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        private double GetFx(double x)
        {
            if (rbSh.IsChecked == true) return Math.Sinh(x);
            if (rbExp.IsChecked == true) return Math.Exp(x);
            return x * x;
        }

        private void BtnCalc_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtX.Text) || string.IsNullOrWhiteSpace(txtM.Text))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!double.TryParse(txtX.Text, out double x) ||
                !double.TryParse(txtM.Text, out double m))
            {
                MessageBox.Show("Введите числовые значения!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            double fx = GetFx(x);
            double result;

            // Ветка 1: -1 < m < x → sin
            if (m > -1 && m < x)
            {
                result = Math.Sin(5 * fx + 3 * m * Math.Abs(fx));
            }
            // Ветка 2: x > m (но не попали в ветку 1) → cos
            else if (x > m)
            {
                result = Math.Cos(3 * fx + 5 * m * Math.Abs(fx));
            }
            // Ветка 3: x = m → (f(x) + m)²
            else if (x == m)
            {
                result = Math.Pow(fx + m, 2);
            }
            else
            {
                MessageBox.Show("Условие задачи не выполняется (x < m и x ≠ m).",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            txtResult.Text = result.ToString("F6");
        }
    }
}