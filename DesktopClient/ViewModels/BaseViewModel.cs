using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.Composite.Presentation.Events;

namespace DesktopClient.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private BaseViewModel _parent;

        public BaseViewModel Parent
        {
            get => _parent;
            set
            {
                if (value != _parent)
                {
                    _parent = value;
                }
            }
        }

        protected BaseViewModel(BaseViewModel parent)
        {
            Parent = parent;
        }
        
        public void OnPropertyChanged(string property)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #region Events

        public virtual IEventAggregator EventAggregator { get; set; }

        public virtual void Subscribe<E, P>(Action<P> action) where E : CompositePresentationEvent<P>
        {
            EventAggregator.GetEvent<E>().Subscribe(action);
        }

        public virtual void PublishEvent<E, P>(P payload) where E : CompositePresentationEvent<P>
        {
            EventAggregator.GetEvent<E>().Publish(payload);
            if (Parent != null)
                Parent.PublishEvent<E, P>(payload);
        }

        public virtual void Subscribe<P>(Action<P> action)
        {
            EventAggregator.GetEvent<CompositePresentationEvent<P>>().Subscribe(action);
        }

        public virtual void PublishEvent<P>(P payload)
        {
            // Some constructor scenarios results in event publishing before event aggregator is ready - check for this.
            if (EventAggregator != null)
            {
                EventAggregator.GetEvent<CompositePresentationEvent<P>>().Publish(payload);
                if (Parent != null)
                    Parent.PublishEvent(payload);
            }
        }

        #endregion
    }
}
