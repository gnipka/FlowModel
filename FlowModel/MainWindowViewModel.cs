using FlowModel.Parameters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using WPF_MVVM_Classes;

namespace FlowModel
{
    internal class MainWindowViewModel : ViewModelBase
    {

        #region [Input Parametrs]
        private GeometricParameters _GeometricParameters = new GeometricParameters();

        public GeometricParameters GeometricParameters
        { 
            get { return _GeometricParameters; }
            set 
            { 
                _GeometricParameters = value;
                OnPropertyChanged();
            }
        }

        private MaterialProperties _MaterialProperties = new MaterialProperties();
        public MaterialProperties MaterialProperties
        {
            get { return _MaterialProperties; }
            set
            {
                _MaterialProperties = value;
                OnPropertyChanged();
            }
        }

        private VariableParameters _VariableParameters = new VariableParameters();
        public VariableParameters VariableParameters
        {
            get { return _VariableParameters; }
            set
            {
                _VariableParameters = value;
                OnPropertyChanged();
            }
        }

        private EmpiricalCoeff _EmpiricalCoeff = new EmpiricalCoeff();
        public EmpiricalCoeff EmpiricalCoeff
        {
            get { return _EmpiricalCoeff; }
            set
            {
                _EmpiricalCoeff = value;
                OnPropertyChanged();
            }
        }

        private ParametersSolution _ParametersSolution = new ParametersSolution();
        public ParametersSolution ParametersSolution
        {
            get { return _ParametersSolution; }
            set
            {
                _ParametersSolution = value;
                OnPropertyChanged();
            }
        }
        #endregion

        private RelayCommand _Test;
        public RelayCommand Test
        {
            get
            {
                return _Test ??= new RelayCommand(x =>
                {;
                    MessageBox.Show(GeometricParameters.Depth.ToString());
                });
            }
        }
    }
}
