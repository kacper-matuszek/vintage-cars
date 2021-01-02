using System;

namespace VintageCars.Domain.Base
{
    public abstract class BaseModelView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
