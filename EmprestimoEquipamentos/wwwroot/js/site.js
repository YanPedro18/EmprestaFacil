


$(document).ready(function () {

    $('#tabEmprestimos').DataTable({
        language: {
            "decimal": "",
            "emptyTable": "No data available in table",
            "info": "Mostrando _START_ registro de _END_ em um total de _TOTAL_ entradas",
            "infoEmpty": "Showing 0 to 0 of 0 entries",
            "infoFiltered": "(filtered from _MAX_ total entries)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ entradas",
            "loadingRecords": "Loading...",
            "processing": "",
            "search": "Procurar:",
            "zeroRecords": "No matching records found",
            "paginate": {
                "first": "Primeiro",
                "last": "Último",
                "next": "Próximo",
                "previous": "Anterior"
            },
            "aria": {
                "sortAscending": ": activate to sort column ascending",
                "sortDescending": ": activate to sort column descending"
            }
        }


    });

    const modal = document.getElementById('modalAlert');

    // Define a opacidade para 0 ao longo de 3 segundos
    modal.style.transition = 'opacity 5s ease-out';
    modal.style.opacity = 0;

    // Aguarda 3 segundos antes de remover a div
    setTimeout(function () {
        modal.remove();
    }, 4000);


    //setTimeout( function () {
    //    $(".alert").fadeOut("slow", function () {
    //        $(this).alert('close');
    //    });
    //}, 5000);

} );