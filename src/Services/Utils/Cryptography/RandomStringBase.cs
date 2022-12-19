using System.Security.Cryptography;

namespace Services.Utils.Cryptography
{
    public abstract record RandomStringBase
    {
        protected abstract string Chars { get; }

        private readonly int _length;
        private string _result;

        protected RandomStringBase(int length)
        {
            _length = length;
        }

        public string Get => _result ??= GetInternal();

        private string GetInternal()
        {
            var stringChars = new char[_length];

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = Chars[RandomNumberGenerator.GetInt32(Chars.Length)];
            }

            return new string(stringChars);
        }

        public static explicit operator string(RandomStringBase pass)
        {
            return pass?.Get;
        }
    }
}
