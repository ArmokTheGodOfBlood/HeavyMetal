using HeavyMetal.Code;
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

namespace HeavyMetal.OtherPages
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class WorkshopPage : Page
    {
        public static int shop_id;
        public static WorkshopPage instance;

        public WorkshopPage()
        {
            this.InitializeComponent();
            instance = this;
        }
        public static void RefreshHandler(int new_id)
        {
            instance.Refresh(new_id);
        }
        public void Refresh(int id)
        {
            shop_id = id;
            string[] data = SQLHandler.Workshop_GetAllPublic_By_ID(id);
            Name.Text = data[0].Trim();
            try
            {
                Type.Text = data[1].Trim();
            }
            catch (Exception)
            {
            }

            data = SQLHandler.WBW_GetWorkers_ByShopID(shop_id).ToArray();
            foreach (var v in data)
            {
                StackPanel stack = new StackPanel() { Orientation = Orientation.Horizontal};
                stack.Children.Add(new TextBlock() { Text = v });
                Button button = new Button() { Content = "Show" };
                button.Click += OpenLinkedUser;
                stack.Children.Add(button);

                Members.Children.Add(stack);
            }
        }
        public void OpenLinkedUser(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            StackPanel stack = (StackPanel)button.Parent;
            TextBlock textBlock = (TextBlock)stack.Children[0];
            int id = int.Parse(textBlock.Text);

            MainPage.NavHelper(typeof(PublicUser), id);
            PublicUser.RefreshHandler(id);
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            MainPage.NavBackHelper();
        }
        private void Prev_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Next_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            //SQLHandler.User_FullUpdate(user_id, Login.Text, Name.Text, Secondname.Text, Surename.Text, Role.Text);
            Refresh(shop_id);
        }
    }
}
