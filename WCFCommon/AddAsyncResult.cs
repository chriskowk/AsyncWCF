using System;
using System.Collections.Generic;
using System.Text;
using WCFServiceContract;

namespace WCFCommon
{
    public class AddAsyncResult : AsyncResult
    {
        public readonly int number1 = 0;
        public readonly int number2 = 0;

        private int result;
        public AddDataContract AddContract { get; set; }
        public Exception Exception { get; set; }
        public int Result
        {
            get { return result; }
            set { result = value; }
        }

        public AddAsyncResult(int num1, int num2, AsyncCallback callback, object state)
            : base(callback, state)
        {
            this.number1 = num1;
            this.number2 = num2;
        }

        public AddAsyncResult(AddDataContract input, AsyncCallback callback, object state)
            : base(callback, state)
        {
            this.AddContract = input;
        }
    }
}
