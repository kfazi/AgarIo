// ReSharper disable once CheckNamespace
namespace Caliburn.Micro.Validation
{
    using System;
    using System.Linq.Expressions;

    using Caliburn.Micro;

    public class ValidatingPropertyChangedBase<T> : PropertyChangedBase, ISupportValidation<T>
    {
        private readonly Validator<T> _validator;

        public ValidatingPropertyChangedBase()
        {
            _validator = new Validator<T>(this);
        }

        public string Error => _validator.Error;

        void NotifyErrorChanged()
        {
            NotifyOfPropertyChange(() => Error);
        }

        public string Validate()
        {
            NotifyErrorChanged();

            return _validator.Validate();
        }

        public string this[string columnName]
        {
            get
            {
                NotifyErrorChanged();

                return _validator[columnName];
            }
        }

        public FluentValidationRule<T> AddValidationRule(Expression<Func<T, object>> expression)
        {
            return _validator.AddValidationRule(expression);
        }

        public void RemoveValidationRule(Expression<Func<T, object>> expression)
        {
            _validator.RemoveValidationRule(expression);
        }
    }
}