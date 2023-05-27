using Microsoft.Extensions.DependencyInjection;
using MyoFibril.SharedData.Loaders;
using System.Runtime.CompilerServices;

namespace MyoFibril.SharedData;

public static class DependencyInjection
{
    public static void LoadData(this IServiceCollection services)
    {
        services.AddScoped<ExerciseDataLoader>();
    }
}
