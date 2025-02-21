using Microsoft.AspNetCore.Html;

namespace MissionControlSystem.Utilities;

using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

public static class HtmlHelperExtensions
{
    public static IHtmlContent EnumDropDownListFor<TModel, TEnum>(
        this IHtmlHelper<TModel> htmlHelper,
        Expression<Func<TModel, TEnum>> expression,
        IDictionary<string, object> htmlAttributes = null) where TEnum : Enum
    {
        var enumType = typeof(TEnum);
        var enumNames = Enum.GetNames(enumType);
        var enumValues = Enum.GetValues(enumType).Cast<TEnum>();

        var items = enumNames.Select((name, index) => new SelectListItem
        {
            Text = name,
            Value = (enumValues.ElementAt(index)).ToString()
        });

        return htmlHelper.DropDownListFor(expression, items, htmlAttributes);
    }
}