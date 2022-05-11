using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FlowModel.InteractionDB
{
    internal class EmpiricalCoef
    {
        FlowModelContext _FlowModelContext;

        public EmpiricalCoef() { }

        public EmpiricalCoef(string name, int id_unit)
        {
            _FlowModelContext = new FlowModelContext();

            var value = _FlowModelContext.Empirical_coef.Select(x => x.ID_empirical_coef).ToArray();
            Array.Sort(value);

            ID_empirical_coef = value[value.Length - 1] + 1;
            Name_empirical_coef = name;
            ID_unit = id_unit;
        }

        public EmpiricalCoef(int id, string name, int id_unit)
        {
            ID_empirical_coef = id;
            Name_empirical_coef = name;
            ID_unit = id_unit;
        }

        [Key]
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
        public int ID_unit { get; set; }
    }
}
