using Back_For_Portifolio.Presentation.Dto;

namespace Back_For_Portifolio.Application.Interface
{
    public interface IEnviarEmailService
    {
        Task EnviarEmail(DadosEmailDto dadosEmailDto);
    }
}
