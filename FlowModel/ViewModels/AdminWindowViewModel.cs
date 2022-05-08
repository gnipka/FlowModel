using FlowModel.CRUDInterface;
using FlowModel.InteractionDB;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WPF_MVVM_Classes;

namespace FlowModel.ViewModels
{
    internal class AdminWindowViewModel : ViewModelBase
    {
        private FlowModelContext _FlowModelContext;

        public AdminWindowViewModel()
        {
            Advertisement();

            Roles = new List<string>();
            Roles.Add("Администратор");
            Roles.Add("Исследователь");

        }

        public void Advertisement()
        {
            _FlowModelContext = new FlowModelContext();
            _FlowModelContext.User.Load();
            User = _FlowModelContext.User.ToList();
            _FlowModelContext.Material.Load();
            Material = _FlowModelContext.Material.ToList();
            _FlowModelContext.Unit.Load();
            Unit = _FlowModelContext.Unit.ToList();
            _FlowModelContext.Empirical_coef.Load();
            EmpiricalCoef = _FlowModelContext.Empirical_coef.ToList();

            EmpiricalCoefUser = new List<EmpiricalCoefUser>();
            foreach (var item in EmpiricalCoef)
            {                
                var empty = new EmpiricalCoefUser { ID_empirical_coef = item.ID_empirical_coef, Name_empirical_coef = item.Name_empirical_coef, Name_unit = Unit.FirstOrDefault(x => x.ID_unit == item.ID_unit).Name_unit };

                EmpiricalCoefUser.Add(empty);
            }
        }

        #region MaterialConnectDB

        private List<Material> _Material;
        public List<Material> Material
        {
            get { return _Material; }
            set
            {
                _Material = value;
                OnPropertyChanged();
            }
        }

