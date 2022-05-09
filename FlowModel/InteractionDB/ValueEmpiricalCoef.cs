using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FlowModel.InteractionDB
{
    internal class ValueEmpiricalCoef
    {
        FlowModelContext _FlowModelContext;

        public ValueEmpiricalCoef() { }

        public ValueEmpiricalCoef(int id_material, int id_empirical_coef, double value_empirical_coef)
        {
            _FlowModelContext = new FlowModelContext();

            var value = _FlowModelContext.Value_Empirical_Coef.Select(x => x.ID).ToArray();
            Array.Sort(value);

            ID = value[value.Length - 1] + 1;
            ID_material = id_material;
            ID_empirical_coef = id_empirical_coef;
            Value_empirical_coef = value_empirical_coef;
        }

        public ValueEmpiricalCoef(int id, int id_material, int id_empirical_coef, double value_empirical_coef)
        {
            ID = id;
            ID_material = id_material;
            ID_empirical_coef = id_empirical_coef;
            Value_empirical_coef = value_empirical_coef;
        }

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
    }
}
