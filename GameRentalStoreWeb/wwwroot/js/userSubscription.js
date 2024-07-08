var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#userSubscriptionsData').DataTable({
        "ajax": { "url": "/Admin/User/GetAllUserPackages/GetAll" },
        "columns": [
            { "data": "applicationUser.name", "width": "15%" },
            { "data": "applicationUser.email", "width": "20%", "className": "text-center" },
            { "data": "subscriptionPackage.packageName", "width": "15%", "className": "text-center" },
            { "data": "totalSubscriptionMonths", "width": "20%", "className": "text-center" },
            { "data": "subscribedDate", "width": "15%", "className": "text-center" },
            { "data": "expiredDate", "width": "15%", "className": "text-center" },
        ]
    });
}
