using Microsoft.Extensions.DependencyInjection;
using MyoFibril.SharedData.Loaders;
using MyoFibril.SharedData.Services;
using System.Runtime.CompilerServices;

namespace MyoFibril.SharedData;

public static class DependencyInjection
{
    public static void AddSharedData(this IServiceCollection services)
    {
        services.AddScoped<ExerciseDataLoader>();
    }
}
