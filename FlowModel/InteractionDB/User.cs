using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FlowModel.InteractionDB
{
    internal class User
    {
        private FlowModelContext _FlowModelContext;

        public User()
        {

        }

        public User(string login, string pass, string role)
        {
            _FlowModelContext = new FlowModelContext();

            var value = _FlowModelContext.User.Select(x => x.ID_user).ToArray();
            Array.Sort(value);

            ID_user = value[value.Length - 1] + 1;
            Login = login;
            Pass = pass;
            Role = role;
        }

        public User(int id, string login, string pass, string role)
        {
            ID_user = id;
            Login = login;
            Pass = pass;
            Role = role;
        }

        [Key]
        /// <summary>
        /// ID пользователя
        /// </summary>
        public int ID_user { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Pass { get; set; }

        /// <summary>
        /// Роль - администратор/исследователь
        /// </summary>
        public string Role { get; set; }
    }
}
