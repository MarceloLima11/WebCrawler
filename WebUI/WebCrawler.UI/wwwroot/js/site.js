const form = document.querySelector('#form');
const body = document.querySelector('body');

form.addEventListener("submit", () => {
    const div = document.createElement("div");
    div.classList.add("loading", "centralize");

    const loading_container = document.createElement("div");
    loading_container.classList.add("loading");
    div.appendChild(loading_container);


    document.body.appendChild(div);

    body.classList.add("overlay");
    loading_container.classList.add("loading_init");
});