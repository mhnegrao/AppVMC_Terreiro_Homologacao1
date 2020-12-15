using Microsoft.EntityFrameworkCore;
using TVMCPwa.Models;

namespace TVMCPwa.Data
{
    public class DatabaseContext : DbContext
    {
        //o construtor da classe
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Caixa> FluxoCaixa { get; set; }
        public DbSet<LancamentoCaixa> LancamentosCaixa { get; set; }
        public DbSet<Mensalidade> Mensalidades { get; set; }
        public DbSet<Afiliado> Afiliados { get; set; }
        public DbSet<User> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                var conn = "Server = localhost; port = 33069; User Id = root; Password = rootdocker!; Database = tvmc";

                builder.UseMySql(conn);
            }

            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            MappingEntity(builder);
        }

        private void MappingEntity(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //new UserMap(builder.Entity<User>());
            //new ProdutoMap(builder.Entity<Produto>());
            //new ClienteMap(builder.Entity<Cliente>());
            //new PedidoMap(builder.Entity<Pedido>());
            //new PedidoItemMap(builder.Entity<PedidoItem>());
            //new EnderecoMap(builder.Entity<Endereco>());
        }
    }
}
