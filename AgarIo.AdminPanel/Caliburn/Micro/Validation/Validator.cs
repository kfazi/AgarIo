// ReSharper disable once CheckNamespace
namespace Caliburn.Micro.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class Validator<T>
    {
        private readonly T _validatedObject;

        private readonly Dictionary<string, List<FluentValidationRule<T>>> _validationRules;

        private readonly Dictionary<string, string> _errors;

        internal Validator(object validatedObject)
        {
            _validatedObject = (T)validatedObject;
            _validationRules = new Dictionary<string, List<FluentValidationRule<T>>>();
            _errors = new Dictionary<string, string>();
        }

        public FluentValidationRule<T> AddValidationRule(Expression<Func<T, object>> expression)
        {
            var propertyName = expression.GetPropertyFullName();
            if (!_validationRules.ContainsKey(propertyName))
            {
                _validationRules.Add(propertyName, new List<FluentValidationRule<T>>());
            }

            var rule = new FluentValidationRule<T>();
            _validationRules[propertyName].Add(rule);

            return rule;
        }

        public void RemoveValidationRule(Expression<Func<T, object>> expression)
        {
            var propertyName = expression.GetPropertyFullName();
            if (_validationRules.ContainsKey(propertyName))
            {
                _validationRules.Remove(propertyName);
            }
        }

        public virtual string Error
        {
            get
            {
                Validate();
                return string.Join(Environment.NewLine, _errors.Select(x => x.Value).Distinct().ToArray());
            }
        }

        public virtual string this[string columnName]
        {
            get
            {
                return Validate(columnName);
            }
        }

        public virtual string Validate()
        {
            _errors.Clear();
            return Validate(GetType().GetProperties().Select(x => x.Name).Union(_validationRules.Keys));
        }

        private string Validate(string propertyName)
        {
            return Validate(new List<string> { propertyName });
        }

        private string Validate(IEnumerable<string> propertyNames)
        {
            var results = new List<string>();
            foreach (var propertyName in propertyNames.Where(propertyName => _validationRules.ContainsKey(propertyName)))
            {
                if (_errors.ContainsKey(propertyName))
                {
                    _errors.Remove(propertyName);
                }

                foreach (var result in _validationRules[propertyName].Select(validationRule => validationRule.Validate(_validatedObject)).Where(result => !string.IsNullOrEmpty(result)))
                {
                    results.Add(result);

                    if (_errors.ContainsKey(propertyName))
                    {
                        _errors[propertyName] = result;
                    }
                    else
                    {
                        _errors.Add(propertyName, result);
                    }
                }
            }

            return string.Join(Environment.NewLine, results.Distinct().ToArray());
        }
    }
}