var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tbldata').DataTable({
        "ajax": {
            "url": "/Internshipjob/GetAll"
        },
        "columns": [

            { "data": "id", "width": "15%" },
            { "data": "companyName", "width": "15%" },
            { "data": "jobTitle", "width": "15%" },
            { "data": "location", "width": "15%" },
            { "data": "datePosted", "width": "15%" },
            { "data": "skillsReq", "width": "15%" },
            { "data": "type", "width": "15%" },
            { "data": "site", "width": "15%" },
            { "data": "imageUrl", "width": "15%" },
            { "data": "description", "width": "15%" },
            
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="editdelete">
                        <a class="edit" href = "/Internshipjob/Upsert?id=${data}"><img src="https://i.ibb.co/M8CWKSM/Group-416.png" height="25px"></a >
                            <a class="delete" onClick=Delete('Internshipjob/DeletePost/${data}')><img src="https://i.ibb.co/PWwbzb3/Group-417.png" height="25px"></a>
                        </div >`


                },
                "width": "15%"
            }
        ]

    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}