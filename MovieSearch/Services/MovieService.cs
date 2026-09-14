using MovieSearch.Models;
using MovieSearch.Repositories.Interfaces;
using MovieSearch.Services.Interfaces;

namespace MovieSearch.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _repository;

    public MovieService(IMovieRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Movie>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Movie?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task CreateAsync(Movie movie)
    {
        await _repository.AddAsync(movie);
    }

    public async Task UpdateAsync(Movie movie)
    {
        var existingMovie = await _repository.GetByIdAsync(movie.Id);

        if (existingMovie == null)
            return;

        existingMovie.Title = movie.Title;
        existingMovie.Director = movie.Director;
        existingMovie.Genre = movie.Genre;
        existingMovie.Year = movie.Year;
        existingMovie.Poster = movie.Poster;
        existingMovie.Description = movie.Description;

        await _repository.UpdateAsync(existingMovie);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}