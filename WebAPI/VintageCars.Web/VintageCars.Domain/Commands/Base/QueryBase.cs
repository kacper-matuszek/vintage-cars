using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using VintageCars.Domain.Utils;

namespace VintageCars.Domain.Commands.Base
{
    public abstract class QueryBase<TResult> : IRequest<TResult>
        where TResult: class
    {
    }

    public abstract class QueryPagedBase<TResult> : QueryBase<TResult>
        where TResult : class
    {
        public PagedRequest Paged { get; set; }

        public QueryPagedBase()
        {
        }
        protected QueryPagedBase(PagedRequest paged)
        {
            Paged = paged;
        }
    }

    public abstract class AuthorizationQueryBase<TResult> : AuthorizationCommandBase<TResult>
        where TResult : class
    {

    }
}
