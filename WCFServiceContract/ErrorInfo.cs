using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace WCFServiceContract
{
    [DataContract]
    public class ErrorInfo
    {
        public ErrorInfo(string error, string reason)
        {
            this.ErrorDetails = error;
            this.Reason = reason;
        }

        [DataMember]
        public string ErrorDetails { get; set; }

        [DataMember]
        public string Reason { get; set; }
    }
}
