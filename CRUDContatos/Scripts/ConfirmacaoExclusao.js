var item_to_delete;
$("#btnDeletar").click(function(e) {
    item_to_delete = $(this).data('id');
    alert('');
});
$('#btnConfirmaDeletar').click(function () {
    alert('');
    window.location = "/Cliente/ExcluirCliente/" + item_to_delete;
});