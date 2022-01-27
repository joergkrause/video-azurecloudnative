using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Heise.Course.WasmFrontend.Client.Services.Store
{

  public delegate void OnValueChangedDelegate(object value);

  public class State : IState
  {

    private IDictionary<string, object> InnerState { get; } = new Dictionary<string, object>();


    public object this[string key]
    {
      get => InnerState[key];
      set
      {
        InnerState[key] = value;
        if (OnValueChanged != null)
        {
          OnValueChanged(value);
        }
      }
    }

    public event OnValueChangedDelegate OnValueChanged;

    public ICollection<string> Keys => InnerState.Keys;

    public ICollection<object> Values => InnerState.Values;

    public int Count => InnerState.Count;

    public bool IsReadOnly => false;

    public void Add(string key, object value)
    {
      InnerState.Add(key, value);
    }

    public void Add(KeyValuePair<string, object> item)
    {
      InnerState.Add(item.Key, item.Value);
    }

    public void Clear()
    {
    }

    public bool Contains(KeyValuePair<string, object> item)
    {
      return InnerState.Contains(item);
    }

    public bool ContainsKey(string key)
    {
      return InnerState.ContainsKey(key);
    }

    public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
    {
      //;
    }

    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
      return InnerState.GetEnumerator();
    }

    public bool Remove(string key)
    {
      return InnerState.Remove(key);
    }

    public bool Remove(KeyValuePair<string, object> item)
    {
      return InnerState.Remove(item);
    }

    public bool TryGetValue(string key, [MaybeNullWhen(false)] out object value)
    {
      return InnerState.TryGetValue(key, out value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return InnerState.GetEnumerator();
    }

    public T GetValue<T>(string key)
    {
      return (T)InnerState[key];
    }
  }
}
