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

            _FlowModelContext.Characteristic_material.Load();
            CharacteristicMaterial = _FlowModelContext.Characteristic_material.ToList();

            CharacteristicMaterialUser = new List<CharacteristicMaterialUser>();
            foreach (var item in CharacteristicMaterial)
            {
                var empty = new CharacteristicMaterialUser { ID_characteristic = item.ID_characteristic, Name_characteristic = item.Name_characteristic, Name_unit = Unit.FirstOrDefault(x => x.ID_unit == item.ID_unit).Name_unit };

                CharacteristicMaterialUser.Add(empty);
            }

            _FlowModelContext.Value_Characteristic_Material.Load();
            ValueCharacteristicMaterial = _FlowModelContext.Value_Characteristic_Material.ToList();

            ValueCharacteristicMaterialUser = new List<ValueCharacteristicMaterialUser>();
            foreach (var item in ValueCharacteristicMaterial)
            {
                var empty = new ValueCharacteristicMaterialUser { Id = item.Id, Name_characteristic = CharacteristicMaterial.FirstOrDefault(x => x.ID_characteristic == item.ID_characteristic).Name_characteristic, Name_material = Material.FirstOrDefault(x => x.ID_material == item.ID_material).Name_material, Value_characteristic = item.Value_characteristic };

                ValueCharacteristicMaterialUser.Add(empty);
            }

            _FlowModelContext.Value_Empirical_Coef.Load();
            ValueEmpiricalCoef = _FlowModelContext.Value_Empirical_Coef.ToList();

            ValueEmpiricalCoefUser = new List<ValueEmpiricalCoefUser>();
            foreach (var item in ValueEmpiricalCoef)
            {
                var empty = new ValueEmpiricalCoefUser { ID = item.ID, Name_empirical_coef = EmpiricalCoef.FirstOrDefault(x => x.ID_empirical_coef == item.ID_empirical_coef).Name_empirical_coef, Name_material = Material.FirstOrDefault(x => x.ID_material == item.ID_material).Name_material, Value_empirical_coef = item.Value_empirical_coef };

                ValueEmpiricalCoefUser.Add(empty);
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
                    if (NewMaterial != string.Empty && NewMaterial != "" && NewMaterial != null)
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
                        MessageBox.Show("Выберите строку, которую следует изменить или проверьте, что все поля заполнены", "Ошибка при изменении записи");
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
                        if (ValueCharacteristicMaterial.FirstOrDefault(x => x.ID_material == SelectedItem.ID_material) is null &&  ValueEmpiricalCoef.FirstOrDefault(x => x.ID_material == SelectedItem.ID_material) is null)
                        {
                            var CRUD = new MaterialCRUD();

                            CRUD.Delete(SelectedItem);

                            Advertisement();
                        }

                        else
                        {
                            MessageBox.Show("Выбранная запись используется в другой таблице. Вы не можете ее удалить.", "Ошибка при удалении записи");
                        }
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
                    if (NewUnit != string.Empty && NewUnit != "" && NewUnit != null)
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
                        MessageBox.Show("Выберите строку, которую следует изменить или проверьте, что все поля заполнены", "Ошибка при изменении записи");
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
                        if(EmpiricalCoef.FirstOrDefault(x => x.ID_unit == SelectedUnit.ID_unit) is null && CharacteristicMaterial.FirstOrDefault(x => x.ID_unit == SelectedUnit.ID_unit) is null)
                        {
                            var CRUD = new UnitCRUD();

                            CRUD.Delete(SelectedUnit);
                        }
                        
                        else
                            MessageBox.Show("Выбранная запись используется в другой таблице. Вы не можете ее удалить.", "Ошибка при удалении записи");

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
                    if (NewLogin != string.Empty && NewLogin != "" && NewLogin != null && NewPass != string.Empty && NewPass != "" && NewPass != null && NewRole != string.Empty && NewRole != "" && NewRole != null)
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
                        MessageBox.Show("Выберите строку, которую следует изменить или проверьте, что все поля заполнены", "Ошибка при изменении записи");
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
                        if (User.Count(x => x.Role == "Администратор") > 1)
                        {
                            var CRUD = new UserCRUD();

                            CRUD.Delete(SelectedUser);
                        }
                        else
                            MessageBox.Show("Вы не можете удалить последнего зарегистрированного администратора", "Ошибка при удалении записи");

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
                    if (NameEmpiricalCoef != string.Empty && NameEmpiricalCoef != "" && NameEmpiricalCoef != null && NameUnit is not null)
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

                    if (NameEmpiricalCoef != string.Empty && NameEmpiricalCoef != "" && NameEmpiricalCoef != null && NameUnit is not null)
                    {

                        var varEmpiricalCoef = new EmpiricalCoef(SelectedEmpiricalCoefUser.ID_empirical_coef, NameEmpiricalCoef, NameUnit.ID_unit);

                        var CRUD = new EmpiricalCoefCRUD();

                        CRUD.Update(varEmpiricalCoef);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Выберите строку, которую следует изменить или проверьте, что все поля заполнены", "Ошибка при изменении записи");
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
                    if (SelectedEmpiricalCoefUser is not null)
                    {
                        if(ValueEmpiricalCoef.FirstOrDefault(x => x.ID_empirical_coef == SelectedEmpiricalCoefUser.ID_empirical_coef) is null)
                        {
                            var CRUD = new EmpiricalCoefCRUD();

                            var varEmpiricalCoef = new EmpiricalCoef(SelectedEmpiricalCoefUser.ID_empirical_coef, SelectedEmpiricalCoefUser.Name_empirical_coef, Unit.FirstOrDefault(x => x.Name_unit == SelectedEmpiricalCoefUser.Name_unit).ID_unit);

                            CRUD.Delete(varEmpiricalCoef);
                        }
                        else
                            MessageBox.Show("Выбранная запись используется в другой таблице. Вы не можете ее удалить.", "Ошибка при удалении записи");


                        Advertisement();
                    }
                    else
                        MessageBox.Show("Выберите строку, которую следует удалить", "Ошибка при удалении записи");
                });
            }
        }

        #endregion

        #region CharacteristicMaterialConnectDB

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

        private List<CharacteristicMaterialUser> _CharacteristicMaterialUser;
        public List<CharacteristicMaterialUser> CharacteristicMaterialUser
        {
            get { return _CharacteristicMaterialUser; }
            set
            {
                _CharacteristicMaterialUser = value;
                OnPropertyChanged();
            }
        }

        private CharacteristicMaterialUser _SelectedCharacteristicMaterialUser;
        public CharacteristicMaterialUser SelectedCharacteristicMaterialUser
        {
            get { return _SelectedCharacteristicMaterialUser; }
            set
            {
                _SelectedCharacteristicMaterialUser = value;
                OnPropertyChanged();
                if (_SelectedCharacteristicMaterialUser is not null)
                {
                    NameCharacteristicMaterial = _SelectedCharacteristicMaterialUser.Name_characteristic;
                    NameUnitCharacteristic = Unit.FirstOrDefault(x => x.Name_unit == SelectedCharacteristicMaterialUser.Name_unit);
                }
            }
        }

        private string _NameCharacteristicMaterial;
        public string NameCharacteristicMaterial
        {
            get { return _NameCharacteristicMaterial; }
            set
            {
                _NameCharacteristicMaterial = value;
                OnPropertyChanged();
            }
        }

        private Unit _NameUnitCharacteristic;
        public Unit NameUnitCharacteristic
        {
            get { return _NameUnitCharacteristic; }
            set
            {
                _NameUnitCharacteristic = value;
                OnPropertyChanged();
            }
        }


        private RelayCommand _AddCharacteristicMaterial;

        public RelayCommand AddCharacteristicMaterial
        {
            get
            {
                return _AddCharacteristicMaterial ??= new RelayCommand(x =>
                {
                    if (NameCharacteristicMaterial != string.Empty && NameCharacteristicMaterial != "" && NameCharacteristicMaterial != null && NameUnitCharacteristic is not null)
                    {
                        var varCharacteristicMaterial = new CharacteristicMaterial(NameCharacteristicMaterial, NameUnitCharacteristic.ID_unit);

                        var CRUD = new CharacteristicMaterialCRUD();

                        CRUD.Create(varCharacteristicMaterial);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Заполните все поля, чтобы добавить новую запись", "Ошибка при добавлении записи");
                });
            }
        }

        private RelayCommand _UpdateCharacteristicMaterial;

        public RelayCommand UpdateCharacteristicMaterial
        {
            get
            {
                return _UpdateCharacteristicMaterial ??= new RelayCommand(x =>
                {

                    if (NameCharacteristicMaterial != string.Empty && NameCharacteristicMaterial != "" && NameCharacteristicMaterial != null && NameUnitCharacteristic is not null)
                    {

                        var varCharacteristicMaterial = new CharacteristicMaterial(SelectedCharacteristicMaterialUser.ID_characteristic, NameCharacteristicMaterial, NameUnitCharacteristic.ID_unit);

                        var CRUD = new CharacteristicMaterialCRUD();

                        CRUD.Update(varCharacteristicMaterial);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Выберите строку, которую следует изменить или проверьте, что все поля заполнены", "Ошибка при изменении записи");
                });
            }
        }

        private RelayCommand _RemoveCharacteristicMaterial;

        public RelayCommand RemoveCharacteristicMaterial
        {
            get
            {
                return _RemoveCharacteristicMaterial ??= new RelayCommand(x =>
                {
                    if (SelectedCharacteristicMaterialUser is not null)
                    {
                        if(ValueCharacteristicMaterial.FirstOrDefault(x => x.ID_characteristic == SelectedCharacteristicMaterialUser.ID_characteristic) is null)
                        {
                            var CRUD = new CharacteristicMaterialCRUD();

                            var varCharacteristicMaterial = new CharacteristicMaterial(SelectedCharacteristicMaterialUser.ID_characteristic, SelectedCharacteristicMaterialUser.Name_characteristic, Unit.FirstOrDefault(x => x.Name_unit == SelectedCharacteristicMaterialUser.Name_unit).ID_unit);

                            CRUD.Delete(varCharacteristicMaterial);
                        }
                        else
                            MessageBox.Show("Выбранная запись используется в другой таблице. Вы не можете ее удалить.", "Ошибка при удалении записи");

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Выберите строку, которую следует удалить", "Ошибка при удалении записи");
                });
            }
        }

        #endregion

        #region ValueCharacteristicMaterialConnectDB

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

        private List<ValueCharacteristicMaterialUser> _ValueCharacteristicMaterialUser;
        public List<ValueCharacteristicMaterialUser> ValueCharacteristicMaterialUser
        {
            get { return _ValueCharacteristicMaterialUser; }
            set
            {
                _ValueCharacteristicMaterialUser = value;
                OnPropertyChanged();
            }
        }

        private ValueCharacteristicMaterialUser _SelectedValueCharacteristicMaterialUser;
        public ValueCharacteristicMaterialUser SelectedValueCharacteristicMaterialUser
        {
            get { return _SelectedValueCharacteristicMaterialUser; }
            set
            {
                _SelectedValueCharacteristicMaterialUser = value;
                OnPropertyChanged();
                if (_SelectedValueCharacteristicMaterialUser is not null)
                {
                    NameMaterialCharacteristic = Material.FirstOrDefault(x => x.Name_material == _SelectedValueCharacteristicMaterialUser.Name_material);
                    NameCharacteristicValue = CharacteristicMaterial.FirstOrDefault(x => x.Name_characteristic == _SelectedValueCharacteristicMaterialUser.Name_characteristic);
                    ValueCharacteristic = _SelectedValueCharacteristicMaterialUser.Value_characteristic;
                }
            }
        }

        private Material _NameMaterialCharacteristic;
        public Material NameMaterialCharacteristic
        {
            get { return _NameMaterialCharacteristic; }
            set
            {
                _NameMaterialCharacteristic = value;
                OnPropertyChanged();
            }
        }

        private CharacteristicMaterial _NameCharacteristicValue;
        public CharacteristicMaterial NameCharacteristicValue
        {
            get { return _NameCharacteristicValue; }
            set
            {
                _NameCharacteristicValue = value;
                OnPropertyChanged();
            }
        }

        private double _ValueCharacteristic;
        public double ValueCharacteristic
        {
            get { return _ValueCharacteristic; }
            set { _ValueCharacteristic = value; OnPropertyChanged(); }
        }

        private RelayCommand _AddCharacteristicMaterialValue;

        public RelayCommand AddCharacteristicMaterialValue
        {
            get
            {
                return _AddCharacteristicMaterialValue ??= new RelayCommand(x =>
                {
                    if (ValueCharacteristic.ToString() != string.Empty && ValueCharacteristic.ToString() != "" && ValueCharacteristic.ToString() != null && NameCharacteristicValue is not null && NameMaterialCharacteristic is not null)
                    {
                        var varCharacteristicMaterialValue = new ValueCharacteristicMaterial(NameMaterialCharacteristic.ID_material, NameCharacteristicValue.ID_characteristic, ValueCharacteristic);

                        var CRUD = new ValueCharacteristicMaterialCRUD();

                        CRUD.Create(varCharacteristicMaterialValue);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Заполните все поля, чтобы добавить новую запись", "Ошибка при добавлении записи");
                });
            }
        }

        private RelayCommand _UpdateCharacteristicMaterialValue;

        public RelayCommand UpdateCharacteristicMaterialValue
        {
            get
            {
                return _UpdateCharacteristicMaterialValue ??= new RelayCommand(x =>
                {

                    if (ValueCharacteristic.ToString() != string.Empty && ValueCharacteristic.ToString() != "" && ValueCharacteristic.ToString() != null && NameCharacteristicValue is not null && NameMaterialCharacteristic is not null)
                    {

                        var varCharacteristicMaterialValue = new ValueCharacteristicMaterial(SelectedValueCharacteristicMaterialUser.Id, NameMaterialCharacteristic.ID_material, NameCharacteristicValue.ID_characteristic, ValueCharacteristic);

                        var CRUD = new ValueCharacteristicMaterialCRUD();

                        CRUD.Update(varCharacteristicMaterialValue);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Выберите строку, которую следует изменить или проверьте, что все поля заполнены", "Ошибка при изменении записи");
                });
            }
        }

        private RelayCommand _RemoveCharacteristicMaterialValue;

        public RelayCommand RemoveCharacteristicMaterialValue
        {
            get
            {
                return _RemoveCharacteristicMaterialValue ??= new RelayCommand(x =>
                {
                    //if (ValueCharacteristic.ToString() != string.Empty && NameCharacteristicValue is not null && NameMaterialCharacteristic is not null)
                    if (SelectedValueCharacteristicMaterialUser is not null)
                    {
                        var CRUD = new ValueCharacteristicMaterialCRUD();

                        var varCharacteristicMaterialValue = new ValueCharacteristicMaterial(SelectedValueCharacteristicMaterialUser.Id, Material.FirstOrDefault(x => x.Name_material == SelectedValueCharacteristicMaterialUser.Name_material).ID_material, CharacteristicMaterial.FirstOrDefault(x => x.Name_characteristic == SelectedValueCharacteristicMaterialUser.Name_characteristic).ID_characteristic, SelectedValueCharacteristicMaterialUser.Value_characteristic);

                        CRUD.Delete(varCharacteristicMaterialValue);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Выберите строку, которую следует удалить", "Ошибка при удалении записи");
                });
            }
        }

        #endregion

        #region ValueEmpiricalCoeflConnectDB

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

        private List<ValueEmpiricalCoefUser> _ValueEmpiricalCoefUser;
        public List<ValueEmpiricalCoefUser> ValueEmpiricalCoefUser
        {
            get { return _ValueEmpiricalCoefUser; }
            set
            {
                _ValueEmpiricalCoefUser = value;
                OnPropertyChanged();
            }
        }

        private ValueEmpiricalCoefUser _SelectedValueEmpiricalCoefUser;
        public ValueEmpiricalCoefUser SelectedValueEmpiricalCoefUser
        {
            get { return _SelectedValueEmpiricalCoefUser; }
            set
            {
                _SelectedValueEmpiricalCoefUser = value;
                OnPropertyChanged();
                if (_SelectedValueEmpiricalCoefUser is not null)
                {
                    NameMaterialEmpiricalCoef = Material.FirstOrDefault(x => x.Name_material == _SelectedValueEmpiricalCoefUser.Name_material);
                    NameEmpiricalCoefValue = EmpiricalCoef.FirstOrDefault(x => x.Name_empirical_coef == _SelectedValueEmpiricalCoefUser.Name_empirical_coef);
                    ValueEmpiricalCoefMaterial = _SelectedValueEmpiricalCoefUser.Value_empirical_coef;
                }
            }
        }

        private Material _NameMaterialEmpiricalCoef;
        public Material NameMaterialEmpiricalCoef
        {
            get { return _NameMaterialEmpiricalCoef; }
            set
            {
                _NameMaterialEmpiricalCoef = value;
                OnPropertyChanged();
            }
        }

        private EmpiricalCoef _NameEmpiricalCoefValue;
        public EmpiricalCoef NameEmpiricalCoefValue
        {
            get { return _NameEmpiricalCoefValue; }
            set
            {
                _NameEmpiricalCoefValue = value;
                OnPropertyChanged();
            }
        }

        private double _ValueEmpiricalCoefMaterial;
        public double ValueEmpiricalCoefMaterial
        {
            get { return _ValueEmpiricalCoefMaterial; }
            set { _ValueEmpiricalCoefMaterial = value; OnPropertyChanged(); }
        }

        private RelayCommand _AddEmpiricalCoefValue;

        public RelayCommand AddEmpiricalCoefValue
        {
            get
            {
                return _AddEmpiricalCoefValue ??= new RelayCommand(x =>
                {
                    if (ValueEmpiricalCoefMaterial.ToString() != string.Empty && ValueEmpiricalCoefMaterial.ToString() != "" && ValueEmpiricalCoefMaterial.ToString() != null && NameMaterialEmpiricalCoef is not null && NameEmpiricalCoefValue is not null)
                    {
                        var varEmpiricalCoefValue = new ValueEmpiricalCoef(NameMaterialEmpiricalCoef.ID_material, NameEmpiricalCoefValue.ID_empirical_coef, ValueEmpiricalCoefMaterial);

                        var CRUD = new ValueEmpiricalCoefCRUD();

                        CRUD.Create(varEmpiricalCoefValue);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Заполните все поля, чтобы добавить новую запись", "Ошибка при добавлении записи");
                });
            }
        }

        private RelayCommand _UpdateEmpiricalCoefValue;

        public RelayCommand UpdateEmpiricalCoefValue
        {
            get
            {
                return _UpdateEmpiricalCoefValue ??= new RelayCommand(x =>
                {

                    if (ValueEmpiricalCoefMaterial.ToString() != string.Empty && ValueEmpiricalCoefMaterial.ToString() != "" && ValueEmpiricalCoefMaterial.ToString() != null && NameMaterialEmpiricalCoef is not null && NameEmpiricalCoefValue is not null)
                    {
                        var varEmpiricalCoefValue = new ValueEmpiricalCoef(SelectedValueEmpiricalCoefUser.ID, NameMaterialEmpiricalCoef.ID_material, NameEmpiricalCoefValue.ID_empirical_coef, ValueEmpiricalCoefMaterial);

                        var CRUD = new ValueEmpiricalCoefCRUD();

                        CRUD.Update(varEmpiricalCoefValue);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Выберите строку, которую следует изменить или проверьте, что все поля заполнены", "Ошибка при изменении записи");
                });
            }
        }

        private RelayCommand _RemoveEmpiricalCoefValue;

        public RelayCommand RemoveEmpiricalCoefValue
        {
            get
            {
                return _RemoveEmpiricalCoefValue ??= new RelayCommand(x =>
                {
                    if (SelectedValueEmpiricalCoefUser is not null)
                    {
                        var CRUD = new ValueEmpiricalCoefCRUD();

                        var varEmpiricalCoefValue = new ValueEmpiricalCoef(SelectedValueEmpiricalCoefUser.ID, Material.FirstOrDefault(x => x.Name_material == SelectedValueEmpiricalCoefUser.Name_material).ID_material, EmpiricalCoef.FirstOrDefault(x => x.Name_empirical_coef == SelectedValueEmpiricalCoefUser.Name_empirical_coef).ID_empirical_coef, SelectedValueEmpiricalCoefUser.Value_empirical_coef);

                        CRUD.Delete(varEmpiricalCoefValue);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Выберите строку, которую следует удалить", "Ошибка при удалении записи");
                });
            }
        }

        #endregion

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
