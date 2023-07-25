let dataTable;
$(document).ready(() => {
    loadDataTable();

})

const Delete = (id) => {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })

    swalWithBootstrapButtons.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'No, cancel!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: `/employee/${id}`,
                type: "delete",
                success: (data) => {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                },
                error: (xhr, status, error) => {
                    toastr.error("An error occurred during the delete request.");
                }
            });
        } else if (
            /* Read more about handling dismissals below */
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire(
                'Cancelled',
                'Your imaginary file is safe :)',
                'error'
            )
        }
    })
}
const loadDataTable = () => {
    dataTable = $('#myTable').DataTable({
        "ajax": { url: "/employee/getall" },
        "columns": [
            {
                data: null,
                render: function (data, type, full, meta) {
                    return `<input type="checkbox"  name="selectedIds" value="${data.id}">`;
                },
                "width": "5%"
            },
            { data: 'name', "width": "10%" },
            {
                data: 'dateOfBirth', render: function (data) {
                    // Format the date to display only day/month/year
                    const date = new Date(data);
                    const formattedDate = date.toLocaleDateString('en-US');
                    return formattedDate;
                },
                "width": "10%"
            },
            { data: 'age', "width": "5%" },
            {
                data: 'gender', render: function (data) {
                    return data==0?"Nam":"Nữ";
                },
                "width": "10%" },
            { data: 'identityCardNumber', "width": "10%" },
            { data: 'phoneNumber', "width": "10%" },
            { data: 'ethnicity.ethnicityName', "width": "5%" },
            { data: 'occupation.occupationName', "width": "5%" },
            { data: 'ward.wardName', "width": "10%" },
            { data: 'district.districtName', "width": "10%" },
            { data: 'city.cityName', "width": "10%" },
            {
                data: 'id',
                "render": (data) => {
                    return `
                        <div class=" btn-group " role="group">
                            <a href="/employee/edit/${data}" style="font-size:14px; height:auto" class="btn btn-primary mx-2">
                                <i  style="font-size:15px;" class="bi bi-pencil-square"></i>Edit
                            </a>
                            <a onClick="Delete(${data})" class="btn btn-danger mx-2">
                                <i class="bi bi-trash-fill"></i>Delete
                            </a>
                        </div>
                    `
                },
                "width": "5%"
            },
        ]
    });
    // Áp dụng lớp CSS cho input search
    const $searchContainer = $('.dataTables_filter');
    $searchContainer.addClass('custom-search-container');
    $searchContainer.find('label').addClass('custom-search-label');
    $searchContainer.find('input').addClass('custom-search-input');

}


