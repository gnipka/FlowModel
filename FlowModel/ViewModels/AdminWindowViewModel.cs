using FlowModel.InteractionDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MVVM_Classes;

namespace FlowModel.ViewModels
{
    internal class AdminWindowViewModel : ViewModelBase
    {
        private FlowModelContext _FlowModelContext;

        public AdminWindowViewModel()
        {
            _FlowModelContext = new FlowModelContext();
            _FlowModelContext.User.Load();
            User = _FlowModelContext.User.ToList();
            _FlowModelContext.Material.Load();
            Material = _FlowModelContext.Material.ToList();
            _FlowModelContext.Characteristic_material.Load();
            CharacteristicMaterial = _FlowModelContext.Characteristic_material.ToList();
            _FlowModelContext.Empirical_coef.Load();
            EmpiricalCoef = _FlowModelContext.Empirical_coef.ToList();
            //_FlowModelContext.Value_Characteristic_Material.Load();
            //ValueCharacteristicMaterial = _FlowModelContext.Value_Characteristic_Material.ToList();
            //_FlowModelContext.Value_Empirical_Coef.Load();
            //ValueEmpiricalCoef = _FlowModelContext.Value_Empirical_Coef.ToList();
        }

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

    }
}
