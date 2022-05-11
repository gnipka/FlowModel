using FlowModel.InteractionDB;
using FlowModel.MathPart;
using FlowModel.OutputData;
using FlowModel.Parameters;
using FlowModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPF_MVVM_Classes;

namespace FlowModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private FlowModelContext _FlowModelContext;

        public MainWindowViewModel()
        {
            _FlowModelContext = new FlowModelContext();

            Materials = _FlowModelContext.Material.ToList();

            SelectedMaterial = Materials[0];

            CalcMemory();
        }

        async void CalcMemory()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    string prcName = Process.GetCurrentProcess().ProcessName;
                    var counter = new PerformanceCounter("Process", "Working Set - Private", prcName);
                    MemoryParam = (int)(counter.RawValue / (1024* 1024));
                }
            });
        }

        private void FillBox()
        {
            MaterialProperties = new MaterialProperties
            {
                Density = _FlowModelContext.Value_Characteristic_Material.First(x => x.ID_characteristic == _FlowModelContext.Characteristic_material.First(z => z.Name_characteristic == "Плотность").ID_characteristic && x.ID_material == SelectedMaterial.ID_material).Value_characteristic,
                HeatCapacity = _FlowModelContext.Value_Characteristic_Material.First(x => x.ID_characteristic == _FlowModelContext.Characteristic_material.First(z => z.Name_characteristic == "Удельная теплоемкость").ID_characteristic && x.ID_material == SelectedMaterial.ID_material).Value_characteristic,
                MeltingPoint = _FlowModelContext.Value_Characteristic_Material.First(x => x.ID_characteristic == _FlowModelContext.Characteristic_material.First(z => z.Name_characteristic == "Температура плавления").ID_characteristic && x.ID_material == SelectedMaterial.ID_material).Value_characteristic
            };
            EmpiricalCoeff = new EmpiricalCoeff
            {
                ConsistencyCoeff = _FlowModelContext.Value_Empirical_Coef.First(x => x.ID_empirical_coef == _FlowModelContext.Empirical_coef.First(z => z.Name_empirical_coef == "Коэффициент консистенции при температуре приведения").ID_empirical_coef && x.ID_material == SelectedMaterial.ID_material).Value_empirical_coef,
                TemperatureCoeffViscosity = _FlowModelContext.Value_Empirical_Coef.First(x => x.ID_empirical_coef == _FlowModelContext.Empirical_coef.First(z => z.Name_empirical_coef == "Температурный коэффициент вязкости").ID_empirical_coef && x.ID_material == SelectedMaterial.ID_material).Value_empirical_coef,
                CastingTemperature = _FlowModelContext.Value_Empirical_Coef.First(x => x.ID_empirical_coef == _FlowModelContext.Empirical_coef.First(z => z.Name_empirical_coef == "Температура приведения").ID_empirical_coef && x.ID_material == SelectedMaterial.ID_material).Value_empirical_coef,
                CurrentIndex = _FlowModelContext.Value_Empirical_Coef.First(x => x.ID_empirical_coef == _FlowModelContext.Empirical_coef.First(z => z.Name_empirical_coef == "Индекс течения").ID_empirical_coef && x.ID_material == SelectedMaterial.ID_material).Value_empirical_coef,
                HeatTransferCoeff = _FlowModelContext.Value_Empirical_Coef.First(x => x.ID_empirical_coef == _FlowModelContext.Empirical_coef.First(z => z.Name_empirical_coef == "Коэффициент теплоотдачи от крышки канала к материалу").ID_empirical_coef && x.ID_material == SelectedMaterial.ID_material).Value_empirical_coef
            };


        }

        private int _MemoryParam;
        public int MemoryParam
        {
            get { return _MemoryParam; }
            set
            {
                _MemoryParam = value;
                OnPropertyChanged();
            }
        }

        #region [Input Parametrs]

        private List<Material> _Materials;
        public List<Material> Materials
        {
            get { return _Materials; }
            set
            {
                _Materials = value;
                OnPropertyChanged();
            }
        }


        private Material _SelectedMaterial;

        public Material SelectedMaterial
        {
            get
            {
                return _SelectedMaterial;
            }
            set
            {
                if (_SelectedMaterial != value)
                {
                    _SelectedMaterial = value;
                    OnPropertyChanged();
                    FillBox();
                }
            }
        }

        private GeometricParameters _GeometricParameters = new GeometricParameters { Depth=0.009, Length=4.5, Width=0.2 };

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

        private VariableParameters _VariableParameters = new VariableParameters { CoverSpeed = 1.2, CoverTemperature = 220 };
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

        private ParametersSolution _ParametersSolution = new ParametersSolution { Step = 0.1 };
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

        #region [Output Parameters]

        private OutputParameter _OutputParameter = new OutputParameter();
        public OutputParameter OutputParameter
        {
            get { return _OutputParameter; }
            set
            {
                _OutputParameter = value;
                OnPropertyChanged();
            }
        }

        private string _Time;
        public string Time
        {
            get { return _Time; }
            set
            {
                _Time = value;
                OnPropertyChanged();
            }
        }


        #endregion

        public void CallCalc()
        {
            MathCalc calc = new MathCalc(EmpiricalCoeff, GeometricParameters, MaterialProperties, ParametersSolution, VariableParameters);

            calc.GetOutputParameters();

            OutputParameter = calc.InputData;
            Time = Math.Round(OutputParameter.Time.TotalMilliseconds, 2).ToString();
        }

        public void ProcessElement(DependencyObject element, StringBuilder sb)
        {

            if (element is TextBox)
            {
                if (Validation.GetHasError(element))
                {
                    sb.Append("ошибка:\r\n");
                }
            }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                ProcessElement(VisualTreeHelper.GetChild(element, i), sb);
            }
        }

        private RelayCommand _Calc;
        public RelayCommand Calc
        {
            get
            {
                return _Calc ??= new RelayCommand(x =>
                {
                    StringBuilder sb = new StringBuilder();
                    ProcessElement((DependencyObject)x, sb);

                    string message = sb.ToString();
                    if (message == string.Empty)
                        CallCalc();
                    else
                        MessageBox.Show("Проверьте корректность введенных значений", "Ошибка при валидации данных");
                });
            }
        }

        private PlotTempWindow? _WindowTemp = null;
        private PlotTempViewModel _ViewModelTemp;

        private PlotViscosityWindow? _WindowViscosity = null;
        private PlotViscosityViewModel _ViewModelViscosity;

        private RelayCommand _ShowPlot;

        public RelayCommand ShowPlot
        {
            get
            {
                return _ShowPlot ??= new RelayCommand(x =>
                 {
                     if (OutputParameter.ProcessStateParameters is null)
                     {
                         CallCalc();
                     }

                     _ViewModelTemp = new PlotTempViewModel(OutputParameter);
                     _WindowTemp = new PlotTempWindow();
                     _WindowTemp.DataContext = _ViewModelTemp;
                     _WindowTemp.Show();

                     _ViewModelViscosity = new PlotViscosityViewModel(OutputParameter);
                     _WindowViscosity = new PlotViscosityWindow();
                     _WindowViscosity.DataContext = _ViewModelViscosity;
                     _WindowViscosity.Show();
                 });
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

        private RelayCommand _Export;

        public RelayCommand Export
        {
            get
            {
                return _Export ??= new RelayCommand(x =>
                {
                    if (OutputParameter.ProcessStateParameters is null)
                    {
                        CallCalc();
                    }

                    var directory = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Отчеты"));

                    if (!directory.Exists)
                    {
                        directory = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Отчеты"));
                        directory.Create();
                    }

                    string path = Path.Combine(directory.FullName, $"Отчет №1. {DateTime.Today.ToShortDateString()}.xlsx");
                    var file = new FileInfo(Path.Combine(directory.FullName, path));

                    int num = 1;
                    while (file.Exists)
                    {
                        path = Path.Combine(directory.FullName, $"Отчет №{num}. {DateTime.Today.ToShortDateString()}.xlsx");
                        path.Replace(',', ' ');
                        file = new FileInfo(Path.Combine(path));
                        num++;
                    };

                    ExportExcel.Export(OutputParameter, EmpiricalCoeff, GeometricParameters, MaterialProperties, ParametersSolution, VariableParameters);

                    var result = MessageBox.Show($"Открыть файл в формате Excel? Он также будет сохранен по пути {directory.FullName}", "Экспорт в Excel", MessageBoxButton.YesNo);

                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            ExportExcel.Save(path);
                            break;
                        case MessageBoxResult.No:
                            ExportExcel.SaveAndClose(path);
                            break;
                    }
                });
            }
        }
    }
}
