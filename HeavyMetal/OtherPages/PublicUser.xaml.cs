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
    public sealed partial class PublicUser : Page
    {
        public static int user_id;
        public static PublicUser instance;
        public PublicUser()
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
            user_id = id;
            string[] data = SQLHandler.User_GetAllPublic_By_ID(id);
            Login.Text = data[0].Trim();
            try
            {
                Name.Text = data[1].Trim();
            }
            catch (Exception)
            {
            }
            try
            {
                Secondname.Text = data[2].Trim();
            }
            catch (Exception)
            {
            }
            try
            {
                Surename.Text = data[3].Trim();
            }
            catch (Exception)
            {
            }
            Role.Text = data[4].Trim();

            data = SQLHandler.WBW_GetShops_ByWorkerID(user_id).ToArray();
            foreach (var v in data)
            {
                Shops.Children.Add(new TextBlock() { Text = v});
            }
            data = SQLHandler.WBW_GetChecklist_ByWorkerID(user_id).ToArray();
            foreach (var v in data)
            {
                Checks.Children.Add(new TextBlock() { Text = v });
            }
            data = SQLHandler.WBF_GetFunctions_ByWorkerID(user_id).ToArray();
            foreach (var v in data)
            {
                Roles.Children.Add(new TextBlock() { Text = v });
            }
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
            SQLHandler.User_FullUpdate(user_id, Login.Text, Name.Text, Secondname.Text, Surename.Text, Role.Text);
            Refresh(user_id);
        }
    }
}
