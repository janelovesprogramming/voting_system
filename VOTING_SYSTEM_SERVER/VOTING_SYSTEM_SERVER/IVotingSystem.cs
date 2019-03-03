using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace VOTING_SYSTEM_SERVER
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract]
    public interface IVotingSystem
    {
        //[WebGet(UriTemplate = "/{value}")]
        //[OperationContract]
        //string GetData(int value);

        [WebGet(UriTemplate = "/candidate/{id}")]
        [OperationContract]
        string GetCandidateInfo(string id);

        [WebGet(UriTemplate = "/candidates")]
        [OperationContract]
        string GetCandidatesList();

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        string AddUser(string user);

        [WebGet(UriTemplate = "/")]
        [OperationContract]
        string Login(string data);

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        void SetVoiceForCandidate(string data);

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        string CreateVoting(string data);

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        string GetVoicesCountInVoting(string data);

        //[OperationContract]
        //CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Добавьте здесь операции служб
    }


    // Используйте контракт данных, как показано в примере ниже, чтобы добавить составные типы к операциям служб.
    //[DataContract]
    //public class CompositeType
    //{
    //    bool boolValue = true;
    //    string stringValue = "Hello ";

    //    [DataMember]
    //    public bool BoolValue
    //    {
    //        get { return boolValue; }
    //        set { boolValue = value; }
    //    }

    //    [DataMember]
    //    public string StringValue
    //    {
    //        get { return stringValue; }
    //        set { stringValue = value; }
    //    }
    //}
}
