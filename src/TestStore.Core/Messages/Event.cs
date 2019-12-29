using MediatR;
using System;

namespace TestStore.Core.Messages
{
    /// <summary>
    /// INotification: interface de marcacao
    /// todo evento e' uma mensagem e uma notificacao
    /// </summary>
    public abstract class Event: Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
