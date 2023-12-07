using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrnaDigital.Models;
using UrnaDigital.Service.Identificacao;
using UrnaDigital.Service.Funcionario;
using Microsoft.EntityFrameworkCore;

namespace UrnaDigital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IVotacaoInterface _votacaoInterface;
                         IFuncionarioInterface _funcionarioInterface;

        public HomeController(IVotacaoInterface votacaoInterface,
                              IFuncionarioInterface funcionarioInterface)
        {
            _votacaoInterface = votacaoInterface;
            _funcionarioInterface = funcionarioInterface;
        }

        [HttpGet("GetFuncionarioByMatricula")]
        public async Task<ServiceResponse<FuncionarioModel>> GetFuncionarioByMatricula(int intMatricula)
        {
            ServiceResponse<FuncionarioModel> serviceResponse = await _funcionarioInterface.GetFuncionarioByMatricula(intMatricula);

            return serviceResponse;
        }

        [HttpGet("GetRestaurantes")]
        public async Task<ServiceResponse<List<RestaurantesModel>>> GetRestaurantes()
        {
            ServiceResponse<List<RestaurantesModel>> serviceResponse = await _votacaoInterface.GetRestaurantes();

            return serviceResponse;
        }

        [HttpPost("CreateVoto")]
        public async Task<ServiceResponse<List<VotacaoModel>>> CreateVoto(VotacaoModel novoVoto)
        {
            return (await _votacaoInterface.CreateVoto(novoVoto));
        }

        [HttpGet("GetRestauranteEscolhido")]
        public async Task<ServiceResponse<RestaurantesModel>> GetRestauranteEscolhido()
        {
            ServiceResponse<RestaurantesModel> serviceResponse = await _votacaoInterface.GetRestauranteEscolhido();

            return serviceResponse;
        }

        [HttpPost("ZerarVotacao")]
        public async void ZerarVotacao()
        {
            _votacaoInterface.ZerarVotacao();
        }
    }
}
