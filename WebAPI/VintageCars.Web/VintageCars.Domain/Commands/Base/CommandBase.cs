using System;
using MediatR;

namespace VintageCars.Domain.Commands.Base
{
    public abstract class CommandBase<T> : IRequest<T> where T : class
    {
    }

    public abstract class CommandBase : IRequest
    {
    }

    public abstract class AuthorizationCommandBase : CommandBase
    {
        public Guid UserId { get; set; }
    }

    public abstract class AuthorizationCommandBase<T> : IRequest<T> where T : class
    {
        public Guid UserId { get; set; }
    }
}
