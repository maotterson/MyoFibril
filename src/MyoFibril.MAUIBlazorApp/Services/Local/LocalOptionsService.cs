using MyoFibril.Domain.Enums;

namespace MyoFibril.MAUIBlazorApp.Services.Local;
public class LocalOptionsService : ILocalOptionsService
{
    public WeightInfoUnit SelectedUnit => WeightInfoUnit.Pounds; // todo: allow ability to change unit type
}