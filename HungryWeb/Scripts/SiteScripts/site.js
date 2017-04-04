//cuando el elemento colapsado es mostrado
$('#theSection').on('show.bs.collapse', function () {

    $('#theButton').text('Ocultar Sopas');

});

//cuando es colapsado de nuevo el contenido

$('#theSection').on('hide.bs.collapse', function () {

    $('#theButton').text('Mostrar Sopas');
});