using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.Entity;
using Newtonsoft.Json;

namespace VOTING_SYSTEM_SERVER
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class VotingSystem : IVotingSystem
    {
        public string GetData(int id)
        {
            using (voting_systemEntities db = new voting_systemEntities())
            {
                DbSet<User> users = db.Users;
                users.Add(new User());
                foreach(User user in users)
                {

                }
                return string.Format("You entered: {0}", id);
            }
        }
        
        public string AddUser(string user)
        {
            User js = JsonConvert.DeserializeObject<User>(user);
            return "";
        }

        //public CompositeType GetDataUsingDataContract(CompositeType composite)
        //{
        //    if (composite == null)
        //    {
        //        throw new ArgumentNullException("composite");
        //    }
        //    if (composite.BoolValue)
        //    {
        //        composite.StringValue += "Suffix";
        //    }
        //    return composite;
        //}
    }
}
