using System.Threading.Tasks;

namespace TVMCPwa.Services
{
    public interface IManutencaoService
    {
        Task GerarXls();

        Task GerarBackup();

        Task RestaurarBackup();

        Task LimparBaseDeDados();
    }
}