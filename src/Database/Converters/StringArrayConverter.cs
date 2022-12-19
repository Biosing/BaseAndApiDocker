using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Database.Converters
{
    public class StringArrayConverter : ValueConverter<List<string>, string>
    {
        public StringArrayConverter()
            : base(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList())
        {
        }
    }
}