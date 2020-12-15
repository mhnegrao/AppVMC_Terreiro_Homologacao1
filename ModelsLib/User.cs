using System;

namespace AppVMC.Models
{
    //[DisplayForClass(Name = "Usuario")]
    public class User : ModelBase
    {
        public User()
        {
            DataInclusao = DateTime.Now;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string Email { get; set; }
        public int IdCategoria { get; set; }
    }
}