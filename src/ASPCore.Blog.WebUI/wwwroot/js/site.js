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

function addArticle() {
    window.location = window.location.origin + "\\Article\\Add";
};

function updateArticle(btn) {
    var id = $(btn).parent().parent()[0].id;
    window.location = window.location.origin + "\\Article\\Update?id=" + id;
};

function deleteArticle(btn) {
    var id = $(btn).parent().parent()[0].id;
    window.location = window.location.origin + "\\Article\\Delete?id=" + id;
};

function addCategory() {
    window.location = window.location.origin + "\\Category\\Add";
};

function updateCategory(btn) {
    var id = $(btn).parent().parent()[0].id;
    window.location = window.location.origin + "\\Category\\Update?id=" + id;
};

function deleteCategory(btn) {
    var id = $(btn).parent().parent()[0].id;
    window.location = window.location.origin + "\\Category\\Delete?id=" + id;
};

function addTag() {
    window.location = window.location.origin + "\\Tag\\Add";
};

function updateTag(btn) {
    var id = $(btn).parent().parent()[0].id;
    window.location = window.location.origin + "\\Tag\\Update?id=" + id;
};

function deleteTag(btn) {
    var id = $(btn).parent().parent()[0].id;
    window.location = window.location.origin + "\\Tag\\Delete?id=" + id;
};