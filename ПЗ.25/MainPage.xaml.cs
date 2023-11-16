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

namespace ПЗ._25
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Dictionary<string, double> prices = new Dictionary<string, double>()
        {
            { "Книга", 1000 },
            { "Кофе", 120.50 },
            { "Сумка", 15060 },
            { "Кружка", 100.99 }

        };
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void CreateOrderButton_Click(object sender, RoutedEventArgs e)
        {
            double totalPrice = 0;
            List<string> selectedItems = new List<string>();

            if (CoffeeCheckBox.IsChecked == true)
            {
                totalPrice += prices["Книга"];
                selectedItems.Add("Книга");
            }
            if (TaxiCheckBox.IsChecked == true)
            {
                totalPrice += prices["Кофе"];
                selectedItems.Add("Кофе");
            }
            if (BurgerCheckBox.IsChecked == true)
            {
                totalPrice += prices["Сумка"];
                selectedItems.Add("Сумка");
            }
            if (PizzaCheckBox.IsChecked == true)
            {
                totalPrice += prices["Кружка"];
                selectedItems.Add("Кружка");
            }

            string orderSummary = string.Join(", ", selectedItems);

            OrderSummaryTextBlock.Text = orderSummary;
            TotalPriceTextBlock.Text = $"Стоимость заказа: {totalPrice.ToString("0.00")} руб.";
        }
    }
}
