using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Utils.Cryptography
{
    public record RandomCapital : RandomStringBase
    {
        public RandomCapital(int length)
            : base(length)
        {
        }

        protected override string Chars => "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    }
}
