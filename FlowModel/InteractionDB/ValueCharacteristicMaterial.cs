using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FlowModel.InteractionDB
{
    internal class ValueCharacteristicMaterial
    {
        FlowModelContext _FlowModelContext;

        public ValueCharacteristicMaterial() { }

        public ValueCharacteristicMaterial(int id_material, int id_characteristic, double value_characteristic)
        {
            _FlowModelContext = new FlowModelContext();

            var value = _FlowModelContext.Value_Characteristic_Material.Select(x => x.Id).ToArray();
            Array.Sort(value);

            Id = value[value.Length - 1] + 1;
            ID_material = id_material;
            ID_characteristic = id_characteristic;
            Value_characteristic = value_characteristic;
        }

        public ValueCharacteristicMaterial(int id, int id_material, int id_characteristic, double value_characteristic)
        {
            Id = id;
            ID_material = id_material;
            ID_characteristic = id_characteristic;
            Value_characteristic = value_characteristic;
        }

        [Key]
        /// <summary>
        /// ID для значения характеристики
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ID материала
        /// </summary>
        public int ID_material { get; set; }

        /// <summary>
        /// ID харакстеристики материала
        /// </summary>
        public int ID_characteristic { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        public double Value_characteristic { get; set; }

    }
}
