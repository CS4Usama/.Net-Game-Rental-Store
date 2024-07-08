var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#userPkgData').DataTable({
        "ajax": { "url": "/User/UserPackage/GetAll" },
        "columns": [
            { "data": "subscriptionPackage.packageName", "width": "15%" },
            { "data": "subscriptionPackage.gamesPerMonth", "width": "14%", "className": "text-center" },
            { "data": "subscriptionPackage.rentNewReleasedGame", "width": "14%", "className": "text-center" },
            { "data": "subscriptionPackage.maxReplacePerMonth", "width": "14%", "className": "text-center" },
            { "data": "totalSubscriptionMonths", "width": "14%", "className": "text-center" },
            { "data": "subscribedDate", "width": "15%", "className": "text-center" },
            { "data": "expiredDate", "width": "15%", "className": "text-center" },
        ]
    });
}
