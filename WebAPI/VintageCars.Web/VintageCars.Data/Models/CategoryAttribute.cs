using Nop.Core;

namespace VintageCars.Data.Models
{
    public class CategoryAttribute : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
