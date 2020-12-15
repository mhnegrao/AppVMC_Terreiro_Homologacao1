using System.Threading.Tasks;

namespace PwaTVMC.Server.Services
{
    public interface IManutencaoService
    {
        Task GerarXls();

        Task GerarBackup();

        Task RestaurarBackup();

        Task LimparBaseDeDados();
    }
}