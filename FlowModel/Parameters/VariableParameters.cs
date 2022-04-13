using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowModel.Parameters
{
    /// <summary>
    /// Варьируемые параметры
    /// </summary>
    public class VariableParameters
    {
        /// <summary>
        /// Скорость крышки
        /// </summary>
        public double CoverSpeed { get; set; }

        /// <summary>
        /// Температура крышки
        /// </summary>
        public double CoverTemperature { get; set; }
    }
}
