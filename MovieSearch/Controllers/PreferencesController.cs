using Microsoft.AspNetCore.Mvc;

namespace MovieSearch.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PreferencesController : ControllerBase
{
    [HttpPost("visit")]
    public IActionResult RegisterVisit()
    {
        var count = HttpContext.Session.GetInt32("VisitCount") ?? 0;
        count++;

        HttpContext.Session.SetInt32("VisitCount", count);

        return Ok(new { visitCount = count });
    }

    [HttpGet("visit")]
    public IActionResult GetVisitCount()
    {
        var count = HttpContext.Session.GetInt32("VisitCount") ?? 0;

        return Ok(new { visitCount = count });
    }

    [HttpPost("theme/{theme}")]
    public IActionResult SetTheme(string theme)
    {
        if (theme != "light" && theme != "dark")
            return BadRequest();

        Response.Cookies.Append("movie_theme", theme, new CookieOptions
        {
            HttpOnly = true,
            IsEssential = true,
            Expires = DateTimeOffset.UtcNow.AddDays(30)
        });

        return Ok(new { theme });
    }

    [HttpGet("theme")]
    public IActionResult GetTheme()
    {
        Request.Cookies.TryGetValue("movie_theme", out var theme);

        return Ok(new { theme });
    }
}