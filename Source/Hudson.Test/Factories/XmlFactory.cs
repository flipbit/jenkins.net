using System;
using System.IO;
using Hudson.Domain;

namespace Hudson.Factories
{
    internal static class XmlFactory
    {
        public static XmlPage Load(string fileName)
        {
            var page = new XmlPage(new Uri("http://www.example.com/"), File.ReadAllText("../../xml/" + fileName));

            return page;
        }

        public static XmlPage LoadInvalidXml()
        {
            return new XmlPage(new Uri("http://www.example.com/"));
        }
    }
}
