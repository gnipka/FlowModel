using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace FlowModel.InteractionDB
{
    internal class Material
    {
        [Key]
        /// <summary>
        /// ID материала
        /// </summary>
        public int ID_material { get; set; }

        /// <summary>
        /// Наименование материала
        /// </summary>
        public string Name_material { get; set; }

        //public virtual ICollection<ValueCharacteristicMaterial>
        //    ValueCharacteristicMaterials
        //{ get; private set; } =
        //    new ObservableCollection<ValueCharacteristicMaterial>();

        //public virtual ICollection<ValueEmpiricalCoef>
        //    ValueEmpiricalCoef
        //{ get; private set; } =
        //    new ObservableCollection<ValueEmpiricalCoef>();
    }
}
