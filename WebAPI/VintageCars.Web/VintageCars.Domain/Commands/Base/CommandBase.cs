using MediatR;

namespace VintageCars.Domain.Commands.Base
{
    public abstract class CommandBase<T> : IRequest<T> where T : class
    {
    }
}
