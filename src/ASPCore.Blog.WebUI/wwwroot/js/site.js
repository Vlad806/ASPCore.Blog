// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function filteredByCategory(elem) {
    var id = elem.id;
    window.location = window.location.origin + "\\Article\\ArticlesList" + "?categoryId=" + id;
};

function filteredByTag(elem) {
    var id = elem.id;
    window.location = window.location.origin + "\\Article\\ArticlesList" + "?tagId=" + id;
};

function getArticle(art) {
    var id = art.id;
    window.location = window.location.origin + "\\Article\\ArticleDetails" + "?id=" + id;
};