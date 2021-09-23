using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace konatus.api.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage = "O campo`{0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo`{0} está em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(10, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 5)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmPassword { get; set; }

        public IEnumerable<ClaimViewModel> Claims { get; set; }
    }
}