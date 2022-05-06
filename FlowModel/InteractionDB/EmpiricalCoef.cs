using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace FlowModel.InteractionDB
{
    internal class EmpiricalCoef
    {
        [Key]
        /// <summary>
        /// ID эмипирического коэффициента
        /// </summary>
        public int ID_empirical_coef { get; set; }
        /// <summary>
        /// наименование эмпирического коэффициента
        /// </summary>
        public string Name_empirical_coef { get; set; }

        //public virtual ICollection<ValueEmpiricalCoef>
        //    ValueEmpiricalCoef
        //{ get; private set; } =
        //    new ObservableCollection<ValueEmpiricalCoef>();
    }
}
