using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowModel.OutputData
{
    /// <summary>
    /// Выходные параметры
    /// </summary>
    public class OutputParameter
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
        /// Время счета
        /// </summary>
        public TimeSpan Time { get; set; }

        /// <summary>
        /// Массив выходных значений
        /// </summary>
        public ProcessStateParameters[]? ProcessStateParameters { get; set; }

    }
}
