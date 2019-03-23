using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ServiceVoiting
{
    public class Service1 : IService1
    {
        public Auth Authorisation(string Login, string Password)
        {
            Auth auth = new Auth();
            if (FindByLoginUsers(Login, Password))
            {
                using (voiting_systemEntities db = new voiting_systemEntities())
                {
                    users use = db.users
                               .Where(s => s.login == Login)
                               .Where(p => p.password == Password)
                               .FirstOrDefault();
                    if (use != null)
                    {
                        auth.error = false;
                        auth.error_message = null;
                        auth.id_user = use.id_user;
                        return auth;
                    }
                    else
                    {
                        auth.error = true;
                        auth.error_message = "Неверное имя пользователя или пароль";
                        return auth;
                    }
                }

            }
            else
            {
                auth.error = true;
                auth.error_message = "Неверное имя пользователя или пароль";
                return auth;
            }
        }

        public bool FindByLoginUsers(string Login, string Password)
        {
            using (voiting_systemEntities db = new voiting_systemEntities())
            {
                users use = db.users
                           .Where(s => s.login == Login)
                           .Where(p => p.password == Password)
                           .FirstOrDefault();
                if (use != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public candidate add_candidate(candidate cand)
        {
            using (voiting_systemEntities db = new voiting_systemEntities())
            {
                var count = db.candidate
                .Where(s => s.id_candidate == cand.id_candidate)
                .Count();
                if (count == 0)
                {
                    db.candidate.Add(cand);
                    db.SaveChanges();
                }
                return cand;
            }
        }

        public bulletin add_bulletin(bulletin b)
        {
            using (voiting_systemEntities db = new voiting_systemEntities())
            {
                var count = db.bulletin
                .Where(s => s.user_id == b.user_id)
                .Where(s => s.candidate_voiting_id == b.candidate_voiting_id)
                .Count();
                if (count == 0)
                {
                    db.bulletin.Add(b);
                    db.SaveChanges();
                }
                return b;
            }
        }


        public voiting add_voiting(voiting v)
        {
            using (voiting_systemEntities db = new voiting_systemEntities())
            {
                var count = db.voiting
                .Where(s => s.name == v.name)
                .Count();
                if (count == 0)
                {
                    db.voiting.Add(v);
                    db.SaveChanges();
                }
                return v;
            }
        }


        public users add_user(users user)
        {
            using (voiting_systemEntities db = new voiting_systemEntities())
            {
                var count = db.users
                .Where(s => s.seria_number == user.seria_number)
                .Count();
                if (count == 0)
                {
                    db.users.Add(user);
                    db.SaveChanges();
                }
                return user;
            }
        }

        public List<voiting> select_voiting()
        {
            using (voiting_systemEntities db = new voiting_systemEntities())
            {
                List<voiting> v = db.voiting.ToList();
                return v;
            }
        }

        

        public List<candidate> select_candidate()
        {
            using (voiting_systemEntities db = new voiting_systemEntities())
            {
                List<candidate> c = db.candidate.ToList();
                return c;
            }
        }
        public class Auth
        {
            public bool error;
            public string error_message;
            public int id_user;
        }

    }
}
