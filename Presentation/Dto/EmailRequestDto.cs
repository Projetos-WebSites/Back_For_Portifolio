using System.ComponentModel.DataAnnotations;

namespace Back_For_Portifolio.Presentation.Dto
{
    public class EmailRequestDto
    {

        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        [MinLength(2, ErrorMessage = "O nome deve ter pelo menos 2 caracteres.")]
        [MaxLength(80, ErrorMessage = "O nome deve ter no máximo 80 caracteres.")]
        [RegularExpression(@"^[A-Za-zÀ-ú\s]+$", ErrorMessage = "O nome deve conter apenas letras e espaços.")]
        public string name { get; set; }

        [Required(ErrorMessage = "O campo e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        [MaxLength(120, ErrorMessage = "O e-mail deve ter no máximo 120 caracteres.")]
        public string email { get; set; }

        [Required(ErrorMessage = "O campo mensagem é obrigatório.")]
        [MinLength(5, ErrorMessage = "A mensagem deve ter pelo menos 5 caracteres.")]
        [MaxLength(2000, ErrorMessage = "A mensagem deve ter no máximo 2000 caracteres.")]
        [RegularExpression(@"^(?!.*<script).*", ErrorMessage = "Mensagem contém conteúdo inválido.")]
        public string message { get; set; }

    }
}
