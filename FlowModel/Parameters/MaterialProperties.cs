using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowModel.Parameters
{
    /// <summary>
    /// Свойства материала
    /// </summary>
    public class MaterialProperties
    {
        /// <summary>
        /// Плотность
        /// </summary>
        public double Density { get; set; }

        /// <summary>
        /// Удельная теплоемкость
        /// </summary>
        public double HeatCapacity { get; set; }

        /// <summary>
        /// Температура плавления
        /// </summary>
        public double MeltingPoint { get; set; }
    }
}
