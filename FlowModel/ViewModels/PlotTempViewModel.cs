using FlowModel.OutputData;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MVVM_Classes;

namespace FlowModel
{
    public class PlotTempViewModel : ViewModelBase
    {
        /// <summary>
        /// Модель для взаимоействия с графиком
        /// </summary>
        private PlotModel _MyModel;

        public PlotModel MyModel
        {
            get
            {
                return _MyModel;
            }
            set
            {
                _MyModel = value;
                OnPropertyChanged();
            }
        }

        private OutputParameter _OutputParameter;

        public PlotTempViewModel(OutputParameter outputParameter)
        {
            _OutputParameter = outputParameter;


            _MyModel = new PlotModel { Title = "График зависимости температуры от координаты по длине канала", TitleFontSize = 16 };
            var line = new LineSeries()
            {
                Color = OxyPlot.OxyColors.Plum,
                StrokeThickness = 1,
                MarkerSize = 2,
                LineStyle = LineStyle.Solid,
                TrackerFormatString = "{2:0.00}, {4:0.00}"
            };
            for (int i = 0; i < _OutputParameter.ProcessStateParameters.Length; i++)
            {
                line.Points.Add(new OxyPlot.DataPoint(_OutputParameter.ProcessStateParameters[i].Coordinate, _OutputParameter.ProcessStateParameters[i].Temperature));
            }

            _MyModel.Padding = new OxyThickness(10, 0, 10, 0);
            _MyModel.Series.Add(line);
            _MyModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Координата по длине канала, м" });
            _MyModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Температура, С" });
        }
    }
}
