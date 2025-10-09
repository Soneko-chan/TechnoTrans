using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UI.Converters
{
    public class NameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not string name || string.IsNullOrWhiteSpace(name))
                return string.Empty;

            
            if (IsFormatted(name))
                return name;

            var parts = name.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0) return string.Empty;

            string lastName = parts[0];
            string initials = "";

            if (parts.Length > 1)
                initials += $" {GetInitial(parts[1])}.";
            if (parts.Length > 2)
                initials += $"{GetInitial(parts[2])}.";

            return $"{lastName}{initials}";
        }

        private bool IsFormatted(string name)
        {
            
            return name.Contains(".") &&
                   (name.Contains(" ") || name.Length - name.Replace(".", "").Length >= 2);
        }

        private string GetInitial(string word)
        {
            
            word = word.Trim().Replace(".", "");
            return word.Length > 0 ? word[0].ToString() : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString() ?? string.Empty;
        }
    }
}