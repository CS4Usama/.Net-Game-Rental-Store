var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#packagesData').DataTable({
        "ajax": { "url": "/Admin/SubscriptionPackage/GetAll" },
        "columns": [
            { "data": "packageName", "width": "18%" },
            { "data": "gamesPerMonth", "width": "18%", "className": "text-center" },
            { "data": "rentNewReleasedGame", "width": "19%", "className": "text-center" },
            { "data": "maxReplacePerMonth", "width": "19%", "className": "text-center" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                                <a href="/admin/subscriptionpackage/upsert?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit </a>
                                <a onClick=Delete('/admin/subscriptionpackage/delete/${data}') class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete </a>
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
                    toaster.success(data.message);
                }
            })
        }
    });
}
