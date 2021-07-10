// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function Incluir() {
    $("#Accounts").append($("#Avaliable option:selected"));
}

function displayResult() {
    //Essa linha de código vai selecionar todos os itens que existem
    //no select #Accounts para garantir a sua persistência à Controller
    $("#Accounts option").prop('selected', true);


    console.log($("#Accounts").val());
}