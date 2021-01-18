using System.ComponentModel.DataAnnotations;

namespace ECAuth.Models.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Contraseña")]
        [StringLength(30, ErrorMessage = "La longitud de la {0} debe superior a {2} caracteres e inferior a {1} caracteres.", MinimumLength = 10)]
        [DataType(DataType.Password)]
        [RegularExpression("^(?!.*?(.)\\1\\1)([^0-9])((?=\\w*[a-z]))((?=\\w*[A-Z]))((?=\\w*\\d{2,}))[0-9A-Za-z].{10,30}$", ErrorMessage = "La contraseña no es segura, no cumple con todos los requerimientos." +
            "La contraseña debe tener entre 10 y 30 caracteres.  " +
            "Debe contener mayúsculas y minúsculas. " +
            "Debe contener al menos 2 números. " +
            "Puede contener los siguientes caracteres especiales(! % _ + - : ? = ) " +
            "No podrá repetirse ninguna de las 12 últimas contraseñas utilizadas anteriormente. " +
            "No podrá comenzar por número. " +
            "No permitirá caracteres repetidos consecutivos tres veces o más(ej: aaa, 111, PPP). ")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
