using Back_For_Portifolio.Application.Interface;
using Back_For_Portifolio.Presentation.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Back_For_Portifolio.Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnviarEmailController : ControllerBase
    {
        private readonly IEnviarEmailService enviarEmailService;
        private readonly ILogger<EnviarEmailController> logger;
        private readonly IConfiguration _configuration;

        public EnviarEmailController(IEnviarEmailService enviarEmailService, IConfiguration configuration,  ILogger<EnviarEmailController> logger)
        {
            this.enviarEmailService = enviarEmailService;
            this.logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> EnviarEmail(EmailRequestDto emailRequestDto)
        {

            if (string.IsNullOrWhiteSpace(emailRequestDto.email) || !emailRequestDto.email.Contains("@"))
                return BadRequest("E-mail inválido.");

            if (emailRequestDto.message.Length < 10)
                return BadRequest("Mensagem muito curta para ser válida.");

            try
            {
                var caminhoHtml = Path.Combine(Directory.GetCurrentDirectory(), "Presentation", "Template", "EmailHtmlTemplate.html");
                var html = await System.IO.File.ReadAllTextAsync(caminhoHtml);

                html = html.Replace("{{NOME}}", emailRequestDto.name)
                           .Replace("{{EMAIL}}", emailRequestDto.email)
                           .Replace("{{MESSAGE}}", emailRequestDto.message)
                           .Replace("{{DATA_ENVIO}}", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));

                var dadosEmail = new DadosEmailDto
                {
                    mensagemHtml = html
                };

                await enviarEmailService.EnviarEmail(dadosEmail);

                return Ok(new { Mensagem = "E-mail enviado com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro interno para enviar email", Error = ex.Message });
            }


        }
    }
}
