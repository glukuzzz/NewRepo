using System.Configuration;
using System.Xml;

namespace RSDN
{
    public class RusCurrencySectionHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            foreach (XmlNode curr in section.ChildNodes)
            {
                if (curr.Name == "currency")
                {
                    XmlNode senior = curr["senior"];
                    XmlNode junior = curr["junior"];
                    RusCurrency.Register(
                        curr.Attributes["code"].InnerText,
                        (curr.Attributes["male"].InnerText == "1"),
                        senior.Attributes["one"].InnerText,
                        senior.Attributes["two"].InnerText,
                        senior.Attributes["five"].InnerText,
                        junior.Attributes["one"].InnerText,
                        junior.Attributes["two"].InnerText,
                        junior.Attributes["five"].InnerText);
                }
            }
            return null;
        }
    };
};