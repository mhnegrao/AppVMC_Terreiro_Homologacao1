namespace TVMCPwa.Models
{
    //[DisplayForClass(Name = "Usuario")]
    public class User : ModelBase
    {
        /// <summary>
        /// Constante usada para validação de nomes em api
        /// </summary>
        //public const string displayName = "usuario";

        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string Email { get; set; }
        public int IdCategoria { get; set; }

    }
}