
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyMetal.Code.Pages.LogInPage
{
    static class LogInPageModel
    {
        public static bool password_visibility = false;
        public static string password = "admin";
        public static string username = "admin";

        public static void TrySignIn()
        {
            int id = SQLHandler.User_GetID_ByName(username, password);
            int access = SQLHandler.User_GetAccess_ByID(id);
            switch (access)
            {
                case 1:
                    MainPage.NavHelper(new AdminOverview());
                    break;
                default:
                    ErrorDialog error = new ErrorDialog();
                    error.ShowAsync();
                    break;
            }
        }
        public static void TrySignUp()
        {
            SQLHandler.User_Insert(username.Trim(), password.Trim());
        }
    }
}
