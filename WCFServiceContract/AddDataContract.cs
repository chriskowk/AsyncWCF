using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using WCFServiceContract;

namespace WCFServiceContract
{
    [DataContract]
    public class AddDataContract
    {
        [DataMember]
        public int Nbr1 { get; set; }

        [DataMember]
        public int Nbr2 { get; set; }

        [DataMember]
        public int Result { get; set; }
    }
}
