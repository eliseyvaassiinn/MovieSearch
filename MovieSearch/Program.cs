using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/search", async (string title, IHttpClientFactory httpClientFactory) =>
{
    if (string.IsNullOrWhiteSpace(title))
    {
        return Results.BadRequest(new
        {
            error = "Введите название фильма"
        });
    }

    const string apiKey = "ccdefeff";

    var encodedTitle = Uri.EscapeDataString(title);

    var url = $"https://www.omdbapi.com/?apikey={apiKey}&t={encodedTitle}&plot=full";

    try
    {
        var client = httpClientFactory.CreateClient();

        var response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return Results.Problem("Ошибка");
        }

        var json = await response.Content.ReadAsStringAsync();

        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;

        if (root.TryGetProperty("Response", out var responseProperty) &&
            responseProperty.GetString() == "False")
        {
            var error = root.TryGetProperty("Error", out var errorProperty)
                ? errorProperty.GetString()
                : "Фильм не найдено";

            return Results.NotFound(new
            {
                error
            });
        }

        return Results.Content(json, "application/json");
    }
    catch (Exception ex)
    {
        return Results.Problem($"Ошибка: {ex.Message}");
    }
});

app.Run();