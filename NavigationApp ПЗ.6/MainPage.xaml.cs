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

namespace NavigationApp_ПЗ._6
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private object textBlock1;

        public MainPage()
        {
            this.InitializeComponent();
        }
        private void Forward_Click(object sender, RoutedEventArgs e)
        {
            PageInfo pageInfo = new PageInfo { Id = 001, Name = "страница" };
            Frame.Navigate(typeof(Page2), pageInfo);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
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

        
    }
}
