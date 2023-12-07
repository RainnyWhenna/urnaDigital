using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrnaDigital.Models;
using UrnaDigital.DataContext;
using System.Linq;
using System.Threading;

namespace UrnaDigital.Service.Identificacao
{
    public class VotacaoService : IVotacaoInterface
    {

        private readonly ApplicationDbContext _context;

        public VotacaoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<VotacaoModel>>> CreateVoto(VotacaoModel novoVoto)
        {
            ServiceResponse<List<VotacaoModel>> serviceResponse = new ServiceResponse<List<VotacaoModel>>();

            _context.Add(novoVoto);
            await _context.SaveChangesAsync();

            serviceResponse.Dados = _context.Votacao.ToList();
            return serviceResponse;
        }

        public async Task ZerarVotacao()
        {
            DateTime targetTime = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,14,0,0);
            DayOfWeek targetDayOfWeek = DayOfWeek.Friday;

            TimeSpan initialDelay = CalculaDelay(targetTime,targetDayOfWeek);

            static TimeSpan CalculaDelay(DateTime targetTime,DayOfWeek targetDayOfWeek)
            {
                DateTime now = DateTime.Now;
                int daysUntilTarget = ((int)targetDayOfWeek - (int)now.DayOfWeek + 7) % 7;
                DateTime nextTarget = now.Date.AddDays(daysUntilTarget).Add(targetTime.TimeOfDay);

                if (nextTarget < now)
                {
                    nextTarget = nextTarget.AddDays(7);
                }

                return nextTarget - now;
            }
        }

        public async Task ExecutaEndpointAsync(object state)
        {
            foreach (var item in _context.Restaurantes.Where(r => r.IdRestaurante >= 0).ToList())
            {
                item.EscolhidoEm = null;
                _context.Restaurantes.Update(item);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<ServiceResponse<List<RestaurantesModel>>> GetRestaurantes()
        {
            ServiceResponse<List<RestaurantesModel>> serviceResponse = new ServiceResponse<List<RestaurantesModel>>();

            List<RestaurantesModel> lstRestaurantes = _context.Restaurantes.Where(x => x.EscolhidoEm == null).ToList();

            serviceResponse.Dados = lstRestaurantes;

            return serviceResponse;
        }

        public async Task<ServiceResponse<RestaurantesModel>> GetRestauranteEscolhido()
        {
            ServiceResponse<RestaurantesModel> serviceResponse = new ServiceResponse<RestaurantesModel>();

            DateTime dataAtual = DateTime.Now;
            DateTime dataInicial = dataAtual.AddDays(-(int)dataAtual.DayOfWeek + (int)DayOfWeek.Monday).Date;
            DateTime dataFinal = dataInicial.AddDays(4).Date.AddHours(13);

            var resultado = from v in _context.Votacao
                            join r in _context.Restaurantes on v.IdRestaurante equals r.IdRestaurante
                            where v.DtVotacao >= dataInicial && v.DtVotacao <= dataFinal
                            group new { v.IdRestaurante,r.NomeRestaurante } by new { v.IdRestaurante,r.NomeRestaurante } into g
                            orderby g.Count() descending
                            select new
                            {
                                IdRestaurante = g.Key.IdRestaurante,
                                NomeRestaurante = g.Key.NomeRestaurante,
                                Votos = g.Count()
                            };

            RestaurantesModel escolhido = new RestaurantesModel();

            escolhido.IdRestaurante = resultado.ToList().First().IdRestaurante;
            escolhido.NomeRestaurante = resultado.ToList().First().NomeRestaurante;
            escolhido.EscolhidoEm = DateTime.Now;

            _context.Update(escolhido);
            await _context.SaveChangesAsync();

            serviceResponse.Dados = escolhido;

            return serviceResponse;
        }
    }
}
