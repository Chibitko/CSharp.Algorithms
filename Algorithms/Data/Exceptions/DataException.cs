using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Algorithms.Data.Exceptions
{
    [Serializable]
    public class DataException : ApplicationException
    {
        public DataException()
        {
        }

        public DataException(string message)
            : base(message)
        {
        }

        public DataException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected DataException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}