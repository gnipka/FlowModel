using FlowModel.InteractionDB;
using System.Collections.Generic;
using System.Linq;

namespace FlowModel.CRUDInterface
{
    internal class UnitCRUD : ICRUD<Unit>
    {
        public List<Unit> _Unit { get; set; }

        public UnitCRUD()
        {
            using FlowModelContext _context = new();
            _Unit = _context.Unit.ToList();
        }

        public void Create(Unit item)
        {
            using FlowModelContext _context = new();
            _context.Unit.Add(item);
            _context.SaveChanges();
        }

        public void Delete(Unit item)
        {
            using FlowModelContext _context = new();
            _context.Unit.Remove(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var s = _Unit.Find(x => x.ID_unit == id);
            using FlowModelContext _context = new();
            _context.Unit.Remove(s);
            _context.SaveChanges();
        }

        public Unit Read(int id)
        {
            return _Unit.Find(x => x.ID_unit == id);
        }

        public void Update(Unit item)
        {
            var s = _Unit.Find(x => x.ID_unit == item.ID_unit);
            s.Name_unit = item.Name_unit;

            using FlowModelContext _context = new();
            _context.Unit.Update(s);
            _context.SaveChanges();
        }
    }
}
