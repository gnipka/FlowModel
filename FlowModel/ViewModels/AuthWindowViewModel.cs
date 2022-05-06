using FlowModel.InteractionDB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPF_MVVM_Classes;

namespace FlowModel.ViewModels
{
    internal class AuthWindowViewModel : ViewModelBase
    {
        private FlowModelContext _FlowModelContext;

        public AuthWindowViewModel()
        {
            _FlowModelContext = new FlowModelContext();
            BrushesLogin = Color.Gray.Name.ToString();
            BrushesPass = Color.Gray.Name.ToString();
        }

        private string _Login;
        public string Login
        {
            get { return _Login; }
            set
            {
                _Login = value;
                OnPropertyChanged();
            }
        }

        private string _Pass;
        public string Pass
        {
            get { return _Pass; }
            set
            {
                _Pass = value;
                OnPropertyChanged();
            }
        }

        private string _ErrorLogin;
        public string ErrorLogin
        {
            get { return _ErrorLogin; }
            set
            {
                _ErrorLogin = value;
                OnPropertyChanged();
            }
        }

        private string _ErrorPass;
        public string ErrorPass
        {
            get { return _ErrorPass; }
            set
            {
                _ErrorPass = value;
                OnPropertyChanged();
            }
        }

        private string _BrushesLogin;
        public string BrushesLogin
        {
            get { return _BrushesLogin; }
            set
            {
                _BrushesLogin = value;
                OnPropertyChanged();
            }
        }
        private string _BrushesPass;
        public string BrushesPass
        {
            get { return _BrushesPass; }
            set
            {
                _BrushesPass = value;
                OnPropertyChanged();
            }
        }

        private MainWindow? _MainWindow = null;
        private MainWindowViewModel _MainWindowViewModel;

        private AdminWindow? _AdminWindow = null;
        private AdminWindowViewModel _AdminWindowViewModel;

        private RelayCommand _OpenWindow;

        public RelayCommand OpenWindow
        {
            get
            {
                return _OpenWindow ??= new RelayCommand(x =>
                {
                    PasswordBox passBox = (PasswordBox)x;
                    Pass = passBox.Password;
                    var user = _FlowModelContext.User.ToList().FirstOrDefault(x => x.Login == Login && x.Pass == Pass);
                    if (user is not null)
                    {
                        if(user.Role == "researcher")
                        {
                            _MainWindowViewModel = new MainWindowViewModel();
                            _MainWindow = new MainWindow();
                            _MainWindow.DataContext = _MainWindowViewModel;
                            _MainWindow.Show();                            
                        }

                        else if(user.Role == "admin")
                        {
                            _AdminWindowViewModel = new AdminWindowViewModel();
                            _AdminWindow = new AdminWindow();
                            _AdminWindow.DataContext = _AdminWindowViewModel;
                            _AdminWindow.Show();
                        }
                    }

                    else if (_FlowModelContext.User.ToList().FirstOrDefault(x => x.Login == Login ) is null)
                    {
                        ErrorLogin = "Логин введен неверное";
                        BrushesLogin = Color.Red.Name.ToString();
                    }

                    else if (_FlowModelContext.User.ToList().FirstOrDefault(x => x.Login == Login) is null)
                    {
                        ErrorLogin = "Пароль введен неверное";
                        BrushesPass = Color.Red.Name.ToString();
                    }
                });
            }
        }
    }
}
