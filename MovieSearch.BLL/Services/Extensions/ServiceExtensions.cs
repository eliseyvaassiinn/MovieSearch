using Microsoft.Extensions.DependencyInjection;
using MovieSearch.DAL.Repositories;
using MovieSearch.DAL.Repositories.Interfaces;
using MovieSearch.DAL.UnitOfWork;
using MovieSearch.BLL.Services.Interfaces;

namespace MovieSearch.BLL.Services.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddMovieServices(this IServiceCollection services)
    {
        services.AddScoped<IRepository<MovieSearch.DAL.Models.Movie>, Repository<MovieSearch.DAL.Models.Movie>>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IMovieService, MovieService>();

        return services;
    }
}