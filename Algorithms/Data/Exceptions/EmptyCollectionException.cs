using System;
using System.Runtime.Serialization;

namespace Algorithms.Data.Exceptions
{
    [Serializable]
    public class EmptyCollectionException : DataException
    {
        public EmptyCollectionException()
        {
        }

        public EmptyCollectionException(string message)
            : base(message)
        {
        }

        public EmptyCollectionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected EmptyCollectionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}