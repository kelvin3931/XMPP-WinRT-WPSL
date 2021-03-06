﻿// RunningState.cs
//
//Copyright © 2006 - 2012 Dieter Lunn
//Modified 2012 Paul Freund ( freund.paul@lvl3.org )
//
//This library is free software; you can redistribute it and/or modify it under
//the terms of the GNU Lesser General Public License as published by the Free
//Software Foundation; either version 3 of the License, or (at your option)
//any later version.
//
//This library is distributed in the hope that it will be useful, but WITHOUT
//ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
//FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
//
//You should have received a copy of the GNU Lesser General Public License along
//with this library; if not, write to the Free Software Foundation, Inc., 59
//Temple Place, Suite 330, Boston, MA 02111-1307 USA

using System.Linq;
using XMPP.common;
using XMPP.tags;
namespace XMPP.states
{
	public class RunningState : IState
	{
        public RunningState(Manager manager) : base(manager) 
        {
            Manager.Events.OnSend += OnSend;
            Manager.Events.Ready(this);
        }

        public override void Dispose()
        {
            Manager.Events.OnSend -= OnSend;
        }

        private void OnSend(object sender, TagEventArgs e)
        {
            Manager.Connection.Send(e.tag);
        }

        public override void Execute(Tag data = null)
        {
            if (data != null)
            {
                if (Manager.Settings.EnableReceipt && data is tags.jabber.client.message)
                {
                    //XEP-0184 enabled
                    var rec = data.Elements().FirstOrDefault(el => el.Name.LocalName == tags.xmpp.receipts.Namespace.received.LocalName);
                    if (rec != null)
                        Manager.Events.Receipt(this, data); //it's a receipt, not going to show as message
                    else
                    {
                        //it's not a recipt, gotta show it
                        Manager.Events.Receive(this, data);

                        //if there's receipt request, I'll send it in AckState
                        var req = data.Elements().FirstOrDefault(e => e.Name.LocalName == tags.xmpp.receipts.Namespace.request.LocalName);
                        if (req != null && req.Name.NamespaceName == tags.xmpp.receipts.Namespace.Name)
                        {
                            Manager.State = new AckState(Manager);
                            Manager.State.Execute(data);
                        }
                    }
                }
                else
                    Manager.Events.Receive(this, data); //XEP-0184 disabled
            }
        }
    }
}