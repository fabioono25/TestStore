using System.Threading.Tasks;

namespace TestStore.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
