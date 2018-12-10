using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;

namespace DesktopClient.Events.EventArgs
{
    public class ItemAddedEventArgs : System.EventArgs
    {
        public Item ItemAdded { get; set; }
    }
}
