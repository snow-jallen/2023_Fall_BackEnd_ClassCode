using System.Runtime.Serialization;

namespace EfCoreDemo.Exceptions
{
    public class MissingNameException : Exception
    {
        public MissingNameException()
        {
        }

        public MissingNameException(string? message) : base(message)
        {
        }

        public MissingNameException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected MissingNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
