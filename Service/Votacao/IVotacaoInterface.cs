using UrnaDigital.Models;

namespace UrnaDigital.Service.Identificacao
{
    public interface IVotacaoInterface
    {
        Task<ServiceResponse<List<RestaurantesModel>>> GetRestaurantes();
        Task<ServiceResponse<RestaurantesModel>> GetRestauranteEscolhido();
        Task<ServiceResponse<List<VotacaoModel>>> CreateVoto(VotacaoModel novoVoto);
        Task ZerarVotacao();
    }
}