using System.Reflection;

namespace BuilderGenerator;

public static class BuilderGenerator
{
    public static string Generate(Type type, bool useSetPropertiesBuilder = false)
    {
        var builderName = $"{type.Name}Builder";
        var propName = $"_{char.ToLower(type.Name[0]) + type.Name.Substring(1)}";

        var output = $"public class {builderName}{Environment.NewLine}{{{Environment.NewLine}" +
                     $"\tprivate {type.Name} {propName};{Environment.NewLine}";

        if (useSetPropertiesBuilder)
            output +=
                $"\tprivate Dictionary<string, object> _setProperties;{Environment.NewLine}{Environment.NewLine}";

        output +=
            $"\tprivate {builderName}(){Environment.NewLine}" +
            $"\t{{{Environment.NewLine}" +
            $"\t\t{propName} = new {type.Name}();{Environment.NewLine}";

        if (useSetPropertiesBuilder)
            output +=
                $"\t\t_setProperties = new Dictionary<string, object>();{Environment.NewLine}";

        output +=
            $"\t}}{Environment.NewLine}{Environment.NewLine}" +
            $"\tpublic static {builderName} Create() => new {builderName}();{Environment.NewLine}{Environment.NewLine}" +
            $"\tpublic {type.Name} Build(){Environment.NewLine}" +
            $"\t{{{Environment.NewLine}" +
            $"\t\treturn {propName};{Environment.NewLine}" +
            $"\t}}{Environment.NewLine}{Environment.NewLine}";

        foreach (var property in type.GetProperties())
        {
            var functionName = $"With{char.ToUpper(property.Name[0]) + property.Name.Substring(1)}";
            var propertyName = char.ToLower(property.Name[0]) + property.Name.Substring(1);
            output +=
                $"\tpublic {builderName} {functionName}({property.GetPropertyType()} {propertyName}){Environment.NewLine}" +
                $"\t{{{Environment.NewLine}" +
                $"\t\t{propName}.{property.Name} = {propertyName};{Environment.NewLine}";

            if (useSetPropertiesBuilder)
                output +=
                    $"\t\t_setProperties[nameof({propName}.{property.Name})] = {propertyName};{Environment.NewLine}";

            output +=
                $"\t\treturn this;{Environment.NewLine}" +
                $"\t}}{Environment.NewLine}{Environment.NewLine}";
        }

        output += "}";
        return output;
    }

    public static string GetPropertyType(this PropertyInfo propertyInfo)
    {
        if (propertyInfo.PropertyType == typeof(string))
            return "string";

        if (propertyInfo.PropertyType == typeof(bool))
            return "bool";

        if (propertyInfo.PropertyType == typeof(bool?))
            return "bool?";

        if (propertyInfo.PropertyType == typeof(long))
            return "long";

        if (propertyInfo.PropertyType == typeof(int))
            return "int";

        if (propertyInfo.PropertyType == typeof(long?))
            return "long?";

        if (propertyInfo.PropertyType == typeof(int?))
            return "int?";

        if (propertyInfo.PropertyType == typeof(decimal))
            return "decimal";

        if (propertyInfo.PropertyType == typeof(decimal?))
            return "decimal?";

        if (propertyInfo.PropertyType == typeof(double))
            return "double";

        if (propertyInfo.PropertyType == typeof(double?))
            return "double?";


        if (propertyInfo.PropertyType == typeof(DateTime))
            return "DateTime";

        if (propertyInfo.PropertyType == typeof(DateTime?))
            return "DateTime?";

        return "object";
    }
}