using MovieSearch.DAL.Models;
using MovieSearch.DAL.Repositories.Interfaces;

namespace MovieSearch.DAL.UnitOfWork;

public interface IUnitOfWork
{
    IRepository<Movie> Movies { get; }

    Task<int> SaveChangesAsync();
}