var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    var columns = [
        { "data": "game.title", "width": "25%" },
        { "data": "rentedDate", "width": "25%", "className": "text-center" },
        { "data": "userPackage.expiredDate", "width": "25%", "className": "text-center" }
    ];

    if (subscriptionPackage !== "Basic") {
        columns.push({
            "data": "id",
            "render": function (data) {
                return `<div class="w-50 btn-group" role="group">
                            <a onClick="Delete('/user/userpackage/replace/${data}')" class="btn btn-secondary mx-2"> <i class="bi bi-arrow-clockwise"></i> Replace </a>
                        </div>`;
            },
            "width": "25%",
            "className": "text-center"
        });
    }

    dataTable = $('#rentedGamesData').DataTable({
        "ajax": { "url": "/User/UserPackage/GetAllRentedGames/GetAll" },
        "columns": columns
    });
}



function Delete(url) {
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
                type: 'DELETE',
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