        private Material _SelectedItem;
        public Material SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (_SelectedItem is not null)
                {
                    NewMaterial = _SelectedItem.Name_material;
                }
            }
        }

        private string _NewMaterial;
        public string NewMaterial
        {
            get { return _NewMaterial; }
            set
            {
                _NewMaterial = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _AddMaterial;

        public RelayCommand AddMaterial
        {
            get
            {
                return _AddMaterial ??= new RelayCommand(x =>
                {
                    if (NewMaterial != string.Empty)
                    {
                        var varMaterial = new Material(NewMaterial);

                        var CRUD = new MaterialCRUD();

                        CRUD.Create(varMaterial);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Заполните все поля, чтобы добавить новую запись", "Ошибка при добавлении записи");
                });
            }
        }

        private RelayCommand _UpdateMaterial;

        public RelayCommand UpdateMaterial
        {
            get
            {
                return _UpdateMaterial ??= new RelayCommand(x =>
                {

                    if (SelectedItem is not null)
                    {

                        var varMaterial = new Material(SelectedItem.ID_material, NewMaterial);

                        var CRUD = new MaterialCRUD();

                        CRUD.Update(varMaterial);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Выберите строку, которую следует изменить", "Ошибка при изменении записи");
                });
            }
        }

        private RelayCommand _RemoveMaterial;

        public RelayCommand RemoveMaterial
        {
            get
            {
                return _RemoveMaterial ??= new RelayCommand(x =>
                {
                    if (SelectedItem is not null)
                    {
                        var CRUD = new MaterialCRUD();

                        CRUD.Delete(SelectedItem);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Выберите строку, которую следует удалить", "Ошибка при удалении записи");
                });
            }
        }

        #endregion

        #region UnitConnectDB

        private List<Unit> _Unit;
        public List<Unit> Unit
        {
            get { return _Unit; }
            set
            {
                _Unit = value;
                OnPropertyChanged();
            }
        }

        private Unit _SelectedUnit;
        public Unit SelectedUnit
        {
            get { return _SelectedUnit; }
            set
            {
                _SelectedUnit = value;
                OnPropertyChanged();
                if (_SelectedUnit is not null)
                    NewUnit = _SelectedUnit.Name_unit;

            }
        }

        private string _NewUnit;
        public string NewUnit
        {
            get { return _NewUnit; }
            set
            {
                _NewUnit = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _AddUnit;

        public RelayCommand AddUnit
        {
            get
            {
                return _AddUnit ??= new RelayCommand(x =>
                {
                    if (NewUnit != string.Empty)
                    {
                        var varUnit = new Unit(NewUnit);

                        var CRUD = new UnitCRUD();

                        CRUD.Create(varUnit);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Заполните все поля, чтобы добавить новую запись", "Ошибка при добавлении записи");
                });
            }
        }

        private RelayCommand _UpdateUnit;

        public RelayCommand UpdateUnit
        {
            get
            {
                return _UpdateUnit ??= new RelayCommand(x =>
                {

                    if (SelectedUnit is not null)
                    {

                        var varUnit = new Unit(SelectedUnit.ID_unit, NewUnit);

                        var CRUD = new UnitCRUD();

                        CRUD.Update(varUnit);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Выберите строку, которую следует изменить", "Ошибка при изменении записи");
                });
            }
        }

        private RelayCommand _RemoveUnit;

        public RelayCommand RemoveUnit
        {
            get
            {
                return _RemoveUnit ??= new RelayCommand(x =>
                {
                    if (SelectedUnit is not null)
                    {
                        var CRUD = new UnitCRUD();

                        CRUD.Delete(SelectedUnit);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Выберите строку, которую следует удалить", "Ошибка при удалении записи");
                });
            }
        }

        #endregion

        #region UserConnectDB

        private List<User> _User;
        public List<User> User
        {
            get { return _User; }
            set
            {
                _User = value;
                OnPropertyChanged();
            }
        }

        private User _SelectedUser;
        public User SelectedUser
        {
            get { return _SelectedUser; }
            set
            {
                _SelectedUser = value;
                OnPropertyChanged();
                if (_SelectedUser is not null)
                {
                    NewLogin = _SelectedUser.Login;
                    NewPass = _SelectedUser.Pass;
                    if (_SelectedUser.Role == "Администратор")
                        NewRole = Roles[0];
                    else
                        NewRole = Roles[1];
                }

            }
        }

        private string _NewLogin;
        public string NewLogin
        {
            get { return _NewLogin; }
            set
            {
                _NewLogin = value;
                OnPropertyChanged();
            }
        }

        private string _NewPass;
        public string NewPass
        {
            get { return _NewPass; }
            set
            {
                _NewPass = value;
                OnPropertyChanged();
            }
        }

        private List<string> _Roles;
        public List<string> Roles
        {
            get { return _Roles; }
            set
            {
                _Roles = value;
                OnPropertyChanged();
            }
        }

        private string _NewRole;
        public string NewRole
        {
            get { return _NewRole; }
            set
            {
                _NewRole = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _AddUser;

        public RelayCommand AddUser
        {
            get
            {
                return _AddUser ??= new RelayCommand(x =>
                {
                    if (NewLogin != string.Empty && NewPass != string.Empty && NewRole != string.Empty)
                    {
                        var varUser = new User(NewLogin, NewPass, NewRole);

                        var CRUD = new UserCRUD();

                        CRUD.Create(varUser);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Заполните все поля, чтобы добавить новую запись", "Ошибка при добавлении записи");
                });
            }
        }

        private RelayCommand _UpdateUser;

        public RelayCommand UpdateUser
        {
            get
            {
                return _UpdateUser ??= new RelayCommand(x =>
                {

                    if (SelectedUser is not null)
                    {

                        var varUser = new User(SelectedUser.ID_user, NewLogin, NewPass, NewRole);

                        var CRUD = new UserCRUD();

                        CRUD.Update(varUser);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Выберите строку, которую следует изменить", "Ошибка при изменении записи");
                });
            }
        }

        private RelayCommand _RemoveUser;

        public RelayCommand RemoveUser
        {
            get
            {
                return _RemoveUser ??= new RelayCommand(x =>
                {
                    if (SelectedUser is not null)
                    {
                        var CRUD = new UserCRUD();

                        CRUD.Delete(SelectedUser);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Выберите строку, которую следует удалить", "Ошибка при удалении записи");
                });
            }
        }

        #endregion


        #region EmpiricalCoefConnectDB

        private List<EmpiricalCoef> _EmpiricalCoef;
        public List<EmpiricalCoef> EmpiricalCoef
        {
            get { return _EmpiricalCoef; }
            set
            {
                _EmpiricalCoef = value;
                OnPropertyChanged();
            }
        }

        private List<EmpiricalCoefUser> _EmpiricalCoefUser;
        public List<EmpiricalCoefUser> EmpiricalCoefUser
        {
            get { return _EmpiricalCoefUser; }
            set
            {
                _EmpiricalCoefUser = value;
                OnPropertyChanged();
            }
        }

        private EmpiricalCoefUser _SelectedEmpiricalCoefUser;
        public EmpiricalCoefUser SelectedEmpiricalCoefUser
        {
            get { return _SelectedEmpiricalCoefUser; }
            set
            {
                _SelectedEmpiricalCoefUser = value;
                OnPropertyChanged();
                if (_SelectedEmpiricalCoefUser is not null)
                {
                    NameEmpiricalCoef = _SelectedEmpiricalCoefUser.Name_empirical_coef;
                    NameUnit = Unit.FirstOrDefault(x => x.Name_unit == _SelectedEmpiricalCoefUser.Name_unit);
                }
            }
        }

        private string _NameEmpiricalCoef;
        public string NameEmpiricalCoef
        {
            get { return _NameEmpiricalCoef; }
            set
            {
                _NameEmpiricalCoef = value;
                OnPropertyChanged();
            }
        }

        private Unit _NameUnit;
        public Unit NameUnit
        {
            get { return _NameUnit; }
            set
            {
                _NameUnit = value;
                OnPropertyChanged();
            }
        }


        private RelayCommand _AddEmpiricalCoef;

        public RelayCommand AddEmpiricalCoef
        {
            get
            {
                return _AddEmpiricalCoef ??= new RelayCommand(x =>
                {
                    if (NameEmpiricalCoef != string.Empty && NameUnit is not null)
                    {
                        var varEmpiricalCoef = new EmpiricalCoef(NameEmpiricalCoef, NameUnit.ID_unit);

                        var CRUD = new EmpiricalCoefCRUD();

                        CRUD.Create(varEmpiricalCoef);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Заполните все поля, чтобы добавить новую запись", "Ошибка при добавлении записи");
                });
            }
        }

        private RelayCommand _UpdateEmpriricalCoef;

        public RelayCommand UpdateEmpriricalCoef
        {
            get
            {
                return _UpdateEmpriricalCoef ??= new RelayCommand(x =>
                {

                    if (NameEmpiricalCoef != string.Empty && NameUnit is not null)
                    {

                        var varEmpiricalCoef = new EmpiricalCoef(SelectedEmpiricalCoefUser.ID_empirical_coef, NameEmpiricalCoef, NameUnit.ID_unit);

                        var CRUD = new EmpiricalCoefCRUD();

                        CRUD.Update(varEmpiricalCoef);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Выберите строку, которую следует изменить", "Ошибка при изменении записи");
                });
            }
        }

        private RelayCommand _RemoveEmpiricalCoef;

        public RelayCommand RemoveEmpiricalCoef
        {
            get
            {
                return _RemoveEmpiricalCoef ??= new RelayCommand(x =>
                {
                    if (NameEmpiricalCoef != string.Empty && NameUnit is not null)
                    {
                        var CRUD = new EmpiricalCoefCRUD();

                        var varEmpiricalCoef = new EmpiricalCoef(SelectedEmpiricalCoefUser.ID_empirical_coef, SelectedEmpiricalCoefUser.Name_empirical_coef, Unit.FirstOrDefault(x => x.Name_unit == SelectedEmpiricalCoefUser.Name_unit).ID_unit);

                        CRUD.Delete(varEmpiricalCoef);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Выберите строку, которую следует удалить", "Ошибка при удалении записи");
                });
            }
        }

        #endregion

        private List<CharacteristicMaterial> _CharacteristicMaterial;
        public List<CharacteristicMaterial> CharacteristicMaterial
        {
            get { return _CharacteristicMaterial; }
            set
            {
                _CharacteristicMaterial = value;
                OnPropertyChanged();
            }
        }


        private List<ValueCharacteristicMaterial> _ValueCharacteristicMaterial;
        public List<ValueCharacteristicMaterial> ValueCharacteristicMaterial
        {
            get { return _ValueCharacteristicMaterial; }
            set
            {
                _ValueCharacteristicMaterial = value;
                OnPropertyChanged();
            }
        }

        private List<ValueEmpiricalCoef> _ValueEmpiricalCoef;
        public List<ValueEmpiricalCoef> ValueEmpiricalCoef
        {
            get { return _ValueEmpiricalCoef; }
            set
            {
                _ValueEmpiricalCoef = value;
                OnPropertyChanged();
            }
        }

        private AuthWindow? _AuthWindow = null;
        private AuthWindowViewModel _AuthWindowViewModel;

        private RelayCommand _ShowAuth;

        public RelayCommand ShowAuth
        {
            get
            {
                return _ShowAuth ??= new RelayCommand(x =>
                {
                    _AuthWindowViewModel = new AuthWindowViewModel();
                    _AuthWindow = new AuthWindow();
                    _AuthWindow.DataContext = _AuthWindowViewModel;
                    _AuthWindow.Show();


                    ((Window)x).Hide();
                });
            }
        }
    }
}
