var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    var columns = [
        { "data": "game.title", "width": "25%" },
        { "data": "rentedDate", "width": "20%", "className": "text-center" },
        { "data": "userPackage.expiredDate", "width": "20%", "className": "text-center" }
    ];


    columns.push({
        "data": "id",
        "render": function (data) {
            let buttons = `<div class="w-75 btn-group" role="group">
                        <a href="/user/gamerating/" class="btn btn-secondary mx-2"> <i class="bi bi-hand-index-thumb"></i> Rate It </a>`;
            if (subscriptionPackage !== "Basic") {
                buttons += `<a onClick="Replace('/user/userpackage/replace/${data}')" class="btn btn-secondary mx-2"> <i class="bi bi-arrow-clockwise"></i> Replace </a>`;
            }
            buttons += `</div>`;
            return buttons;
        },
        "width": "35%",
        "className": "text-center"
    });
    

    dataTable = $('#rentedGamesData').DataTable({
        "ajax": { "url": "/User/UserPackage/GetAllRentedGames/GetAll" },
        "columns": columns
    });
}



function Replace(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, replace it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'POST',
                success: function (data) {
                    dataTable.ajax.reload();
                    console.log(data);
                    if (data.success) {
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    });
}
