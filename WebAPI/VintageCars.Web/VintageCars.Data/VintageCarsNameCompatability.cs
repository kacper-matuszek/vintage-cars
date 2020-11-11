using System;
using System.Collections.Generic;
using Nop.Data.Mapping;
using VintageCars.Data.Models;

namespace VintageCars.Data
{
    public partial class VintageCarsNameCompatability : INameCompatibility
    {
        public Dictionary<Type, string> TableNames => new Dictionary<Type, string>()
        {
            { typeof(CategoryAttributeMapping), "Category_CategoryAttribute_Mapping" }
        };
        public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string>();
    }
}
