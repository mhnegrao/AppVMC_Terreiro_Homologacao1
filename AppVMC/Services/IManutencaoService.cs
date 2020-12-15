using System.Threading.Tasks;

namespace AppVMC.Services
{
    public interface IManutencaoService
    {
        Task GerarXls();

        Task GerarBackup();

        Task RestaurarBackup();

        Task LimparBaseDeDados();
    }
}