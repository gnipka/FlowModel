using FlowModel.MathOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        #endregion
    }
}
