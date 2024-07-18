var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#ratingApproval').DataTable({
        "ajax": { "url": "/Admin/RatingApproval/GetAll" },
        "columns": [
            { "data": "userName", "width": "15%" },
            { "data": "userEmail", "width": "15%" },
            { "data": "gameName", "width": "15%" },
            { "data": "review", "width": "23%" },
            {
                "data": "rating",
                "render": function (data) {
                    let stars = '';
                    for (let i = 0; i < Math.floor(data); i++) {
                        stars += '<i class="bi bi-star-fill"></i>';
                    }
                    for (let i = Math.floor(data); i < 5; i++) {
                        stars += '<i class="bi bi-star"></i>';
                    }
                    return `<span class='rating-stars'>${stars}</span>`;
                },
                "width": "10%",
                "className": "text-center"
            },
            {
                "data": { id: "id", status: "status" },
                "render": function (data) {
                    if (data.status == "Approved") {
                        return `<div class="text-center">
                                <a onclick=Reject('${data.id}') class="btn btn-danger text-white" style="cursor:pointer;">
                                    <i class="bi bi-x-circle"></i>&nbsp; Reject
                                </a>
                            </div>`
                    } else if (data.status == "Rejected") {
                        return `<div class="text-center">
                                <a onclick=Approve('${data.id}') class="btn btn-success text-white" style="cursor:pointer;">
                                    <i class="bi bi-check2-circle"></i>&nbsp; Approve
                                </a>
                            </div>`
                    } else {
                        return `<div class="text-center">
                                <a onclick=Approve('${data.id}') class="btn btn-success text-white" style="cursor:pointer;">
                                    <i class="bi bi-check2-circle"></i>&nbsp; Approve
                                </a>
                                <a onclick=Reject('${data.id}') class="btn btn-danger text-white" style="cursor:pointer;">
                                    <i class="bi bi-x-circle"></i>&nbsp; Reject
                                </a>
                            </div>`
                    }
                },
                "width": "22%", "className": "text-center"
            }
        ],
        "order": [[1, "asc"]]
    });
}


function Approve(id) {
    $.ajax({
        url: `/Admin/RatingApproval/Approve?id=${id}`,
        success: function (data) {
            if(data.success) {
                toastr.success(data.message);
                dataTable.ajax.reload();
            } else {
                toastr.error(data.message);
                dataTable.ajax.reload();
            }
        }
    });
}

function Reject(id) {
    $.ajax({
        url: `/Admin/RatingApproval/Reject?id=${id}`,
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTable.ajax.reload();
            } else {
                toastr.error(data.message);
                dataTable.ajax.reload();
            }
        }
    });
}
