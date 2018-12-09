using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.Events
{
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent e);
        IObservable<TEvent> GetEvent<TEvent>();
    }
}
