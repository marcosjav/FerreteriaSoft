using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerreteriaNorte.Classes.Clients
{

    [Serializable]
    public class ClientException : Exception
    {
        public ClientException() { }
        public ClientException(string message) : base(message) {
            base.HResult = 1;
        }
        public ClientException(string message, Exception inner) : base(message, inner) { }
        
        protected ClientException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
