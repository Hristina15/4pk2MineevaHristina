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

namespace ПЗ._18
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Button button1 = new Button();
            button1.Content = "Новая кнопка";
            button1.Width = 120;
            button1.Height = 40;
            // добавление кнопки в грид
            layoutGrid.Children.Add(button1);
        }
        
    }

    public class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }

        public override string ToString()
        {
            return $"студент {this.Name} из группы {this.Group}";
        }
    }
}
