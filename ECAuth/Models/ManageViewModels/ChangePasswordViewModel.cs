using System;
using System.ComponentModel.DataAnnotations;

namespace ECAuth.Models.ManageViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "La Contraseña actual es obligatoria")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña actual")]
        public string OldPassword { get; set; }
        //^(?!.*?(.)\\1\\1)([^0-9])((?=\\w*[a-z]))((?=\\w*[A-Z]))((?=\\w*\\d{2,}))[0-9A-Za-z].{10,30}$

        [Required(ErrorMessage = "La Nueva Contraseña es obligatoria")]
        [StringLength(30, ErrorMessage = "La {0} debe tener como mínimo {2}  y como máximo {1} caracteres.", MinimumLength = 10)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva Contraseña")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}$", ErrorMessage = "La contraseña no es segura, no cumple con todos los requerimientos."  +  
            "La contraseña debe tener al menos 8 caracteres.  " +
            "Debe contener mayúsculas y minúsculas. " +
            "Debe contener al menos 1 número. " +
            "Puede contener los siguientes caracteres especiales(! % _ + - : ? = ) " +
            "No podrá repetirse ninguna de las 12 últimas contraseñas utilizadas anteriormente. " +
            "No podrá comenzar por número. " +
            "No permitirá caracteres repetidos consecutivos tres veces o más(ej: aaa, 111, PPP). ")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Nueva Contraseña")]
        [Compare("NewPassword", ErrorMessage = "La nueva Contraseña y su confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }

        public string StatusMessage { get; set; }

        public string UserName { get; set; }

        public string ReturnUrl { get; set; }
    }
}
