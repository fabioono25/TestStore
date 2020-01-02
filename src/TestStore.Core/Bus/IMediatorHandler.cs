using System.Threading.Tasks;
using TestStore.Core.Messages;

namespace TestStore.Core.Bus
{
    /// Nao e' bem um BUS (BUS em memoria)
    public interface IMediatorHandler
    {
        //itens abaixo do estoque
        Task PublicarEvento<T>(T evento) where T : Event;

        //envio de comando
        Task<bool> EnviarComando<T>(T comando) where T : Command;
    }
}
