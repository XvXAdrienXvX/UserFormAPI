using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UserFormAPI.DataAccessLayer.Models;
using UserFormAPI.DataAccessLayer.XMLDb;

namespace UserFormAPI.DataAccessLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        private XDocument xmlData;
        private string xmlFile = "";
        public UserRepository()
        {
            xmlFile = XMLDbUtil.GetFile();
            xmlData = XDocument.Load(xmlFile);
        }

        public void Delete(int Id)
        {
            xmlData.Root.Elements("Item").Where(i => (int)i.Element("userId") == Id).Remove();
        }

        public IEnumerable<User> GetAll()
        {
            List<User> userList = new List<User>();
            foreach (XElement node in xmlData.Descendants("Item"))
            {
                userList.Add(new User()
                {
                    userId = Int32.Parse(node.Element("userId").Value),
                    FirstName = node.Element("FirstName").Value,
                    LastName = node.Element("LastName").Value,
                    Email = node.Element("Email").Value,
                    MobileNumber = Int32.Parse(node.Element("MobileNumber").Value)
                });
            }

            return userList;
        }

        public User GetUserById(int Id)
        {
            IEnumerable<User> userList = GetAll();
            return userList.Where(user => user.userId == Id).FirstOrDefault();
        }

        public void Insert(User entity)
        {
            xmlData.Root.Add
            (
                new XElement("Item", 
                    new XElement("userId", entity.userId), 
                    new XElement("FirstName", entity.FirstName),
                    new XElement("LastName", entity.LastName),
                    new XElement("Email", entity.Email),
                    new XElement("MobileNumber", entity.MobileNumber)
                )
            );
            
        }

        public void Save()
        {
            xmlData.Save(xmlFile);
        }

        public void Update(User entity, int Id)
        {
            XElement node = xmlData.Root.Elements("Item").Where(i => (int)i.Element("userId") == Id).FirstOrDefault();
            foreach (PropertyInfo props in entity.GetType().GetProperties())
            {
                if (object.Equals(props.GetValue(entity, null), 0))
                {
                    continue;
                }

                if (props.GetValue(entity, null) != null)
                {
                    node.SetElementValue(props.Name, props.GetValue(entity, null));
                }
            }
        }
    }
}
