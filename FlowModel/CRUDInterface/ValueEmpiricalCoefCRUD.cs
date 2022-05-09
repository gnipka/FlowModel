using FlowModel.InteractionDB;
using System.Collections.Generic;
using System.Linq;

namespace FlowModel.CRUDInterface
{
    internal class ValueEmpiricalCoefCRUD : ICRUD<ValueEmpiricalCoef>
    {
        public List<ValueEmpiricalCoef> _ValueEmpiricalCoef { get; set; }

        public ValueEmpiricalCoefCRUD()
        {
            using FlowModelContext _context = new();
            _ValueEmpiricalCoef = _context.Value_Empirical_Coef.ToList();
        }

        public void Create(ValueEmpiricalCoef item)
        {
            using FlowModelContext _context = new();
            _context.Value_Empirical_Coef.Add(item);
            _context.SaveChanges();
        }

        public ValueEmpiricalCoef Read(int id)
        {
            return _ValueEmpiricalCoef.Find(x => x.ID == id);
        }

        public void Update(ValueEmpiricalCoef item)
        {
            var s = _ValueEmpiricalCoef.Find(x => x.ID == item.ID);
            s.ID_material = item.ID_material;
            s.ID_empirical_coef = item.ID_empirical_coef;
            s.Value_empirical_coef = item.Value_empirical_coef;

            using FlowModelContext _context = new();
            _context.Value_Empirical_Coef.Update(s);
            _context.SaveChanges();
        }

        public void Delete(ValueEmpiricalCoef item)
        {
            using FlowModelContext _context = new();
            _context.Value_Empirical_Coef.Remove(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var s = _ValueEmpiricalCoef.Find(x => x.ID == id);
            using FlowModelContext _context = new();
            _context.Value_Empirical_Coef.Remove(s);
            _context.SaveChanges();
        }
    }
}
