using System;
using VintageCars.Domain.Commands.Base;

namespace VintageCars.Domain.Catalog.Commands
{
    public class DeleteCategoryCommand : AuthorizationCommandBase
    {
        public Guid Id { get; set; }
    }
}
