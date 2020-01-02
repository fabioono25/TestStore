using System.Threading.Tasks;
using TestStore.Core.Messages;
using TestStore.Core.Messages.CommonMessages.Notifications;

namespace TestStore.Core.Communication.Mediator
{
    /// Nao e' bem um BUS (BUS em memoria)
    public interface IMediatorHandler
    {
        //itens abaixo do estoque
        Task PublicarEvento<T>(T evento) where T : Event;

        //envio de comando
        Task<bool> EnviarComando<T>(T comando) where T : Command;

        //tratar separadamente como uma notificacao
        Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification;
    }
}
