using MyoFibril.Domain.Entities;
using MyoFibril.SharedData.Dto;
using MyoFibril.SharedData.Services;
using MyoFibril.SharedData.Utils;
using System.Text.Json;

namespace MyoFibril.SharedData.Loaders;
public class ExerciseDataLoader
{
    private readonly IFileSystemService _fileSystemService;
    public List<ExerciseEntity> Exercises = new List<ExerciseEntity>();
    public ExerciseDataLoader(IFileSystemService fileSystemService)
    {
        _fileSystemService = fileSystemService;
    }

    public async Task Load()
    {
        string path = "exercises.json";
        var jsonContent = await _fileSystemService.LoadFileAsStringAsync(path);
        var exerciseDtos = JsonSerializer.Deserialize<List<LoadExerciseDto>>(jsonContent) ?? throw new Exception();

        Exercises = exerciseDtos.Select(edto =>
        {
            var exercise = new ExerciseEntity { Name = edto.ExerciseName, MuscleGroups = MuscleGroupCategoryUtils.GetMuscleGroupsFromSlugs(edto.MuscleGroupSlugs) };
            return exercise;
        }).ToList();
    }
}