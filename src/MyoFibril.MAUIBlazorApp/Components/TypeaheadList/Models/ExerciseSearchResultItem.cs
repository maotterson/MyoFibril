using MyoFibril.Contracts.WebAPI.Models;

namespace MyoFibril.MAUIBlazorApp.Components.TypeaheadList.Models;

public class ExerciseSearchResultItem
{
    public ExerciseDto Exercise { get; set; } = default!;
    public Queue<ExerciseSearchMatchingSubstring> MatchingSubstrings { get; set; } = default!;
}
