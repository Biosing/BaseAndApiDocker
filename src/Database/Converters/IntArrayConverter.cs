using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Database.Converters
{
    public class IntArrayConverter : ValueConverter<ICollection<int>, string>
    {
        public IntArrayConverter()
            : base(
                v => string.Join(',', v),
                v => v
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray())
        {
        }
    }
}