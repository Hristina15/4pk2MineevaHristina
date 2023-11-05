using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace ПЗ._20
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private double result = 0;
        private string currentOperator = "";
        public MainPage()
        {
            this.InitializeComponent();
        }
        private void UpdateResult(string value)
        {
            resultTextBox.Text = value;
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            resultTextBox.Text += button.Content.ToString();
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            currentOperator = button.Content.ToString();
            result = double.Parse(resultTextBox.Text);
            resultTextBox.Text = "";
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double secondOperand = double.Parse(resultTextBox.Text);
                double calculatedResult = 0;

                switch (currentOperator)
                {
                    case "+":
                        calculatedResult = result + secondOperand;
                        break;
                    case "-":
                        calculatedResult = result - secondOperand;
                        break;
                    case "*":
                        calculatedResult = result * secondOperand;
                        break;
                    case "/":
                        if (secondOperand == 0)
                        {
                            UpdateResult("Ошибка");
                            return;
                        }
                        calculatedResult = result / secondOperand;
                        break;
                }

                UpdateResult(calculatedResult.ToString());
            }
            catch
            {
                UpdateResult("Ошибка");
            }
        }

        private void PowButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double value = double.Parse(resultTextBox.Text);
                double calculatedResult = Math.Pow(value, 2);
                UpdateResult(calculatedResult.ToString());
            }
            catch
            {
                UpdateResult("Ошибка");
            }
        }

        private void SqrtButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double value = double.Parse(resultTextBox.Text);
                double calculatedResult = Math.Sqrt(value);
                UpdateResult(calculatedResult.ToString());
            }
            catch
            {
                UpdateResult("Ошибка");
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            resultTextBox.Text = "";
        }

        private void resultTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

