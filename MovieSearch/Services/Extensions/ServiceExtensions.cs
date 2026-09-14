using Microsoft.Extensions.DependencyInjection;
using MovieSearch.Repositories;
using MovieSearch.Repositories.Interfaces;
using MovieSearch.Services.Interfaces;

namespace MovieSearch.Services.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddMovieServices(this IServiceCollection services)
    {
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IMovieService, MovieService>();

        return services;
    }
}