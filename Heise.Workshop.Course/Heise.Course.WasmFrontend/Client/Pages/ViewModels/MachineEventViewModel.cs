using System.ComponentModel.DataAnnotations;
using Heise.Course.WasmFrontend.Client.ViewModels.UIAttributes;

namespace Heise.Course.WasmFrontend.Client.ViewModels;

public class MachineEventViewModel
{

    [Hidden]
    public string? Id { get; set; }

    [Display(Name = "Sequenz")]
    public int Sequence { get; set; }

    [Display(Name = "Wert")]
    public float Value { get; set; }

    [Display(Name = "Gerät")]
    public string? Device { get; set; }

    [Display(Name = "Art"), Sortable]
    public EventKind Kind { get; set; }

    [Display(Name = "Zeitstempel"), Sortable]
    public string? Timestamp { get; set; }

}
