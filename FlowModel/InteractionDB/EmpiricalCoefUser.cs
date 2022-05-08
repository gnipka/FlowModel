using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowModel.InteractionDB
{
    internal class EmpiricalCoefUser
    {
        /// <summary>
        /// ID эмипирического коэффициента
        /// </summary>
        public int ID_empirical_coef { get; set; }

        /// <summary>
        /// наименование эмпирического коэффициента
        /// </summary>
        public string Name_empirical_coef { get; set; }

        /// <summary>
        /// ID единицы измерения
        /// </summary>
        public string? Name_unit { get; set; }
    }
}
