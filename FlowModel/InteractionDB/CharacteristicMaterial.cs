using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FlowModel.InteractionDB
{
    internal class CharacteristicMaterial
    {            
        FlowModelContext _FlowModelContext;

        public CharacteristicMaterial() { }

        public CharacteristicMaterial(string name, int id_unit)
        {
            _FlowModelContext = new FlowModelContext();

            var value = _FlowModelContext.Characteristic_material.Select(x => x.ID_characteristic).ToArray();
            Array.Sort(value);

            ID_characteristic = value[value.Length - 1] + 1;
            Name_characteristic = name;
            ID_unit = id_unit;
        }

        public CharacteristicMaterial(int id, string name, int id_unit)
        {
            ID_characteristic = id;
            Name_characteristic = name;
            ID_unit = id_unit;
        }

        [Key]
        /// <summary>
        /// ID характеристики материала
        /// </summary>
        public int ID_characteristic { get; set; }

        /// <summary>
        /// Наименование материала
        /// </summary>
        public string Name_characteristic { get; set; }

        /// <summary>
        /// ID единицы измерения
        /// </summary>
        public int ID_unit { get; set; }
    }
}
