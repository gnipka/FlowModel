using FlowModel.InteractionDB;
using System.Collections.Generic;
using System.Linq;

namespace FlowModel.CRUDInterface
{
    internal class MaterialCRUD : ICRUD<Material>
    {
        public List<Material> _Material { get; set; }

        public MaterialCRUD()
        {
            using FlowModelContext _context = new();
            _Material = _context.Material.ToList();
        }

        public void Create(Material item)
        {
            using FlowModelContext _context = new();
            _context.Material.Add(item);
            _context.SaveChanges();
        }

        public void Delete(Material item)
        {
            using FlowModelContext _context = new();
            _context.Material.Remove(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var s = _Material.Find(x => x.ID_material == id);
            using FlowModelContext _context = new();
            _context.Material.Remove(s);
            _context.SaveChanges();
        }

        public Material Read(int id)
        {
            return _Material.Find(x => x.ID_material == id);
        }

        public void Update(Material item)
        {
            var s = _Material.Find(x => x.ID_material == item.ID_material);
            s.Name_material = item.Name_material;

            using FlowModelContext _context = new();
            _context.Material.Update(s);
            _context.SaveChanges();
        }
    }
}
