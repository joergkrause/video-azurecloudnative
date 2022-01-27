using System;
using System.Collections.Generic;
using System.Text;

namespace Heise.Workshop.Shared
{
  public class StorageContainer<T> where T : class
  {
    public T? Data { get; set; }

    public string Subject { get; set; } = String.Empty;

    public DateTime EventTime { get; set; } = DateTime.Now;

    public string EventType { get; set; } = "Simulator";

  }
}
