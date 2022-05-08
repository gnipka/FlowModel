using System.ComponentModel.DataAnnotations;

namespace FlowModel.InteractionDB
{
    internal class ValueCharacteristicMaterial
    {
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

        //public virtual Material Material { get; set; }

        //public virtual CharacteristicMaterial CharacteristicMaterial { get; set; }
    }
}
