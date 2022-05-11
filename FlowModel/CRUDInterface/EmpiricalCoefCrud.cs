using FlowModel.InteractionDB;
using System.Collections.Generic;
using System.Linq;

namespace FlowModel.CRUDInterface
{
    internal class EmpiricalCoefCRUD : ICRUD<EmpiricalCoef>
    {
        public List<EmpiricalCoef> _EmpiricalCoef { get; set; }

        public EmpiricalCoefCRUD()
        {
            using FlowModelContext _context = new();
            _EmpiricalCoef = _context.Empirical_coef.ToList();
        }

        public void Create(EmpiricalCoef item)
        {
            using FlowModelContext _context = new();
            _context.Empirical_coef.Add(item);
            _context.SaveChanges();
        }

        public void Delete(EmpiricalCoef item)
        {
            using FlowModelContext _context = new();
            _context.Empirical_coef.Remove(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var s = _EmpiricalCoef.Find(x => x.ID_empirical_coef == id);
            using FlowModelContext _context = new();
            _context.Empirical_coef.Remove(s);
            _context.SaveChanges();
        }

        public EmpiricalCoef Read(int id)
        {
            return _EmpiricalCoef.Find(x => x.ID_empirical_coef == id);
        }

        public void Update(EmpiricalCoef item)
        {
            var s = _EmpiricalCoef.Find(x => x.ID_empirical_coef == item.ID_empirical_coef);
            s.Name_empirical_coef = item.Name_empirical_coef;
            s.ID_unit = item.ID_unit;

            using FlowModelContext _context = new();
            _context.Empirical_coef.Update(s);
            _context.SaveChanges();
        }
    }
}
