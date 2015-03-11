﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="bosh.cs" company="">
//   
// </copyright>
// <summary>
//   The namespace.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Xml.Linq;
using XMPP.Registries;

namespace XMPP.Tags.Bosh
{
    public class Namespace
    {
        public const string Name = "http://jabber.org/protocol/httpbind";
        public const string Xmpp = "urn:xmpp:xbosh";

        public static XName Body = XName.Get("body", Name);
    }

    [XmppTag(typeof(Namespace), typeof(Body))]
    public class Body : Tag
    {
        public Body() : base(Namespace.Body)
        {
        }

        public Body(XElement other) : base(other)
        {
        }

        public int? WaitAttr
        {
            get { return GetAttributeValueAsInt("wait"); }
            set { InnerElement.SetAttributeValue("wait", value); }
        }

        public int? InactivityAttr
        {
            get { return GetAttributeValueAsInt("inactivity"); }
            set { InnerElement.SetAttributeValue("inactivity", value); }
        }

        public int? PollingAttr
        {
            get { return GetAttributeValueAsInt("polling"); }
            set { InnerElement.SetAttributeValue("polling", value); }
        }

        public int? RequestsAttr
        {
            get { return GetAttributeValueAsInt("requests"); }
            set { InnerElement.SetAttributeValue("requests", value); }
        }

        public int? HoldAttr
        {
            get { return GetAttributeValueAsInt("hold"); }
            set { InnerElement.SetAttributeValue("hold", value); }
        }

        public string SidAttr
        {
            get { return (string) GetAttributeValue("sid"); }
            set { InnerElement.SetAttributeValue("sid", value); }
        }

        public long? RidAttr
        {
            get { return GetAttributeValueAsLong("rid"); }
            set { InnerElement.SetAttributeValue("rid", value); }
        }

        public string FromAttr
        {
            get { return (string)GetAttributeValue("from"); }
            set { InnerElement.SetAttributeValue("from", value); }
        }

        public string ToAttr
        {
            get { return (string)GetAttributeValue("to"); }
            set { InnerElement.SetAttributeValue("to", value); }
        }

        public string TypeAttr
        {
            get { return (string)GetAttributeValue("type"); }
            set { InnerElement.SetAttributeValue("type", value); }
        }

        public bool? RestartAttr
        {
            get { return GetAttributeValueAsBool(XName.Get("restart", Namespace.Xmpp)); }
            set { InnerElement.SetAttributeValue(XName.Get("restart", Namespace.Xmpp), value); }
        }

        public string VersionAttr
        {
            get { return (string)GetAttributeValue(XName.Get("version", Namespace.Xmpp)); }
            set { InnerElement.SetAttributeValue(XName.Get("version", Namespace.Xmpp), value); }
        }

        public string LangAttr
        {
            get { return (string)GetAttributeValue("lang"); }
            set { InnerElement.SetAttributeValue("lang", value); }
        }
    }
}