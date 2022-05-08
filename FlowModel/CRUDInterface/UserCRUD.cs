using FlowModel.InteractionDB;
using System.Collections.Generic;
using System.Linq;

namespace FlowModel.CRUDInterface
{
    internal class UserCRUD : ICRUD<User>
    {
        public List<User> _User { get; set; }

        public UserCRUD()
        {
            using FlowModelContext _context = new();
            _User = _context.User.ToList();
        }

        public void Create(User item)
        {
            using FlowModelContext _context = new();
            _context.User.Add(item);
            _context.SaveChanges();
        }

        public void Delete(User item)
        {
            using FlowModelContext _context = new();
            _context.User.Remove(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var s = _User.Find(x => x.ID_user == id);
            using FlowModelContext _context = new();
            _context.User.Remove(s);
            _context.SaveChanges();
        }

        public User Read(int id)
        {
            return _User.Find(x => x.ID_user == id);
        }

        public void Update(User item)
        {
            var s = _User.Find(x => x.ID_user == item.ID_user);
            s.Login = item.Login;
            s.Pass = item.Pass;
            s.Role = item.Role;

            using FlowModelContext _context = new();
            _context.User.Update(s);
            _context.SaveChanges();
        }
    }
}
