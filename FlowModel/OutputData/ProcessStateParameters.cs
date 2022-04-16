using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowModel.OutputData
{
    /// <summary>
    /// Выходные параметры состояния процесса
    /// </summary>
    public class ProcessStateParameters
    {
        /// <summary>
        /// Координата по длине канала
        /// </summary>
        
        public double Coordinate { get; set; } 
        
        /// <summary>
        /// Температура
        /// </summary>
        public double Temperature { get; set; }

        /// <summary>
        /// Вязкость
        /// </summary>
        public double Viscosity { get; set; }

    }
}
