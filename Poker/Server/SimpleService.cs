using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Server
{
    [ServiceContract]
    interface IContract
    {
        [OperationContract]
        bool Send(string msg, int address);
    }

    class SimpleService : IContract
    {
        public bool Send(string msg, int address) {
            Console.WriteLine("Player {0} doing {1}", address, msg);
            if (msg == "")
                return false;
            return true;
        }
    }
}
