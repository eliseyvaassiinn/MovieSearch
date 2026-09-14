using MovieSearch.DAL.Models;
using MovieSearch.DAL.Repositories;
using MovieSearch.DAL.Repositories.Interfaces;

namespace MovieSearch.DAL.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly MovieContext _context;

    public IRepository<Movie> Movies { get; }

    public UnitOfWork(MovieContext context)
    {
        _context = context;
        Movies = new Repository<Movie>(context);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}