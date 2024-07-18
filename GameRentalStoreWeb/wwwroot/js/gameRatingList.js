var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#userGameRatings').DataTable({
        "ajax": { "url": "/User/GameRating/GetAllGameRatings" },
        "columns": [
            { "data": "gameName", "width": "25%" },
            { "data": "review", "width": "45%" },
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
                "width": "15%",
                "className": "text-center"
            },
            {
                "data": "status",
                "render": function (data) {
                    if (data == "Pending") {
                        return `<span class='text-warning fw-medium''>Pending</span>`;
                    } else if (data == "Approved") {
                        return `<span class='text-success fw-medium''>Approved</span>`;
                    } else if (data == "Rejected") {
                        return `<span class='text-danger fw-medium''>Rejected</span>`;
                    }
                },
                "width": "15%", "className": "text-center"
            }
        ]
    });
}
