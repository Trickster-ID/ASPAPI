var Departments = [];
var datenow = new Date();
/*--------------------------------------------------------------------------------------------------*/
$(document).ready(function () {
    table = $('#Division').dataTable({
        "ajax": {
            url: "/Division/cloadDivisi",
            type: "GET",
            dataType: "json",
            dataSrc: "",
        },
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'copyHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 3]
                }
            },
            {
                extend: 'csvHtml5',
                filename: function () {
                    return 'tabledept ' + moment(datenow).format('DD/MM/YYYY');
                },
                title: 'Table Division',
                exportOptions: {
                    columns: [0, 1, 2, 3]
                }
            },
            {
                extend: 'excelHtml5',
                filename: function () {
                    return 'tabledept ' + moment(datenow).format('DD/MM/YYYY');
                },
                title: 'Table Division',
                exportOptions: {
                    columns: [0, 1, 2, 3]
                }
            },
            {
                extend: 'pdfHtml5',
                filename: function () {
                    return 'tabledept ' + moment(datenow).format('DD/MM/YYYY');
                },
                title: 'Table Division',
                exportOptions: {
                    columns: [0, 1, 2, 3]
                }
            },

            'print'
            //'colvis'
        ],
        "columnDefs": [
            { "orderable": false, "targets": 4 },
            { "searchable": false, "targets": 4 }
        ],
        "columns": [
            { "data": "Nama" },
            { "data": "DepartmentName" },
            {
                "data": "CreateDate", "render": function (data) {
                    return moment(data).format('DD/MM/YYYY, h:mm a');
                }
            },
            {
                "data": "UpdateDate", "render": function (data) {
                    var dateupdate = "Not Updated Yet";
                    var nulldate = null;
                    if (data == nulldate) {
                        return dateupdate;
                    } else {
                        return moment(data).format('DD/MM/YYYY, h:mm a');
                    }
                }
            },
            {
                data: null, render: function (data, type, row) {
                    return " <td><button type='button' class='btn btn-warning' id='BtnEdit' onclick=GetById('" + row.Id + "');>Edit</button> <button type='button' class='btn btn-danger' id='BtnDelete' onclick=Delete('" + row.Id + "');>Delete</button ></td >";
                }
            },
        ]
    });
}); //function load data division
/*--------------------------------------------------------------------------------------------------*/
document.getElementById("btnadd2").addEventListener("click", function () {
    $('#Id').val('');
    $('#Name').val('');
    $('#SaveBtn').show();
    $('#UpdateBtn').hide();
    loadDepartment2($('#DepartmentOption'));
}); //fungsi btn add
/*--------------------------------------------------------------------------------------------------*/
function loadDepartment2(element) { 
    if (Departments.length == 0) {
        $.ajax({
            type: "GET",
            url: "/Dept/cloadDepartment",
            success: function (data) {
                Departments = data;
                renderDepartment(element);
            }
        })
    }
    else {
        renderDepartment(element);
    }
} //Load data department
function renderDepartment(element) {
    var $option = $(element);
    $option.empty();
    $option.append($('<option/>').val('0').text('Select Department').hide());
    $.each(Departments, function (i, val) {
        $option.append($('<option/>').val(val.Id).text(val.Name));
    })
} //Load data department to option field
/*--------------------------------------------------------------------------------------------------*/
function GetById(Id) {
    $.ajax({
        url: "/Division/GetById/" + Id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (result) {
            const obj = JSON.parse(result);
            $('#Id').val(obj.Id);
            $('#Name').val(obj.Nama);
            $('#DepartmentOption').val(obj.DepartmentId);
            $('#myModalDiv').modal('show');
            $('#UpdateBtn').show();
            $('#SaveBtn').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responsText);
        }
    })
} //function get id to edit
/*--------------------------------------------------------------------------------------------------*/
function Save() {
    $.fn.dataTable.ext.errMode = 'none';
    table = $('#Division').DataTable({
        "ajax": {
            url: "/Division/cloadDivisi"
        }
    });
    var Divisi = new Object();
    Divisi.Nama = $('#Name').val();
    Divisi.DepartmentId = $('#DepartmentOption').val();
    if ($('#Name').val() == "") {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Name Cannot be Empty',
        })
        return false;
    } else {
        $.ajax({
            type: 'POST',
            url: '/Division/Insert/',
            data: Divisi
        }).then((result) => {
            if (result.StatusCode == 200) {
                Swal.fire({
                    icon: 'success',
                    potition: 'center',
                    title: 'Division Add Successfully',
                    timer: 2500
                }).then(function () {
                    $('#myModalDiv').modal('hide');
                    table.ajax.reload();
                    $('#Id').val('');
                    $('#Name').val('');
                    $('#DepartmentOption').val('');
                });
            }
            else {
                Swal.fire('Error', 'Failed to Input', 'error');
            }
        })
    }
} //function save
/*--------------------------------------------------------------------------------------------------*/
function Edit() {
    $.fn.dataTable.ext.errMode = 'none';
    var table = $('#Division').DataTable({
        "ajax": {
            url: "/Division/cloadDivisi"
        }
    });
    var Division = new Object();
    Division.Id = $('#Id').val();
    Division.Nama = $('#Name').val();
    Division.DepartmentId = $('#DepartmentOption').val();
    if ($('#Name').val() == "") {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Name Cannot be Empty',
        })
        return false;
    } else {
        $.ajax({
            type: 'POST',
            url: '/Division/Insert',
            data: Division
            //success: function () {
            //    $('#Update').hide();
            //    $('#Save').show();
            //}
        }).then((result) => {
            debugger;
            if (result.StatusCode == 200) {
                Swal.fire({
                    icon: 'success',
                    potition: 'center',
                    //type: 'success',
                    title: 'Division Update Successfully',
                    timer: 2500
                }).then(function () {
                    $('#myModalDiv').modal('hide');
                    table.ajax.reload();
                    $('#Id').val('');
                    $('#Name').val('');
                    $('#DepartmentOption').val('');
                });
            } else {
                Swal.fire('Error', 'Failed to Edit', 'error');
            }
        })
    }
} //function edit
/*--------------------------------------------------------------------------------------------------*/
function Delete(Id) {
    $.fn.dataTable.ext.errMode = 'none';
    var table = $('#Division').DataTable({
        "ajax": {
            url: "/Division/cloadDivisi"
        }
    });
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        showCancelButton: true,
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.value) {
            debugger;
            $.ajax({
                url: "/Division/Delete/",
                data: { Id: Id }
            }).then((result) => {
                debugger;
                if (result.StatusCode == 200) {
                    Swal.fire({
                        icon: 'success',
                        position: 'center',
                        title: 'Delete Successfully',
                        timer: 2000
                    }).then(function () {
                        $('#myModalDiv').modal('hide');
                        table.ajax.reload();
                        $('#Id').val('');
                        $('#Name').val('');
                        $('#DepartmentOption').val('');
                    });
                }
                else {
                    Swal.fire({
                        icon: 'error',
                        title: 'error',
                        text: 'Failed to Delete',
                    })
                    ClearScreen();
                }
            })
        }
    });
} //function delete