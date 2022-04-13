using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowModel.InputData
{
    /// <summary>
    /// Выходные параметры
    /// </summary>
    public class InputParameter
    {
        /// <summary>
        /// Производительность
        /// </summary>
        public double Efficiency { get; set; }

        /// <summary>
        /// Температура продукта
        /// </summary>
        public double TemperatureProduct { get; set; }

        /// <summary>
        /// Вязкость продукта
        /// </summary>
        public double ViscosityProduct { get; set; }

        /// <summary>
        /// Массив выходных значений
        /// </summary>
        public ProcessStateParameters[]? ProcessStateParameters { get; set; }

    }
}
