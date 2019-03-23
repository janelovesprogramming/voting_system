using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ServiceVoiting
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        Service1.Auth Authorisation(string Login, string Password);

        [OperationContract]
        candidate add_candidate(candidate cand);

        [OperationContract]
        users add_user(users user);

        [OperationContract]
        voiting add_voiting(voiting v);

        [OperationContract]
        bulletin add_bulletin(bulletin b);

        [OperationContract]
        List<voiting> select_voiting();

        [OperationContract]
        List<candidate> select_candidate();
    }
}
