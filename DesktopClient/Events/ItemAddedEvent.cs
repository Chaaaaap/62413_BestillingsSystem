using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using DesktopClient.Events.EventArgs;
using Microsoft.Practices.Composite.Presentation.Events;

namespace DesktopClient.Events
{
    public class ItemAddedEvent : CompositePresentationEvent<ItemAddedEventArgs>
    {
    }

    public class ItemAddedEventArgs
    {
        public Item ItemAdded { get; set; }
    }
}
