using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ПЗ._21
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        private string customText;

        public event PropertyChangedEventHandler PropertyChanged;

        public string CustomText
        {
            get { return customText; }
            set
            {
                customText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CustomText)));
            }
        }

        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = this;

            CustomText = "Hello World!";
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            CustomText = "Привет!";
        }
    }
}
