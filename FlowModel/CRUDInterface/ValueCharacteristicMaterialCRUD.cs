using FlowModel.InteractionDB;
using System.Collections.Generic;
using System.Linq;

namespace FlowModel.CRUDInterface
{
    internal class ValueCharacteristicMaterialCRUD : ICRUD<ValueCharacteristicMaterial>
    {
        public List<ValueCharacteristicMaterial> _ValueCharacteristicMaterial { get; set; }

        public ValueCharacteristicMaterialCRUD()
        {
            using FlowModelContext _context = new();
            _ValueCharacteristicMaterial = _context.Value_Characteristic_Material.ToList();
        }

        public void Create(ValueCharacteristicMaterial item)
        {
            using FlowModelContext _context = new();
            _context.Value_Characteristic_Material.Add(item);
            _context.SaveChanges();
        }

        public void Delete(ValueCharacteristicMaterial item)
        {
            using FlowModelContext _context = new();
            _context.Value_Characteristic_Material.Remove(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var s = _ValueCharacteristicMaterial.Find(x => x.Id == id);
            using FlowModelContext _context = new();
            _context.Value_Characteristic_Material.Remove(s);
            _context.SaveChanges();
        }

        public ValueCharacteristicMaterial Read(int id)
        {
            return _ValueCharacteristicMaterial.Find(x => x.Id == id);
        }

        public void Update(ValueCharacteristicMaterial item)
        {
            var s = _ValueCharacteristicMaterial.Find(x => x.Id == item.Id);
            s.ID_material = item.ID_material;
            s.ID_characteristic = item.ID_characteristic;
            s.Value_characteristic = item.Value_characteristic;

            using FlowModelContext _context = new();
            _context.Value_Characteristic_Material.Update(s);
            _context.SaveChanges();
        }
    }
}
