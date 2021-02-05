using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Threading;
using WCFCommon;
using WCFServiceContract;

namespace WCFService
{
    public class AddService : IAddService
    {
        #region IAddService Members
        public string SimpleMethod(string msg)
        {
            return string.Format("Response from WCF service: {0}", msg);
        }
        public IAsyncResult BeginAdd(int number1, int number2, AsyncCallback callback, object state)
        {
            AddAsyncResult asyncResult = new AddAsyncResult(number1, number2, callback, state);
            ThreadPool.QueueUserWorkItem(new WaitCallback(Callback), asyncResult);
            return asyncResult;
        }

        public int EndAdd(IAsyncResult ar)
        {
            int result = 0;
            if (ar != null)
            {
                using (AddAsyncResult asyncResult = ar as AddAsyncResult)
                {
                    if (asyncResult == null)
                        throw new ArgumentNullException("IAsyncResult parameter is null.");

                    asyncResult.AsyncWait.WaitOne();
                    result = asyncResult.Result;
                }
            }
            return result;
        }

        public IAsyncResult BeginAddDC(AddDataContract input, AsyncCallback callback, object state)
        {
            AddAsyncResult asyncResult = null;
            try
            {
                asyncResult = new AddAsyncResult(input, callback, state);

                //Queues a method for execution. The method executes when a thread pool thread becomes available.
                ThreadPool.QueueUserWorkItem(new WaitCallback(CallbackDC), asyncResult);
            }
            catch (Exception ex)
            {
                ErrorInfo err = new ErrorInfo(ex.Message, "BeginAddDC faills");
                throw new FaultException<ErrorInfo>(err, "reason goes here.");
            }

            return asyncResult;
        }

        public AddDataContract EndAddDC(IAsyncResult ar)
        {
            AddDataContract result = null;
            try
            {
                if (ar != null)
                {
                    using (AddAsyncResult asyncResult = ar as AddAsyncResult)
                    {
                        if (asyncResult == null)
                            throw new ArgumentNullException("IAsyncResult parameter is null.");

                        if (asyncResult.Exception != null)
                            throw asyncResult.Exception;

                        asyncResult.AsyncWait.WaitOne();
                        result = asyncResult.AddContract;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorInfo err = new ErrorInfo(ex.Message, "EndAddDC faills");
                throw new FaultException<ErrorInfo>(err, "reason goes here.");
            }
            return result;
        }

        #endregion

        private void Callback(object state)
        {
            AddAsyncResult asyncResult = state as AddAsyncResult;
            try
            {
                asyncResult.Result = InternalAdd(asyncResult.number1, asyncResult.number2);
            }
            finally
            {
                asyncResult.Complete();
            }
        }

        private void CallbackDC(object state)
        {
            AddAsyncResult asyncResult = null;
            try
            {
                asyncResult = state as AddAsyncResult;
                asyncResult.AddContract = InternalAdd(asyncResult.AddContract);
            }
            catch (Exception ex)
            {
                asyncResult.Exception = ex;
            }
            finally
            {
                asyncResult.Complete();
            }
        }

        private int InternalAdd(int number1, int number2)
        {
            Thread.Sleep(TimeSpan.FromSeconds(20)); //模拟处理复杂运算
            return number1 + number2;
        }

        private AddDataContract InternalAdd(AddDataContract input)
        {
            Thread.Sleep(TimeSpan.FromSeconds(20)); //模拟处理复杂运算
            input.Result = input.Nbr1 + input.Nbr2;
            return input;
        }
    }
}
