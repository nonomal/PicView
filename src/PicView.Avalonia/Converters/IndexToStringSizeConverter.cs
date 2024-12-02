using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace PicView.Avalonia.Converters;

public class IndexToStringSizeConverter: IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (!int.TryParse(value?.ToString(), out var index) || !int.TryParse(parameter?.ToString(), out var parameterIndex))
        {
            return BindingOperations.DoNothing;
        }

        switch (index)
        {
            case 1:
                return index.ToString("/small", CultureInfo.InvariantCulture);
            
            case 2 when parameterIndex is 1:
                return index.ToString("/medium", CultureInfo.InvariantCulture);
            case 2:
                return index.ToString("/small", CultureInfo.InvariantCulture);
            
            case 3 when parameterIndex is 1:
                return index.ToString("/large", CultureInfo.InvariantCulture);
            case 3 when parameterIndex is 2:
                return index.ToString("/medium", CultureInfo.InvariantCulture);
            case 3:
                return index.ToString("/small", CultureInfo.InvariantCulture);
            
            case 4 when parameterIndex is 1:
                return index.ToString("/large", CultureInfo.InvariantCulture);
            case 4 when parameterIndex is 2:
                return index.ToString("/medium", CultureInfo.InvariantCulture);
            case 4 when parameterIndex is 3:
                return index.ToString("/small", CultureInfo.InvariantCulture);
            case 4:
                return index.ToString("/xs", CultureInfo.InvariantCulture);
            
            case 5 when parameterIndex is 1:
                return index.ToString("/xl", CultureInfo.InvariantCulture);
            case 5 when parameterIndex is 2:
                return index.ToString("/large", CultureInfo.InvariantCulture);
            case 5 when parameterIndex is 3:
                return index.ToString("/medium", CultureInfo.InvariantCulture);
            case 5when parameterIndex is 4:
                return index.ToString("/small", CultureInfo.InvariantCulture);
            case 5:
                return index.ToString("/xs", CultureInfo.InvariantCulture);
            
            
            case 6 when parameterIndex is 1:
                return index.ToString("/xl", CultureInfo.InvariantCulture);
            case 6 when parameterIndex is 2:
                return index.ToString("/large", CultureInfo.InvariantCulture);
            case 6 when parameterIndex is 3:
                return index.ToString("/medium", CultureInfo.InvariantCulture);
            case 6 when parameterIndex is 4:
                return index.ToString("/small", CultureInfo.InvariantCulture);
            case 6 when parameterIndex is 5:
                return index.ToString("/xs", CultureInfo.InvariantCulture);
            case 6:
                return index.ToString("/xxs", CultureInfo.InvariantCulture);
            
            case 7 when parameterIndex is 1:
                return index.ToString("/xxl", CultureInfo.InvariantCulture);
            case 7 when parameterIndex is 2:
                return index.ToString("/xl", CultureInfo.InvariantCulture);
            case 7 when parameterIndex is 3:
                return index.ToString("/large", CultureInfo.InvariantCulture);
            case 7 when parameterIndex is 4:
                return index.ToString("/medium", CultureInfo.InvariantCulture);
            case 7 when parameterIndex is 5:
                return index.ToString("/small", CultureInfo.InvariantCulture);
            case 7 when parameterIndex is 6:
                return index.ToString("/xs", CultureInfo.InvariantCulture);
            case 7:
                return index.ToString("/xxs", CultureInfo.InvariantCulture);
            default:
                return BindingOperations.DoNothing;
        }
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return BindingOperations.DoNothing;
    }
}
