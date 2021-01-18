using System.ComponentModel.DataAnnotations;

namespace ECAuth.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El Nombre de Usuario es obligatorio")]
        [Display(Name = "Nombre de Usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "La Contraseña es obligatoria")]
        [Display(Name ="Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Recordar contraseña")]
        public bool RememberMe { get; set; }
    }
}
