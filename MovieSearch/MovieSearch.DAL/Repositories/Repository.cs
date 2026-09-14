using Microsoft.EntityFrameworkCore;
using MovieSearch.DAL.Models;
using MovieSearch.DAL.Repositories.Interfaces;

namespace MovieSearch.DAL.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly MovieContext Context;
    protected readonly DbSet<T> DbSet;

    public Repository(MovieContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await DbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        DbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        DbSet.Remove(entity);
    }

    public IQueryable<T> Query()
    {
        return DbSet.AsQueryable();
    }
}