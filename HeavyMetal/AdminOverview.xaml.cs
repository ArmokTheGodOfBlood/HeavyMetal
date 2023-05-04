using HeavyMetal.TablePages;
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

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace HeavyMetal
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class AdminOverview : Page
    {
        public static AdminOverview instance;
        public AdminOverview()
        {
            this.InitializeComponent();
            frame.Navigate(typeof(UsersTable));
            instance = this;
        }

        private void Users_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(typeof(UsersTable));
        }

        private void WS_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(typeof(WorkshopsTable));
        }
    }
}
