using System;
using VintageCars.Domain.Base;
using VintageCars.Domain.Commands.Base;

namespace VintageCars.Domain.Catalog.Commands
{
    public class DeleteCategoryAttributeValueCommand : CommandBase
    {
        public Guid Id { get; set; }
    }
}
