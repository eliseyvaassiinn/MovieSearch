const movieInput = document.getElementById("movieInput");
const searchButton = document.getElementById("searchButton");
const movieContainer = document.getElementById("movie");
const message = document.getElementById("message");

const translations = {
    uk: {
        title: "Кінопошук",
        subtitle: "Знайдіть інформацію про будь-який фільм",
        placeholder: "Введіть назву фільму...",
        search: "Пошук",
        enterTitle: "Введіть назву",
        searching: "Пошук...",
        notFound: "Фільм не знайдено.",
        error: "Не вдалося знайти інформацію.",
        rating: "Рейтинг",
        year: "Рік",
        runtime: "Тривалість",
        director: "Режисер",
        actors: "Акторський склад",
        plot: "Опис"
    },
    es: {
        title: "Búsqueda de películas",
        subtitle: "Encuentra información sobre cualquier película",
        placeholder: "Introduce el nombre de la película...",
        search: "Buscar",
        enterTitle: "Introduce el nombre",
        searching: "Buscando...",
        notFound: "Película no encontrada.",
        error: "No se pudo encontrar la información.",
        rating: "Puntuación",
        year: "Año",
        runtime: "Duración",
        director: "Director",
        actors: "Reparto",
        plot: "Descripción"
    },
    en: {
        title: "Movie Search",
        subtitle: "Find information about any movie",
        placeholder: "Enter movie title...",
        search: "Search",
        enterTitle: "Enter a title",
        searching: "Searching...",
        notFound: "Movie not found.",
        error: "Could not find information.",
        rating: "Rating",
        year: "Year",
        runtime: "Runtime",
        director: "Director",
        actors: "Cast",
        plot: "Plot"
    }
};

let currentLanguage = localStorage.getItem("language") || "uk";

searchButton.addEventListener("click", searchMovie);

movieInput.addEventListener("keydown", function (event) {
    if (event.key === "Enter") {
        searchMovie();
    }
});

document.querySelectorAll("[data-lang]").forEach(button => {
    button.addEventListener("click", function () {
        setLanguage(this.dataset.lang);
    });
});

function setLanguage(language) {
    currentLanguage = language;
    localStorage.setItem("language", language);

    document.documentElement.lang = language;

    document.querySelectorAll("[data-i18n]").forEach(element => {
        const key = element.dataset.i18n;
        element.textContent = translations[language][key];
    });

    document.querySelectorAll("[data-i18n-placeholder]").forEach(element => {
        const key = element.dataset.i18nPlaceholder;
        element.placeholder = translations[language][key];
    });

    document.title = translations[language].title;
}

async function searchMovie() {

    const title = movieInput.value.trim();

    if (!title) {
        showMessage(translations[currentLanguage].enterTitle);
        return;
    }

    movieContainer.innerHTML = "";
    showMessage(translations[currentLanguage].searching);

    try {

        const response = await fetch(
            `/api/search?title=${encodeURIComponent(title)}`
        );

        const data = await response.json();

        if (!response.ok) {
            showMessage(data.error || translations[currentLanguage].notFound);
            return;
        }

        message.innerHTML = "";

        displayMovie(data);

    } catch (error) {

        console.error(error);

        showMessage(translations[currentLanguage].error);
    }
}

function displayMovie(movie) {

    const t = translations[currentLanguage];

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
                    ${t.rating}: ${escapeHtml(movie.imdbRating)}
                </div>

                <p>
                    <strong>${t.year}:</strong>
                    ${escapeHtml(movie.Year)}
                </p>

                <p>
                    <strong>${t.runtime}:</strong>
                    ${escapeHtml(movie.Runtime)}
                </p>

                <p>
                    <strong>${t.director}:</strong>
                    ${escapeHtml(movie.Director)}
                </p>

                <p>
                    <strong>${t.actors}:</strong>
                    ${escapeHtml(movie.Actors)}
                </p>

                <p>
                    <strong>${t.plot}:</strong>
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

setLanguage(currentLanguage);