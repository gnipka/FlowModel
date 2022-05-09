using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FlowModel.InteractionDB
{
    internal class Material
    {
        private FlowModelContext _FlowModelContext;

        public Material()
        {

        }

        public Material(string name)
        {
            _FlowModelContext = new FlowModelContext();

            var value = _FlowModelContext.Material.Select(x => x.ID_material).ToArray();
            Array.Sort(value);

            ID_material = value[value.Length - 1] + 1;
            Name_material = name;
        }

        public Material(int id, string name)
        {
            ID_material = id;
            Name_material = name;
        }

        [Key]
        /// <summary>
        /// ID материала
        /// </summary>
        public int ID_material { get; set; }

        /// <summary>
        /// Наименование материала
        /// </summary>
        public string Name_material { get; set; }
    }
}
