$(document).ready(function () {
    $('#Department').dataTable({
        "ajax": loadDepartment(),
        "responsive": true,
    });
    $('[data-toggle="tooltip"]').tooltip();
});

function loadDepartment() {
    $.ajax({
        url: "/Dept/cloadDepartment",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            debugger;
            var html = '';
            $.each(result, function (key, Department) {
                html += '<tr>';
                html += '<td>' + Department.Name + '</td>';
                html += '<td>' + moment(Department.CreateDate).format('DD-MM-YYYY') + '</td>';
                html += '<td>' + moment(Department.UpdateDate).format('DD-MM-YYYY') + '</td>';
                html += '<td><button type="button" class="btn btn-warning" id="Update" onclick="return GetById(' + Department.Id + ')">Edit </button>';
                html += '<button type="button" class="btn btn-danger" id="Delete" onclick="return Delete(' + Department.Id + ')">Delete </button></td>';
                html += '</tr>';
            });
            $('.departmentbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText)
        }
    });
}

function Save() {
    debugger;
    var Department = new Object();
    Department.Name = $('#Name').val();
    $.ajax({
        type: 'POST',
        url: '/Dept/Insert',
        data: Department
    }).then((result) => {
        debugger;
        if (result.StatusCode == 200) {
            Swal.fire({
                potition: 'center',
                type: 'success',
                title: 'Department Add Successfully',
                timer: 2500
            }).then(function () {
                location.reload();
                ClearScreen();
            });
        } else {
            Swal.fire('Error', 'Filed to Input', 'error');
            ClearScreen();
        }
    })
}


//$(document).ready(function () {
//    $('#Dept').dataTable({
//        "ajax": loadDept(),
//        "responsive": true,
//    });
//    $('[data-toggle="tooltip"]').tooltip();
//});

//function loadDept() {
//    $.ajax({
//        url: "/Dept/loadDept",
//        type: "GET",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        success: function (result) {
//            debugger;
//            var html = '';
//            $.each(result, function (key, Dept) {
//                html += '<tr>';
//                html += '<td>' + Name + '</td>';
//                html += '<td>' + CreateDate + '</td>';
//                html += '<td>' + UpdateDate + '</td>';
//                html += '<td>' + + '</td>';
//                html += '<td><button type="button" class="btn btn-warning" id="Update" onclick="return GetById(' + Dept.Id + ')">Edit </button>';
//                html += '<button type="button" class="btn btn-danger" id="Delete" onclick="return Delete(' + Dept.Id + ')">Delete </button></td>';
//                html += '</tr>';
//            });
//            $('.deptbody').html(html);
//        },
//        error: function (errormessage) {
//            alert(errormessage.responseText);
//        }
//    });
//}