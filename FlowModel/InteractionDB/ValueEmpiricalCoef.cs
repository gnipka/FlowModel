using System.ComponentModel.DataAnnotations;

namespace FlowModel.InteractionDB
{
    internal class ValueEmpiricalCoef
    {
        [Key]
        /// <summary>
        /// ID значения эмпирического коэффициента
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// ID материала
        /// </summary>
        public int ID_material { get; set; }

        /// <summary>
        /// ID эмпирического коэффициента
        /// </summary>
        public int ID_empirical_coef { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        public double Value_empirical_coef { get; set; }

        //public virtual Material Material { get; set; }

        //public virtual EmpiricalCoef EmpiricalCoef { get; set; }
    }
}
