using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowModel.InteractionDB
{
    internal class Unit
    {
        private FlowModelContext _FlowModelContext;

        public Unit()
        {

        }

        public Unit(string name)
        {
            _FlowModelContext = new FlowModelContext();

            var value = _FlowModelContext.Unit.Select(x => x.ID_unit).ToArray();
            Array.Sort(value);

            ID_unit = value[value.Length - 1] + 1;
            Name_unit = name;
        }

        public Unit(int id, string name)
        {
            ID_unit = id;
            Name_unit = name;
        }

        [Key]
        /// <summary>
        /// ID единицы измерения
        /// </summary>
        public int ID_unit { get; set; }

        /// <summary>
        /// Единица измерения
        /// </summary>
        public string Name_unit { get; set; }
    }
}
