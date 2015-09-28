// ReSharper disable once CheckNamespace
namespace Caliburn.Micro.Validation
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;

    public interface ISupportValidation<T> : IDataErrorInfo
    {
        FluentValidationRule<T> AddValidationRule(Expression<Func<T, object>> expression);

        void RemoveValidationRule(Expression<Func<T, object>> expression);

        string Validate();
    }
}