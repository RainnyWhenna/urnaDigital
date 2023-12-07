using UrnaDigital.Models;

namespace UrnaDigital.Service.Funcionario
{
    public interface IFuncionarioInterface
    {
        Task<ServiceResponse<FuncionarioModel>> GetFuncionarioByMatricula(int intMatricula);
    }
}
