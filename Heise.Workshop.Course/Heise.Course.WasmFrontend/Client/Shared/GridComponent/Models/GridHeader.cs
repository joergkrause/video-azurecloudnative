using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Heise.Course.WasmFrontend.Client.ViewModels.UIAttributes;

namespace Heise.Course.WasmFrontend.Client.Shared.GridComponent.Models;
public class GridHeader<T>
{

    public GridHeader()
    {
    }

    public GridHeader(PropertyInfo propertyInfo)
    {
        Name = propertyInfo.GetCustomAttributes(typeof(DisplayAttribute), true).OfType<DisplayAttribute>().SingleOrDefault()?.Name ?? propertyInfo.Name;
        Description = propertyInfo.GetCustomAttributes(typeof(DisplayAttribute), true).OfType<DisplayAttribute>().SingleOrDefault()?.Description ?? "";
        IsSortable = propertyInfo.GetCustomAttributes(typeof(SortableAttribute), true).Any();
        PropertyName = propertyInfo.Name;
    }

    public string Name { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;

    public string PropertyName { get; set; } = String.Empty;

    public bool IsSortable { get; set; }

}
