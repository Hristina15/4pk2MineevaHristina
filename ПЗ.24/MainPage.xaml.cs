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

namespace ПЗ._24
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void DropDownButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем URL из тега закладки
            string url = ((MenuFlyoutItem)sender).Tag.ToString();

            // Загружаем страницу в WebView
            webView.Navigate(new Uri(url));
        }

        private void AddBookmark_Click(object sender, RoutedEventArgs e)
        {
            // Получаем URL из адресной строки
            string url = urlTextBox.Text.Trim();

            if (!string.IsNullOrEmpty(url))
            {
                // Создаем новый пункт меню для закладок
                MenuFlyoutItem bookmarkItem = new MenuFlyoutItem
                {
                    Text = url,
                    Tag = url
                };

                // Добавляем обработчик события клика на закладку
                bookmarkItem.Click += DropDownButton_Click;

                // Добавляем закладку в меню
                bookmarksFlyout.Items.Add(bookmarkItem);
            }
        }
    }
}
