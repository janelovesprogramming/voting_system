using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Net;

namespace VOTING_SYSTEM_SERVER
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class VotingSystem : IVotingSystem
    {   
        public string CreateVoting(string data)
        {
            using (voting_systemEntities db = new voting_systemEntities())
            {
                Voting voting = JsonConvert.DeserializeObject<Voting>(data);
                var Votings = db.Votings;

                Votings.Add(voting);
                try
                {
                    db.SaveChanges();
                    return null;
                }
                catch(DbEntityValidationException e)
                {
                    return e.Message;
                }
            }                
        }

        public string AddUser(string userData)
        {
            using (voting_systemEntities db = new voting_systemEntities())
            {
                var users = db.Users;
                User user = JsonConvert.DeserializeObject<User>(userData);
                
                try {
                    users.Add(user);
                    db.SaveChanges();

                    return null;
                }
                catch(DbEntityValidationException e)
                {
                    var q = db.GetValidationErrors();
                    return e.Message;
                }
            }
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">Содержит в себе данные типа { Candidate: int, Voting: int, User: int }</param>
        /// <returns></returns>
        public void SetVoiceForCandidate(string data)
        {
            using (voting_systemEntities db = new voting_systemEntities())
            {
                SetVoiceResponce jsonData = JsonConvert.DeserializeObject<SetVoiceResponce>(data);
                int candidate = jsonData.Candidate;
                int voting = jsonData.Voting;
                int user = jsonData.User;

                Candidate_Voting row = (Candidate_Voting)db.Candidate_Voting.Where((a) => a.Voting_ID == voting && a.Candidate_ID == candidate);

                //Скорее всего надо не найти, а создать
                Bulletin bulettin = (Bulletin)db.Bulletins.Where((a) => a.Candidate_Voting == row && a.Users_ID == user);

                bulettin.Vote = 1;

                db.SaveChanges();
            }
        }

        public string GetVoicesCountInVoting(string data)
        {
            //TODO: Возможно, будет метод который делает голосование открытым или закрытым
            using (voting_systemEntities db = new voting_systemEntities())
            {
                var Candidate_Voting = db.Candidate_Voting;
                CountVotingResponce parseData = JsonConvert.DeserializeObject<CountVotingResponce>(data);
                int idVoting = parseData.Voting;
                Bulletin Bulletin;
                int count = 0;

                var Candidate_VotingList = Candidate_Voting.Where((a) => a.Voting_ID == idVoting);

                foreach(Candidate_Voting candidate_voting in Candidate_VotingList)
                {
                    Bulletin = (Bulletin)db.Bulletins.Where((a) => a.Candidate_Voting == candidate_voting);
                    if (Bulletin.Vote != 0)
                    {
                        count++;
                    }
                }

                return "" + count;
            }
            
        }

        public string Login(string userData)
        {
            using (voting_systemEntities db = new voting_systemEntities())
            {
                string token = "";
                User user = JsonConvert.DeserializeObject<User>(userData);
                var findUser = db.Users.Where(
                    (a) => a.Name == user.Name && a.Password == user.Password).ToList().First();

                if(findUser != null)
                {
                    using (MD5 md5 = MD5.Create())
                    {
                        token = GetMd5Hash(md5, findUser.Name + findUser.Password + CreateRandomString(4));
                    }

                    findUser.Token = token;
                    db.SaveChanges();

                    return token;
                }

                return null;
            }

        }

        public string GetCandidateInfo(string id)
        {
            using (voting_systemEntities db = new voting_systemEntities())
            {
                Candidate candidate = db.Candidates.Find(id);
                string jsonCandidate = JsonConvert.SerializeObject(candidate);

                return jsonCandidate;

            }
        }

        public string GetCandidatesList()
        {
            using (voting_systemEntities db = new voting_systemEntities())
            {
                var Candidates = db.Candidates;

                string jsonCandidates = JsonConvert.SerializeObject(Candidates);

                return jsonCandidates;
            }
        }

        private static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        private static string CreateRandomString(int length)
        {
            var result = new char[length];
            var r = new Random();
            for (int i = 0; i < result.Length; i++)
            {
                do
                    result[i] = (char)r.Next(127);
                while (result[i] < '!');
            }
            return new string(result);
        }

        

        // { Name: "Иван", PassportSeries: 222, PassportNumber: 23124, WhoGives: "me", WhenGives: "01.01.2015", isAdmin: true, Password: 123456, Token: "" }

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
    
    
    public class Auth
    {
        public bool error;
        public string error_message;
        public int id_user;
    }

    public class SetVoiceResponce
    {
        public int Candidate { get; set; }
        public int Voting { get; set; }
        public int User { get; set; }
    }

    public class CountVotingResponce
    {
        public int Voting { get; set; }
        public int User { get; set; }
    }    
}
