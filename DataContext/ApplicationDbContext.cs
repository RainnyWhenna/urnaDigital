using Microsoft.EntityFrameworkCore;
using UrnaDigital.Models;

namespace UrnaDigital.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<FuncionarioModel> Funcionarios { get; set; }
        
        public DbSet<RestaurantesModel> Restaurantes { get; set; }

        public DbSet<VotacaoModel> Votacao { get; set; }
}
}
