using MyoFibril.MAUIBlazorApp.Components.TypeaheadList.Models;

namespace MyoFibril.MAUIBlazorApp.Components.TypeaheadList.Utils;

public static class ExerciseSearchUtils
{
    public static Queue<ExerciseSearchMatchingSubstring> GetMatchingSubstrings(string parentString, string substring)
    {
        Queue<ExerciseSearchMatchingSubstring> matchingSubstrings = new Queue<ExerciseSearchMatchingSubstring>();
        int index = 0;
        int substringLength = substring.Length;

        if (substringLength == 0)
        {
            return matchingSubstrings;
        }

        while (index < parentString.Length)
        {
            index = parentString.IndexOf(substring, index, StringComparison.OrdinalIgnoreCase);
            if (index == -1)
                break;

            matchingSubstrings.Enqueue(new ExerciseSearchMatchingSubstring
            {
                StartingIndex = index,
                EndingIndex = index + substringLength
            });

            index += substringLength;
        }

        return matchingSubstrings;
    }
}
