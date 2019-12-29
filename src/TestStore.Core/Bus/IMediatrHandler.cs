using System.Threading.Tasks;
using TestStore.Core.Messages;

namespace TestStore.Core.Bus
{
    /// Nao e' bem um BUS (BUS em memoria)
    public interface IMediatrHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
    }
}
