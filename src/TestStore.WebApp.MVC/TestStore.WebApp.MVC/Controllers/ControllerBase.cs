using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TestStore.Core.Communication.Mediator;
using TestStore.Core.Messages.CommonMessages.Notifications;

namespace TestStore.WebApp.MVC.Controllers
{
    public abstract class ControllerBase: Controller
    {
        //nao injeto a interface aqui porque nao preciso quero ter acesso somente ao metodo Handle, presente na interface IDomainNotificationHandler
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediatorHandler;

        protected Guid ClienteId = Guid.Parse("9a9c261e-ff75-494e-b971-3d4030d56adb");

        protected ControllerBase(INotificationHandler<DomainNotification> notifications,
                                 IMediatorHandler mediatorHandler)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediatorHandler = mediatorHandler;
        }
        protected bool OperacaoValida()
        {
            return !_notifications.TemNotificacao();
        }
        protected IEnumerable<string> ObterMensagensErro()
        {
            return _notifications.ObterNotificacoes().Select(c => c.Value).ToList();
        }

        protected void NotificarErro(string codigo, string mensagem)
        {
            _mediatorHandler.PublicarNotificacao(new DomainNotification(codigo, mensagem));
        }
    }
}
