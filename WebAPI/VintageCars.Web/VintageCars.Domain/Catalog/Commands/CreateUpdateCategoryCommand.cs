using System;
using VintageCars.Domain.Base;
using VintageCars.Domain.Commands.Base;

namespace VintageCars.Domain.Catalog.Commands
{
    public class CreateUpdateCategoryCommand : CommandBase, IBusinessEntity
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
