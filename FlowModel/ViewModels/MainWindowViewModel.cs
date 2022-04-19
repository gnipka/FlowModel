﻿using FlowModel.MathPart;
using FlowModel.OutputData;
using FlowModel.Parameters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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
        public MainWindowViewModel()
        {
            CalcMemory();
        }

        async void CalcMemory()
        {
            await Task.Run(() => {
                while (true)
                {
                    string prcName = Process.GetCurrentProcess().ProcessName;
                    var counter = new PerformanceCounter("Process", "Working Set - Private", prcName);
                    MemoryParam = (int)(counter.RawValue / (1024* 1024));
                    //MemoryParam = (int)System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / 1024;
                }
            });
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
        private GeometricParameters _GeometricParameters = new GeometricParameters { Depth=0.009, Length=4.5, Width=0.2};

        public GeometricParameters GeometricParameters
        { 
            get { return _GeometricParameters; }
            set 
            { 
                _GeometricParameters = value;
                OnPropertyChanged();
            }
        }

        private MaterialProperties _MaterialProperties = new MaterialProperties { Density = 1060, HeatCapacity = 1200, MeltingPoint = 175};
        public MaterialProperties MaterialProperties
        {
            get { return _MaterialProperties; }
            set
            {
                _MaterialProperties = value;
                OnPropertyChanged();
            }
        }

        private VariableParameters _VariableParameters = new VariableParameters { CoverSpeed = 1.2, CoverTemperature = 220};
        public VariableParameters VariableParameters
        {
            get { return _VariableParameters; }
            set
            {
                _VariableParameters = value;
                OnPropertyChanged();
            }
        }

        private EmpiricalCoeff _EmpiricalCoeff = new EmpiricalCoeff { CastingTemperature = 210, ConsistencyCoeff = 9000, CurrentIndex = 0.3, HeatTransferCoeff = 450, TemperatureCoeffViscosity = 0.02};
        public EmpiricalCoeff EmpiricalCoeff
        {
            get { return _EmpiricalCoeff; }
            set
            {
                _EmpiricalCoeff = value;
                OnPropertyChanged();
            }
        }

        private ParametersSolution _ParametersSolution = new ParametersSolution { Step = 0.1};
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


        private RelayCommand _Calc;
        public RelayCommand Calc
        {
            get
            {
                return _Calc ??= new RelayCommand(x =>
                {
                    MathCalc calc = new MathCalc(EmpiricalCoeff, GeometricParameters, MaterialProperties, ParametersSolution, VariableParameters);

                    calc.GetOutputParameters();                    

                    OutputParameter = calc.InputData;
                    Time = Math.Round(OutputParameter.Time.TotalMilliseconds, 2).ToString();
                });
            }
        }

        private PlotTempWindow _WindowTemp = null;
        private PlotTempViewModel _ViewModelTemp;

        private PlotViscosityWindow _WindowViscosity = null;
        private PlotViscosityViewModel _ViewModelViscosity;

        private RelayCommand _ShowPlot;

        public RelayCommand ShowPlot
        {
            get
            {
                return _ShowPlot ??= new RelayCommand(x =>
                 {
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

        private RelayCommand _Export;

        public RelayCommand Export
        {
            get
            {
                return _Export ??= new RelayCommand(x =>
                {                    
                    var directory = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "Отчеты"));

                    if (!directory.Exists)
                    {
                        directory = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "Отчеты"));
                        directory.Create();
                    }

                    string path = $"Отчет №1. {DateTime.Today.ToShortDateString()}.xlsx";
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