using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Heise.Course.WasmFrontend.Client.Shared.GridComponent.Models;
using Heise.Workshop.Shared.UIAttributes;
using Microsoft.AspNetCore.Components;

namespace Heise.Course.WasmFrontend.Client.Shared.GridComponent
{
  public class GridViewModel<T> : ComponentBase, IDisposable
  {

    public GridViewModel()
    {
      var props = typeof(T).GetProperties();
      var no = props.Where(p => p.GetCustomAttributes(typeof(HiddenAttribute), true).Any());
      foreach (var prop in props)
      {
        Headers.Add(new GridHeader<T>(prop));
      }
    }

    public ICollection<GridHeader<T>> Headers { get; set; } = new Collection<GridHeader<T>>();

    public void Sort(SortDirection dir, string propertyName)
    {
      if (Data != null)
      {
        var parameter = Expression.Parameter(typeof(T));
        var property = Expression.Property(parameter, propertyName);
        var propAsObject = Expression.Convert(property, typeof(object));
        var lambda = Expression.Lambda<Func<T, object>>(propAsObject, parameter).Compile();
        Data = dir switch
        {
          SortDirection.Asc => Data.OrderBy(lambda),
          SortDirection.Desc => Data.OrderByDescending(lambda),
          _ => Data
        };
      }
    }

    [Parameter]
    public IEnumerable<T>? Data { get; set; }

    [Parameter]
    public RenderFragment<string>? StringTemplate { get; set; }

    [Parameter]
    public RenderFragment<bool>? BoolTemplate { get; set; }

    public RenderFragment Get(T model, string propName)
    {
      var prop = typeof(T).GetProperty(propName);
      var value = prop?.GetValue(model);
      var type = prop?.PropertyType.Name;
      switch (value) { 
        case var v when v is string sval && StringTemplate != null:
          return StringTemplate?.Invoke(sval)!;
        case var b when b is bool bval && StringTemplate != null:
           return BoolTemplate?.Invoke(bval)!;
      }
      if (value == null){
        return AnyValue("NULL");
      }
      return AnyValue(value.ToString()!);
    }

    private RenderFragment<string> AnyValue = value => builder => builder.AddContent(1, value);

    public PType Get<PType>(T model, string propName)
    {
      return (PType)Convert.ChangeType(typeof(T).GetProperty(propName)?.GetValue(model), typeof(PType))!;
    }


    public void Dispose()
    {
      //
    }
  }
}
