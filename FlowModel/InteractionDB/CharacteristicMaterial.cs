using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace FlowModel.InteractionDB
{
    internal class CharacteristicMaterial
    {
        [Key]
        /// <summary>
        /// ID характеристики материала
        /// </summary>
        public int ID_characteristic { get; set; }

        /// <summary>
        /// Наименование материала
        /// </summary>
        public string Name_characteristic { get; set; }

        //public virtual ICollection<ValueCharacteristicMaterial>
        //    ValueCharacteristicMaterials
        //{ get; private set; } =
        //    new ObservableCollection<ValueCharacteristicMaterial>();
    }
}
