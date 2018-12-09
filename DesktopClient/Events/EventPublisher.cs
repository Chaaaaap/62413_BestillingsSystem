using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.Events
{
    public class EventPublisher : IEventPublisher
    {
        private readonly ConcurrentDictionary<Type, object> _subjects
            = new ConcurrentDictionary<Type, object>();

        public IObservable<TEvent> GetEvent<TEvent>()
        {
            //var subject =
            //    (ISubject<TEvent>) _subjects.GetOrAdd(typeof(TEvent),
            //        t => new Subject<TEvent>());
            //return subject.AsObservable();
            return null;
        }

        public void Publish<TEvent>(TEvent e)
        {
            object subject;
            //if (_subjects.TryGetValue(typeof(TEvent), out subject))
            //{
            //    ((ISubject<TEvent>)subject)
            //        .OnNext(e);
            //}
        }
    }
}
