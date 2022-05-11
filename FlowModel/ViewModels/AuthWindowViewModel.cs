using FlowModel.InteractionDB;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPF_MVVM_Classes;

namespace FlowModel.ViewModels
{
    internal class AuthWindowViewModel : ViewModelBase
    {
        private FlowModelContext _FlowModelContext;

        public AuthWindowViewModel()
        {
            _FlowModelContext = new FlowModelContext();
            BrushesLogin = System.Drawing.Color.Gray.Name.ToString();
            BrushesPass = System.Drawing.Color.Gray.Name.ToString();
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
                        if (user.Role == "Исследователь")
                        {
                            _MainWindowViewModel = new MainWindowViewModel();
                            _MainWindow = new MainWindow();
                            _MainWindow.DataContext = _MainWindowViewModel;
                            _MainWindow.Show();
                            var parent = VisualTreeHelper.GetParent(passBox);
                            while (!(parent is Window))
                            {
                                parent = VisualTreeHelper.GetParent(parent);
                            }
                            ((Window)parent).Close();
                        }

                        else if (user.Role == "Администратор")
                        {
                            _AdminWindowViewModel = new AdminWindowViewModel();
                            _AdminWindow = new AdminWindow();
                            _AdminWindow.DataContext = _AdminWindowViewModel;
                            _AdminWindow.Show();
                            var parent = VisualTreeHelper.GetParent(passBox);
                            while (!(parent is Window))
                            {
                                parent = VisualTreeHelper.GetParent(parent);
                            }
                            ((Window)parent).Close();
                        }
                    }

                    if (!(_FlowModelContext.User.ToList().FirstOrDefault(x => x.Login == Login) is null))
                    {
                        BrushesLogin = System.Drawing.Color.Gray.Name.ToString();

                        if (_FlowModelContext.User.ToList().FirstOrDefault(x => x.Pass == Pass) is null)
                        {
                            BrushesPass = System.Drawing.Color.Red.Name.ToString();
                        }
                        else
                            BrushesPass = System.Drawing.Color.Gray.Name.ToString();
                    }
                    else
                    {
                        BrushesPass = System.Drawing.Color.Red.Name.ToString();
                        BrushesLogin = System.Drawing.Color.Red.Name.ToString();
                    }
                });
            }
        }
    }
}
