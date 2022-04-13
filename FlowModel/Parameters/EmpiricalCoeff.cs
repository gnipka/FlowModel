using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowModel.Parameters
{
    /// <summary>
    /// Эмпирические коэффициенты
    /// </summary>
    public class EmpiricalCoeff
    {
        /// <summary>
        /// Коэффициент консистенции при температуре приведения
        /// </summary>
        public double ConsistencyCoeff { get; set; }

        /// <summary>
        /// Температурный коэффициент вязкости
        /// </summary>
        public double TemperatureCoeffViscosity { get; set; }

        /// <summary>
        /// Температура приведения
        /// </summary>
        public double CastingTemperature { get; set; }

        /// <summary>
        /// Индекс течения
        /// </summary>
        public int CurrentIndex { get; set; }

        /// <summary>
        /// Коэффициент теплоотдачи от крышки канала к материалу
        /// </summary>
        public double HeatTransferCoeff { get; set; }
    }
}
