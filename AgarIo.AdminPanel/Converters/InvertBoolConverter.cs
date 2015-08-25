namespace AgarIo.AdminPanel.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    internal class InvertBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = (bool)value;
            return !boolValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = (bool)value;
            return !boolValue;
        }
    }
}