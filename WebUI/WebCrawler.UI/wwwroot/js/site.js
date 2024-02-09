const form = document.querySelector('#form');

form.addEventListener("submit", () => {
    const div = document.createElement("div");
    div.classList.add("overlay");
    
    //const loading_container = document.createElement("div");
    //loading_container.classList.add("loading");

    //const spin = document.createElement("div");
    //loading_container.appendChild(spin);
    //div.appendChild(loading_container);

    document.body.appendChild(div);
/*    document.body.classList.add("overlay");*/
});