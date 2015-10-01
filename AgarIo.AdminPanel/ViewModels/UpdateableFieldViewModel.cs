namespace AgarIo.AdminPanel.ViewModels
{
    using Caliburn.Micro.Validation;

    public class UpdateableFieldViewModel<T> : ValidatingPropertyChangedBase<UpdateableFieldViewModel<T>>
    {
        private T _value;

        private T _originalValue;

        public UpdateableFieldViewModel(T initialValue)
        {
            _value = initialValue;
        }

        public T Value
        {
            get { return _value; }

            set
            {
                if (Equals(value, _value)) return;
                _value = value;
                NotifyOfPropertyChange(() => Value);
                NotifyOfPropertyChange(() => IsModified);
            }
        }

        public T OriginalValue
        {
            get { return _originalValue; }

            set
            {
                if (Equals(value, _originalValue)) return;

                if (Equals(_value, _originalValue))
                {
                    _value = value;
                    NotifyOfPropertyChange(() => Value);
                }

                _originalValue = value;
                NotifyOfPropertyChange(() => OriginalValue);
                NotifyOfPropertyChange(() => IsModified);
            }
        }

        public bool IsModified => !Equals(_value, _originalValue);

        public void Reset()
        {
            Value = OriginalValue;
        }
    }
}