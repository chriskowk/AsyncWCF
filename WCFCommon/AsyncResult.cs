using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace WCFCommon
{
    /*
    ManualResetEvent allows threads to communicate with each other by signaling. 
    Typically, this communication concerns a task which one thread must complete before other threads can proceed.
    When a thread begins an activity that must complete before other threads proceed, 
    it calls Reset to put ManualResetEvent in the nonsignaled state. 
    This thread can be thought of as controlling the ManualResetEvent. 
    Threads that call WaitOne on the ManualResetEvent will block, awaiting the signal. 
    When the controlling thread completes the activity, it calls Set to signal that the waiting threads can proceed. 
    All waiting threads are released.
    Once it has been signaled, ManualResetEvent remains signaled until it is manually reset. That is, calls to WaitOne return immediately.
    You can control the initial state of a ManualResetEvent by passing a Boolean value to the constructor, 
    true if the initial state is signaled and false otherwise.
    */
    public class AsyncResult : IAsyncResult, IDisposable
    {
        AsyncCallback callback;
        object state;
        ManualResetEvent manualResentEvent;

        public AsyncResult(AsyncCallback callback, object state)
        {
            this.callback = callback;
            this.state = state;
            this.manualResentEvent = new ManualResetEvent(false);
        }

        object IAsyncResult.AsyncState
        {
            get { return state; }
        }

        public ManualResetEvent AsyncWait
        {
            get
            {
                return manualResentEvent;
            }
        }

        WaitHandle IAsyncResult.AsyncWaitHandle
        {
            get { return this.AsyncWait; }
        }

        bool IAsyncResult.CompletedSynchronously
        {
            get { return false; }
        }

        bool IAsyncResult.IsCompleted
        {
            get { return manualResentEvent.WaitOne(0, false); }
        }

        public void Complete()
        {
            manualResentEvent.Set();
            if (callback != null)
                callback(this);
        }

        public void Dispose()
        {
            manualResentEvent.Close();
            manualResentEvent = null;
            state = null;
            callback = null;
        }
    }
}
