using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading;

namespace WCFCommon
{
    public class ProxyFactory<T> : ClientBase<T> where T : class
    {
        public ProxyFactory(string endpoint)
            : base(endpoint)
        {
        }
        
        public T CreateProxy()
        {
            T interFce = base.CreateChannel();

            return interFce;
        }

        public void CloseProxy()
        {
            try
            {
                try
                {
                    this.Close();
                }
                catch { }
                finally
                {
                    this.Abort();
                }
            }
            catch
            {
            }
        }

        //public void InvokeAsync(BeginOperationDelegate beginOperationDelegate, object[] inValues, EndOperationDelegate endOperationDelegate, SendOrPostCallback operationCompletedCallback, object userState)
        //{ 
        //    base.InvokeAsync(beginOperationDelegate, inValues, endOperationDelegate, operationCompletedCallback,userState);
        //}

        // Summary:
        //     A delegate that is used by System.ServiceModel.ClientBase<TChannel>.InvokeAsync(System.ServiceModel.ClientBase.BeginOperationDelegate,System.Object[],System.ServiceModel.ClientBase.EndOperationDelegate,System.Threading.SendOrPostCallback,System.Object)
        //     for calling asynchronous operations on the client.
        //
        // Parameters:
        //   inValues:
        //     The input values to the asynchronous call.
        //
        //   asyncCallback:
        //     Reference to the method to be called when the corresponding asynchronous
        //     operation completes.
        //
        //   state:
        //     An System.Object that lets the client distinguish between different asynchronous
        //     calls. It is made available to the client in the arguments parameter of the
        //     event completion callback.
        //
        // Returns:
        //     The result of the asynchronous call.
        //public delegate IAsyncResult BeginOperationDelegate(object[] inValues, AsyncCallback asyncCallback, object state);

        // Summary:
        //     A delegate that is invoked by System.ServiceModel.ClientBase<TChannel>.InvokeAsync(System.ServiceModel.ClientBase.BeginOperationDelegate,System.Object[],System.ServiceModel.ClientBase.EndOperationDelegate,System.Threading.SendOrPostCallback,System.Object)
        //     on successful completion of the call made by System.ServiceModel.ClientBase<TChannel>.InvokeAsync(System.ServiceModel.ClientBase.BeginOperationDelegate,System.Object[],System.ServiceModel.ClientBase.EndOperationDelegate,System.Threading.SendOrPostCallback,System.Object)
        //     to System.ServiceModel.ClientBase<TChannel>.BeginOperationDelegate.
        //
        // Parameters:
        //   result:
        //     The result returned by the call made by System.ServiceModel.ClientBase<TChannel>.InvokeAsync(System.ServiceModel.ClientBase.BeginOperationDelegate,System.Object[],System.ServiceModel.ClientBase.EndOperationDelegate,System.Threading.SendOrPostCallback,System.Object)to
        //     System.ServiceModel.ClientBase<TChannel>.BeginOperationDelegate.
        //
        // Returns:
        //     An array of System.Object that contains the results of the call to the asynchronous
        //     method. The operation may have multiple return values, which are all returned
        //     in this object array.
        //public delegate object[] EndOperationDelegate(IAsyncResult result);
    }
}
