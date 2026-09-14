using Microsoft.AspNetCore.Mvc;
using MovieSearch.BLL.Services.Interfaces;
using MovieSearch.DAL.Models;

namespace MovieSearch.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movie>>> GetAll()
    {
        var movies = await _movieService.GetAllAsync();

        return Ok(movies);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> GetById(int id)
    {
        var movie = await _movieService.GetByIdAsync(id);

        if (movie == null)
            return NotFound();

        return Ok(movie);
    }

    [HttpPost]
    public async Task<ActionResult<Movie>> Create(Movie movie)
    {
        await _movieService.CreateAsync(movie);

        return CreatedAtAction(
            nameof(GetById),
            new { id = movie.Id },
            movie);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Movie movie)
    {
        if (id != movie.Id)
            return BadRequest();

        var existingMovie = await _movieService.GetByIdAsync(id);

        if (existingMovie == null)
            return NotFound();

        await _movieService.UpdateAsync(movie);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var movie = await _movieService.GetByIdAsync(id);

        if (movie == null)
            return NotFound();

        await _movieService.DeleteAsync(id);

        return NoContent();
    }
}