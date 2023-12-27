using Microsoft.Extensions.Localization;
using System;
using System.Linq.Expressions;

namespace LinguaNex.Extensions.Localization
{
    public static class StringLocalizerExtensions
    {
        public static LocalizedString GetString<TResource>(
            this IStringLocalizer stringLocalizer,
            Expression<Func<TResource, string>> propertyExpression)
            => stringLocalizer[(propertyExpression.Body as MemberExpression).Member.Name];
    }
}