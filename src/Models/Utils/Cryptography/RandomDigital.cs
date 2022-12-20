using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Utils.Cryptography
{
    public record RandomDigital : RandomStringBase
    {
        public RandomDigital(int length)
            : base(length)
        {
        }

        protected override string Chars => "0123456789";
    }
}
