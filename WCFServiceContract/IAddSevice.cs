using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace WCFServiceContract
{
    [ServiceContract()]
    public interface IAddService
    {
        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginAdd(int number1, int number2, AsyncCallback callback, object state);

        int EndAdd(IAsyncResult ar);

        [OperationContract(AsyncPattern = true)]
        [FaultContract(typeof(ErrorInfo))]
        IAsyncResult BeginAddDC(AddDataContract input, AsyncCallback callback, object state);

        //[FaultContract(typeof(ErrorInfo))]
        AddDataContract EndAddDC(IAsyncResult ar);

        [OperationContract]
        string SimpleMethod(string msg);
    }
}
