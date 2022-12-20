using System.Runtime.Serialization;

namespace Services.Utils
{
    [Serializable]
    public class InputValidationException : InvalidOperationException
    {
        public InputValidationException()
        {
        }

        public InputValidationException(string message)
            : base(message)
        {
        }

        public InputValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected InputValidationException(
            SerializationInfo info, StreamingContext context)
        {
        }
    }
}
