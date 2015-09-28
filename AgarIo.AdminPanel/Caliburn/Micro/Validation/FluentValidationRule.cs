// ReSharper disable once CheckNamespace
namespace Caliburn.Micro.Validation
{
    using System;
    using System.Linq.Expressions;

    public class FluentValidationRule<T>
    {
        private Expression<Func<T, bool>> _condition;

        private string _message;

        public FluentValidationRule<T> Condition(Expression<Func<T, bool>> condition)
        {
            _condition = condition;
            return this;
        }

        public FluentValidationRule<T> Message(string message)
        {
            _message = message;
            return this;
        }

        public string Validate(T validatedObject)
        {
            return _condition == null ? string.Empty : (_condition.Compile()(validatedObject) ? string.Empty : _message);
        }
    }
}