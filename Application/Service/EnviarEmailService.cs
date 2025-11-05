
using Back_For_Portifolio.Application.Interface;
using Back_For_Portifolio.Presentation.Dto;
using Microsoft.Extensions.Configuration;
using Resend;
using System;
using System.Threading.Tasks;

namespace Back_For_Portifolio.Application.Service
{
    public class EnviarEmailService : IEnviarEmailService
    {
        private readonly IResend _resend;
        private readonly IConfiguration _configuration;

        public EnviarEmailService(IConfiguration configuration)
        {
            _configuration = configuration;

            var apiKey = _configuration["EmailConfiguration:ApiKeyResend"];

            if (string.IsNullOrWhiteSpace(apiKey))
                throw new Exception("Chave de API da Resend não configurada!");

            _resend = ResendClient.Create(apiKey);
        }

        public async Task EnviarEmail(DadosEmailDto dadosEmailDto)
        {

            var mensagem = new EmailMessage()
            {
                From = _configuration["EmailConfiguration:EmailFrom"],
                To = _configuration["EmailConfiguration:EmailTo"],
                Subject = _configuration["EmailConfiguration:Assunto"],
                HtmlBody = dadosEmailDto.mensagemHtml
            };

            await _resend.EmailSendAsync(mensagem);
        }

    }
}

