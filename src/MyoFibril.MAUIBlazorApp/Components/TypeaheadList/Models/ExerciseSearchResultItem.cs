using MyoFibril.Contracts.WebAPI.Models;
using MyoFibril.Domain.Entities;

namespace MyoFibril.MAUIBlazorApp.Components.TypeaheadList.Models;

public class ExerciseSearchResultItem
{
    public ExerciseEntity Exercise { get; set; } = default!;
    public Queue<ExerciseSearchMatchingSubstring> MatchingSubstrings { get; set; } = default!;
}
