﻿using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace BookGen.Gui.XmlEntities
{
    public class CData : IXmlSerializable
    {
        private string _value;

        public CData() : this(string.Empty)
        {
        }

        public CData(string value)
        {
            _value = value;
        }

        public override string ToString()
        {
            return _value;
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            _value = reader.ReadElementString();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteCData(_value);
        }
    }
}
