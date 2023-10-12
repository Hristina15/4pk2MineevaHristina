using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace NavigationApp_ПЗ._6
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class Page2 : Page
    {
        public Page2()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                PageInfo pageInfo = (PageInfo)e.Parameter;
                textBlock1.Text = pageInfo.Name;
            }

        }

        private void Forward_Click2(object sender, RoutedEventArgs e)
        {
            PageInfo pageInfo = new PageInfo { Id = 001, Name = "страница" };
            Frame.Navigate(typeof(Page3), pageInfo);
        }

        private void Forward_Click3(object sender, RoutedEventArgs e)
        {
            PageInfo pageInfo = new PageInfo { Id = 001, Name = "страница" };
            Frame.Navigate(typeof(Page4), pageInfo);
        }

        private void Forward_Click4(object sender, RoutedEventArgs e)
        {
            PageInfo pageInfo = new PageInfo { Id = 001, Name = "страница" };
            Frame.Navigate(typeof(MainPage), pageInfo);
        }
    }
}
