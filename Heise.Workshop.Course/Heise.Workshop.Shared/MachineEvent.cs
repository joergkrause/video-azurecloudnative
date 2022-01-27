using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Heise.Workshop.Shared.DataModels
{
  public class MachineEvent {

    [JsonProperty("id")]
    public Guid Id { get; set; } = Guid.Empty;

    [JsonProperty("sequence")]
    public int Sequence { get; set; } = 1;

    [JsonProperty("value")]
    public double Value { get; set; } = 0.0;

    [JsonProperty("device")]
    public string? Device { get; set; }

    [JsonProperty("kind")]
    [JsonConverter(typeof(StringEnumConverter))]
    public EventKind Kind { get; set; } = EventKind.Temperature;

    //[JsonConverter(typeof(UnixDateTimeConverter))]
    //[JsonProperty(PropertyName = "_ts")]
    //public virtual DateTime Timestamp { get; }

  }
}
