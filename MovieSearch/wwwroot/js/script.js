const movieInput = document.getElementById("movieInput");
const searchButton = document.getElementById("searchButton");
const movieContainer = document.getElementById("movie");
const message = document.getElementById("message");

searchButton.addEventListener("click", searchMovie);

movieInput.addEventListener("keydown", function (event) {
    if (event.key === "Enter") {
        searchMovie();
    }
});

async function searchMovie() {

    const title = movieInput.value.trim();

    if (!title) {
        showMessage("Введите название");
        return;
    }

    movieContainer.innerHTML = "";
    showMessage("Поиск...");

    try {

        const response = await fetch(
            `/api/search?title=${encodeURIComponent(title)}`
        );

        const data = await response.json();

        if (!response.ok) {
            showMessage(data.error || "Фильм не найдено.");
            return;
        }

        message.innerHTML = "";

        displayMovie(data);

    } catch (error) {

        console.error(error);

        showMessage(
            "Не удалось найти информацию."
        );
    }
}

function displayMovie(movie) {

    const poster =
        movie.Poster && movie.Poster !== "N/A"
            ? movie.Poster
            : "https://via.placeholder.com/300x450?text=No+Poster";

    movieContainer.innerHTML = `
        <div class="movie-card">

            <div class="poster-container">
                <img
                    src="${escapeHtml(poster)}"
                    alt="${escapeHtml(movie.Title)}"
                >
            </div>

            <div class="movie-info">

                <h2>${escapeHtml(movie.Title)}</h2>

                <div class="rating">
                    Рейтинг ${escapeHtml(movie.imdbRating)}
                </div>

                <p>
                    <strong> Год:</strong>
                    ${escapeHtml(movie.Year)}
                </p>

                <p>
                    <strong>Длительность:</strong>
                    ${escapeHtml(movie.Runtime)}
                </p>

                <p>
                    <strong>Режисер:</strong>
                    ${escapeHtml(movie.Director)}
                </p>

                <p>
                    <strong>Актерский состав:</strong>
                    ${escapeHtml(movie.Actors)}
                </p>

                <p>
                    <strong> Описание:</strong>
                </p>

                <p class="plot">
                    ${escapeHtml(movie.Plot)}
                </p>

            </div>

        </div>
    `;
}

function showMessage(text) {
    message.innerHTML = `<p class="message">${escapeHtml(text)}</p>`;
}

function escapeHtml(text) {

    if (!text) {
        return "";
    }

    const div = document.createElement("div");

    div.textContent = text;

    return div.innerHTML;
}