using FlowModel.InteractionDB;
using System.Collections.Generic;
using System.Linq;

namespace FlowModel.CRUDInterface
{
    internal class CharacteristicMaterialCRUD : ICRUD<CharacteristicMaterial>
    {
        public List<CharacteristicMaterial> _CharacteristicMaterials { get; set; }

        public CharacteristicMaterialCRUD()
        {
            using FlowModelContext _context = new();
            _CharacteristicMaterials = _context.Characteristic_material.ToList();
        }

        public void Create(CharacteristicMaterial item)
        {
            using FlowModelContext _context = new();
            _context.Characteristic_material.Add(item);
            _context.SaveChanges();
        }

        public void Delete(CharacteristicMaterial item)
        {
            using FlowModelContext _context = new();
            _context.Characteristic_material.Remove(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var s = _CharacteristicMaterials.Find(x => x.ID_characteristic == id);
            using FlowModelContext _context = new();
            _context.Characteristic_material.Remove(s);
            _context.SaveChanges();
        }

        public CharacteristicMaterial Read(int id)
        {
            return _CharacteristicMaterials.Find(x => x.ID_characteristic == id);
        }

        public void Update(CharacteristicMaterial item)
        {
            var s = _CharacteristicMaterials.Find(x => x.ID_characteristic == item.ID_characteristic);
            s.Name_characteristic = item.Name_characteristic;
            s.ID_unit = item.ID_unit;

            using FlowModelContext _context = new();
            _context.Characteristic_material.Update(s);
            _context.SaveChanges();
        }
    }
}
