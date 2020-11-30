using System;
using VintageCars.Domain.Commands.Base;

namespace VintageCars.Domain.Catalog.Commands
{
    public class DeleteCategoryAttributeCommand : CommandBase
    {
        public Guid Id { get; set; }
    }
}
