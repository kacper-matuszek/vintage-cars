using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace VintageCars.Domain.Commands.Base
{
    public abstract class QueryBase<TResult> : IRequest<TResult>
        where TResult: class
    {
    }
}
