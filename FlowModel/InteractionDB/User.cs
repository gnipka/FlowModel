using System.ComponentModel.DataAnnotations;

namespace FlowModel.InteractionDB
{
    internal class User
    {
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
