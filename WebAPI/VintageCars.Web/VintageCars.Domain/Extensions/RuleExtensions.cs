using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace VintageCars.Domain.Extensions
{
    public static class RuleExtensions
    {
        public static IRuleBuilderOptions<T, Property> SetCurrentValidation<T, Property>(
            this IRuleBuilderOptions<T, Property> option, bool condition, string message)
        {
            return option.When(_ => condition, ApplyConditionTo.CurrentValidator).WithMessage(message);
        }
    }
}
