﻿const form = document.querySelector('#form');
const loading = document.querySelector('.loading');

form.addEventListener("submit", () => {
    loading.classList.add("loading_init");
});