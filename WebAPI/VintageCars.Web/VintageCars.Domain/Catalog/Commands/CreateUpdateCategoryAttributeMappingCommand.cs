using System;
using Newtonsoft.Json;
using Nop.Core.Domain.Catalog;
using VintageCars.Domain.Base;
using VintageCars.Domain.Commands.Base;

namespace VintageCars.Domain.Catalog.Commands
{
    public class CreateUpdateCategoryAttributeMappingCommand : CommandBase, IBusinessEntity
    {
        public Guid? Id { get; set; }
        public Guid CategoryAttributeId { get; set; }
        public Guid CategoryId { get; set; }
        [JsonConverter(typeof(AttributeControlType))]
        public AttributeControlType AttributeControlType { get; set; }
        public int DisplayOrder { get; set; }
    }
}
