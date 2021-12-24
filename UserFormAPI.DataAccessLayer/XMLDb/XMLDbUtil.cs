using System;
using System.IO;
using System.Xml;

namespace UserFormAPI.DataAccessLayer.XMLDb
{
    public static class XMLDbUtil
    {
        private static string directoryPath = "";
        private static string xmlFile = "";
        private static string fileName = "UserDB.xml";

        public static string GetFile()
        {
            directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            xmlFile = directoryPath + @"\UserDB.xml";

            if ((File.Exists(xmlFile)))
            {
                return xmlFile;
            }
            else
            {
                XmlDocument doc = new XmlDocument();
                try
                {
                    doc.LoadXml("<User></User>");
                    using (XmlTextWriter writer = new XmlTextWriter(directoryPath + String.Format("\\{0}", fileName), null))
                    {
                        writer.Formatting = Formatting.Indented;
                        doc.Save(writer);
                    }
                
                    return xmlFile;
                }
                catch (Exception err)
                {
                    throw new Exception(String.Format("Cannot create xml file {0}", err));
                }
                    
            }
        }
    }
}
