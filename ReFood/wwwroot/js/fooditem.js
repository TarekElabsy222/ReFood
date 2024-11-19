var dtbl;
$(document).ready(function () {
    dtbl = $('#fooditem').DataTable({
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/Admin/FoodItem/GetData",
            "type": "POST",
            "datatype": "json"
        },
        "columnsDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }],
        "columns": [
            { "data": "name", "name": "Id", "autowidth": true },
            { "data": "description", "name": "Description", "autowidth": true },
            { "data": "price", "name": "Price", "autowidth": true },
            { "data": "calories", "name": "Calories", "autowidth": true },
            { "data": "fat", "name": "Fat", "autowidth": true },
            { "data": "carbs", "name": "Carbs", "autowidth": true },
            { "data": "protein", "name": "Protein", "autowidth": true },
            { "data": "category.name", "name": "Category", "autowidth": true },
            {
                "data": "id",
                "render": function (data) {
                    return `
                                <a href="/Admin/FoodItem/Edit/${data}" class="btn btn-success">Edit</a>
                                <a onClick=DeleteItem("/Admin/FoodItem/Delete/${data}") class="btn btn-danger">Delete</a>
                            `
                }
            }
        ]
    });
});


function DeleteItem(url) {
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
                type: "Delete",
                success: function (data) {
                    if (data.success) {
                        dtbl.ajax.reload(null, false);
                        Swal.fire({
                            title: "Deleted!",
                            text: "Your food item has been deleted.",
                            icon: "success"
                        });
                        toaster.success(data.message);
                    } else {
                        toaster.error(data.message)
                    }
                }
            });
            Swal.fire({
                title: "Deleted!",
                text: "Your file has been deleted.",
                icon: "success"
            });
        }
    });
}