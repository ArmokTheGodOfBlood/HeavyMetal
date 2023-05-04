using HeavyMetal.Code;
using HeavyMetal.OtherPages;
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

namespace HeavyMetal.TablePages
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class WorkshopsTable : Page
    {
        public WorkshopsTable()
        {
            this.InitializeComponent();
            Refresh();
        }

        public void Refresh()
        {
            StackPanel stack = new StackPanel() { Orientation = Orientation.Horizontal };

            stack.Children.Add(new TextBlock() { Text = "ID", Margin = new Thickness(25, 3, 25, 3) });
            stack.Children.Add(new AppBarSeparator() { BorderThickness = new Thickness(3) });
            stack.Children.Add(new TextBlock() { Text = "Name", Margin = new Thickness(25, 3, 25, 3), Width = 100 });
            stack.Children.Add(new AppBarSeparator() { BorderThickness = new Thickness(3) });
            stack.Children.Add(new TextBlock() { Text = "Type", Margin = new Thickness(25, 3, 25, 3), Width = 100 });

            table.Children.Add(stack);
            table.Children.Add(new MenuFlyoutSeparator() { BorderThickness = new Thickness(3) });

            List<int> ids = SQLHandler.Workshop_GetAllIds();
            foreach (var v in ids)
            {
                string[] data = SQLHandler.Workshop_GetAllPublic_By_ID(v);
                stack = new StackPanel() { Orientation = Orientation.Horizontal };
                stack.Children.Add(new TextBlock() { Text = v.ToString(), Margin = new Thickness(25, 3, 25, 3) });
                foreach (var d in data)
                {
                    stack.Children.Add(new AppBarSeparator() { BorderThickness = new Thickness(3) });
                    try
                    {
                        stack.Children.Add(new TextBlock() { Text = d, Margin = new Thickness(25, 3, 25, 3), Width = 100 });

                    }
                    catch (Exception)
                    {
                        stack.Children.Add(new TextBlock() { Margin = new Thickness(25, 3, 25, 3), Width = 100 });
                    }
                }
                Button button = new Button() { Content = "Show" };
                button.Click += OpenLinkedUser;
                stack.Children.Add(button);

                table.Children.Add(stack);
                table.Children.Add(new MenuFlyoutSeparator() { BorderThickness = new Thickness(3) });
            }
        }

        private void OpenLinkedUser(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            StackPanel stack = (StackPanel)button.Parent;
            TextBlock textBlock = (TextBlock)stack.Children[0];
            int id = int.Parse(textBlock.Text);

            MainPage.NavHelper(typeof(WorkshopPage), id);
            WorkshopPage.RefreshHandler(id);
        }
    }
}
