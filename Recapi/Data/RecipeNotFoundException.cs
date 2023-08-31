using System.Runtime.Serialization;

namespace Recapi.Data
{
    [Serializable]
    internal class RecipeNotFoundException : Exception
    {
        public RecipeNotFoundException()
        {
        }

        public RecipeNotFoundException(string? message) : base(message)
        {
        }

        public RecipeNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected RecipeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}