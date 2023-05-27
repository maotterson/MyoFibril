using MyoFibril.Domain.Enums;

namespace MyoFibril.MAUIBlazorApp.Services.Local;
public interface ILocalOptionsService
{
    WeightInfoUnit SelectedUnit { get; }
}