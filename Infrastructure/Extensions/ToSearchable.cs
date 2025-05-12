using ApplicationCore.Enums;
using System.Globalization;
using System.Text;
namespace Infrastructure.Extensions
{
    public static class ToSearchableExtensions
    {

        public static string ToSearchable(this string value, SearchableType searchable)
        {
            if (string.IsNullOrEmpty(value))
                return value;
            return searchable switch
            {
                SearchableType.None => value,
                SearchableType.IgnoreCase => value.ToUpper().Trim(),
                SearchableType.IgnoreCaseAndDiacritics => value.ToUpper().Trim().ToIgnoreCaseAndDiacritics(),
                _ => value
            };
        }

        private static string ToIgnoreCaseAndDiacritics(this string value)
        {
            var normalizedString = value.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder(capacity: normalizedString.Length);

            for (int i = 0; i < normalizedString.Length; i++)
            {
                char c = normalizedString[i];
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder
                .ToString()
                .Normalize(NormalizationForm.FormC);
        }
    }
}
