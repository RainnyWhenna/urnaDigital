using UrnaDigital.Models;
using UrnaDigital.DataContext;
using System.Collections.Generic;

namespace UrnaDigital.Service.Funcionario
{
    public class FuncionarioService : IFuncionarioInterface
    {
        private readonly ApplicationDbContext _context;

        public FuncionarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        private DateTime HorarioLimite { 
            get  {
                return new DateTime(
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    11, 
                    44,
                    59);
            }
        }

        public Task<ServiceResponse<FuncionarioModel>> GetFuncionarioByMatricula(int intMatricula)
        {
            try {
                ServiceResponse<FuncionarioModel> serviceResponse = new ServiceResponse<FuncionarioModel>();

                if (DateTime.Now > HorarioLimite) { 
                    serviceResponse.Mensagem = "Votação Encerrada.";
                    serviceResponse.Sucesso = false;
                }

                FuncionarioModel? myFuncionario = _context.Funcionarios.FirstOrDefault(f => f.Matricula == intMatricula);
                if (myFuncionario == null) { 
                    serviceResponse.Mensagem = "Funcionário Não Cadastrado";
                    serviceResponse.Sucesso = false;
                    return Task.FromResult(serviceResponse);
                }


                DateTime dtI = DateTime.Today;
                DateTime dtF = dtI.AddHours(23).AddMinutes(59).AddSeconds(59);
                List<VotacaoModel> lstVotacao = _context.Votacao.Where(v => v.IdFuncionario == myFuncionario.IdFuncionario && v.DtVotacao >= dtI && v.DtVotacao <= dtF ).ToList();

                if (lstVotacao.Count() > 0) { 
                    serviceResponse.Mensagem = "Esse funcionário já votou hoje!";
                    serviceResponse.Sucesso = false;
                }
                serviceResponse.Dados = myFuncionario;

                return Task.FromResult(serviceResponse);
            } catch {
                throw;
            }
        }
    }
}
