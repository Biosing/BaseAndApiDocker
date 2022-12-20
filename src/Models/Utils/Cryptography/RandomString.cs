using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Utils.Cryptography
{
    public record RandomString : RandomStringBase
    {
        public RandomString(string chars, int length)
            : base(length)
        {
            Chars = chars;
        }

        protected override string Chars { get; }
    }
}
