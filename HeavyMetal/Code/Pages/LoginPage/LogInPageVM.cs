using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace HeavyMetal.Code.Pages.LogInPage
{
    class LogInPageVM : INotifyPropertyChanged
    {

        private bool _visibility = LogInPageModel.password_visibility;
        public Visibility PasswordTextBoxVisibility
        {
            get
            {
                if (_visibility == true)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }
        public Visibility PasswordBoxVisibility
        {
            get
            {
                if (_visibility == true)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
        }
        public bool pass_Visibility 
        { get 
            { return _visibility; } 
            set 
            {
                _visibility = !_visibility;
                OnPropertyChanged("_visibility");
                OnPropertyChanged("PasswordTextBoxVisibility");
                OnPropertyChanged("PasswordBoxVisibility");
            } 
        }

        private string _password = LogInPageModel.password;
        private string _username = LogInPageModel.username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                LogInPageModel.username = value;
                _username = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                LogInPageModel.password = value;
                _password = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
