﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function scrollToView(elem) {
    if (elem) {
        elem.scrollIntoView();
    }
}

function scrollToLastChild(el) {
    if (el) {
        el.lastElementChild.scrollIntoView();
        console.log("test");
    }
    
}