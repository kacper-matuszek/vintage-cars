using System;
using VintageCars.Domain.Commands.Base;

namespace VintageCars.Domain.Catalog.Commands
{
    public class CreateCategoryAttributeCommand : AuthorizationCommandBase
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
