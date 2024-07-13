var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#gameData').DataTable({
        "ajax": { "url": "/Admin/Game/GetAll" },
        "columns": [
            { "data": "title", "width": "17%" },
            { "data": "platform", "width": "15%" },
            { "data": "genre.name", "width": "12%" },
            { "data": "releaseDate", "width": "10%", "className": "text-center" },
            { "data": "quantity", "width": "10%", "className": "text-center" },
            //{
            //    "data": "rating",
            //    "render": function (data) {
            //        let stars = '';
            //        for (let i = 0; i < Math.floor(data); i++) {
            //            stars += '<i class="bi bi-star-fill"></i>';
            //        }
            //        for (let i = Math.floor(data); i < 5; i++) {
            //            stars += '<i class="bi bi-star"></i>';
            //        }
            //        return `<span class='rating-stars'>${stars}</span>`;
            //    },
            //    "width": "10%",
            //    "className": "text-center"
            //},
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                                <a href="/admin/game/upsert?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit </a>
                                <a onClick=Delete('/admin/game/delete/${data}') class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete </a>
                            </div>`
                },
                "width": "26%",
                "className": "text-center"
            }
        ]
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
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
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
