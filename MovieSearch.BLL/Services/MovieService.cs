using MovieSearch.BLL.Services.Interfaces;
using MovieSearch.DAL.Models;
using MovieSearch.DAL.UnitOfWork;

namespace MovieSearch.BLL.Services;

public class MovieService : IMovieService
{
    private readonly IUnitOfWork _unitOfWork;

    public MovieService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Movie>> GetAllAsync()
    {
        return await _unitOfWork.Movies.GetAllAsync();
    }

    public async Task<Movie?> GetByIdAsync(int id)
    {
        return await _unitOfWork.Movies.GetByIdAsync(id);
    }

    public async Task CreateAsync(Movie movie)
    {
        await _unitOfWork.Movies.AddAsync(movie);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateAsync(Movie movie)
    {
        var existingMovie = await _unitOfWork.Movies.GetByIdAsync(movie.Id);

        if (existingMovie == null)
            return;

        existingMovie.Title = movie.Title;
        existingMovie.Director = movie.Director;
        existingMovie.Genre = movie.Genre;
        existingMovie.Year = movie.Year;
        existingMovie.Poster = movie.Poster;
        existingMovie.Description = movie.Description;

        _unitOfWork.Movies.Update(existingMovie);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var movie = await _unitOfWork.Movies.GetByIdAsync(id);

        if (movie == null)
            return;

        _unitOfWork.Movies.Delete(movie);
        await _unitOfWork.SaveChangesAsync();
    }
}