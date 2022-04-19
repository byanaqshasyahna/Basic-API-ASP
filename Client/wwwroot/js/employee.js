// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$.ajax({
    //url didapat dari local client
    url: "university/getall",
    success: function (result) {
        //console.log(result);
        var text = "";

        var univName = [];
        for (var i = 0; i < result.length; i++) {
            text += `<option value="${i + 4}">${result[i].name}</option>`
            univName.push(result[i].name);
        }

        //untuk memberi option saat regist
        $("#university").append(text);


        ChartView(univName);



    }

})




$(document).ready(function () {
    $('.table-employee').DataTable({
        dom: "<'row'<'col-md-3'l><'col-md-5'B><'col-md-4'f>>" +
            "<'row'<'col-md-12'tr>>" +
            "<'row'<'col-md-5'i><'col-md-7'p>>"
        ,
        buttons: [
            'excel', 'pdf',
            {
                text: 'Add New Employee',
                action: function (e, dt, node, config) {
                    $('#modalRelatedContent').modal('show');
                }
            }
        ],
        lengthMenu: [
            [5, 10, 25, 50, 100, -1],
            [5, 10, 25, 50, 100, "All"]

        ],
        "filter": true,
        "orderMulti": false,
        "ajax": {
            /*"url": "https://localhost:56213/api/Employees/MasterData",*/
            //url di dapat dari local client
            "url": "employees/Getmasterdata",
            /*"headers": {
                "Authorization": "Bearer " + "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJFbWFpbCI6ImRldmlAZ21haWwuY29tIiwicm9sZXMiOiJFbXBsb3llZSIsImV4cCI6MTY1MDI2ODQ3OSwiaXNzIjoiQVBJIn0.4Q-JcQnYFwD9zivV4EYyQyxS8s0NYkEDYJi6yQxOjkA"
            },*/
            "datatype": "json",
            "dataSrc": ""
        },
        "columns": [
            {
                "data": null,
                "name": "no",
                "autoWidth": true,
                "render": function (data, type, row, meta) { 
                    return meta.row + 1;
                }
            },
            {
                "data" : "nik"
            },
            {
                "data" : "fullname" 
            },
            {
                "data": "birthDate",
                "render": function (data, type, row, meta) {
                    var d = new Date(data)
                    return moment(d).format('ll');
                    /*return data;*/
                }
            },
            {
                "data" : "email"
            },
            {
                "data": "phone",
                "render": function (data, type, row, meta) {
                    const [first, ...rest] = data.split('0')
                    return "+62" + rest.join('0');
                }
            },
            {
                "data": "salary",
                "render": function (data, type, row, meta) {
                    //render berfungsi utk membuat column bisa kita manipulasi string nya
                    return "Rp. " + row["salary"];
                },
                "autoWidth": true
            },
            {
                "data": null,
                "orderable":false,
                "render": function (data, type, row, meta) {
                    //render berfungsi utk membuat column bisa kita manipulasi string nya
                    return `<div class="row">
                            <div class="col"><button class="btn btn-primary"  onClick="Delete('${row["nik"]}')">
                            <i class="fas fa-trash"></i>
                            </button></div>
                            <div class="col"><button class="btn btn-primary" data-toggle="modal" data-target="#modalUpdate" onClick="GetDataForUpdate('${row["nik"]}',
                                                '${row["fullname"]}','${row["birthDate"]}','${row["email"]}','${row["phone"]}' ,'${row["salary"]}','${row["gender"]}' )" ')">
                            <i class="fas fa-pen"></i>
                            </button></div>
                            </div>

                            `;
                }
            }
        ]
    });
});

function register() {

    event.preventDefault();

    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    obj.FirstName =  $("#firstName").val();
    obj.LastName = $("#lastName").val();
    obj.Gender = parseInt($("#gender").val());
    obj.Phone = $("#phoneNumber").val();
    obj.Birthdate = $("#birthDate").val();
    obj.salary = $("#salary").val();
    obj.Email = $("#email").val();
    obj.Password = $("#password").val();
    obj.Degree = $("#degree").val();
    obj.GPA = $("#gpa").val().toString();
    obj.UniversityId = parseInt($("#university").val());

    console.log(obj);


    $.ajax({
        //from local api
        /*url: 'https://localhost:56213/api/Accounts/Register',*/
        //from local client
        url: 'accounts/register',
        type: 'POST',
        /*headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
        },
        dataType: 'json',*/
        /*data: JSON.stringify(obj),*/
        data: obj,
    }).done((result) => {
        //buat alert pemberitahuan jika success
        console.log(result);

        if (result == 200) {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Your work has been saved',
                showConfirmButton: false,
                timer: 1500
            }).then((result) => {
                setTimeout(location.reload(), 5000);
            })
        } else {
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: 'Fail to register',
                showConfirmButton: false,
                timer: 1500
            })
        }
        console.log("Sukses");
    }).fail((error) => {
        //alert pemberitahuan jika gagal
        console.log(error);
    })
}
/*function Hapus() {

    var nik = $(".deletebynik").text();
    console.log(nik);

    $.ajax({
        url: 'https://localhost:56213/api/Accounts/delete/'+ nik,
        type: 'DELETE'
    }).done((result) => {
        //buat alert pemberitahuan jika success
        console.log(result);
    }).fail((error) => {
        //alert pemberitahuan jika gagal
        console.log(error);
    })

}*/

function Delete(nik) {
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
                url: "accounts/deleteacc?nik=" + nik ,
                type: 'DELETE'
            }).done((result) => {
                console.log(result);
                //buat alert pemberitahuan jika success
                Swal.fire(
                    'Deleted!',
                    'Your file has been deleted.',
                    'success'
                )/*.then((result) => {
                    setTimeout(location.reload(), 5000);
                })*/
                
            }).fail((error) => {
                //alert pemberitahuan jika gagal
                console.log("eror bro");
            })
            
        }
    })
}

function Update() {

    event.preventDefault();

    var obj = new Object();
    obj.NIK = $("#modalUpdate .empNIK").text();
    obj.Firstname = $("#modalUpdate #firstName").val();
    obj.Lastname = $("#modalUpdate #lastName").val();
    obj.Phone = $("#modalUpdate #phoneNumber").val();
    obj.BirthDate = $("#modalUpdate #birthDate").val();
    obj.salary = $("#modalUpdate #salary").val();
    obj.Email = $("#modalUpdate #email").val();
    obj.Gender = parseInt($("#modalUpdate #gender").val());
    console.log(obj);
    $.ajax({
        url: 'Employees/put',
        type: 'PUT',
        /*headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
        },
        dataType: 'json',
        data: JSON.stringify(obj),*/
        data: obj,
    }).done((result) => {
        //buat alert pemberitahuan jika success
        
        Swal.fire({
            position: 'center',
            icon: 'success',
            title: 'Your work has been saved',
            showConfirmButton: false,
            timer: 1500
        }).then((result) => {
            setTimeout(location.reload(), 5000);
        })
        
        console.log("Sukses");
    }).fail((error) => {
        //alert pemberitahuan jika gagal
        console.log(error);
    })
    
}

function GetDataForUpdate(nik, fullname, birthdate, email, phonenumber, salary, gender) {

    /*console.log( nik);
    console.log( fullname);
    console.log( birthdate);
    console.log( email);
    console.log( phonenumber);
    console.log( salary);
    console.log(gender);*/

    const [first, ...rest] = fullname.split(' ');


    var now = new Date(birthdate);

    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);

    var today = now.getFullYear() + "-" + (month) + "-" + (day);
    console.log(now);
    $("#modalUpdate .empNIK").html(nik);

    $("#modalUpdate #firstName").val(first);
    $("#modalUpdate #lastName").val(rest);
    $("#modalUpdate #birthDate").val(today);
    $("#modalUpdate #email").val(email);
    $("#modalUpdate #phoneNumber").val(phonenumber);
    $("#modalUpdate #salary").val(salary);
    if (gender == "Male") {
        $("#modalUpdate #gender ").val("0");
    } else {
        $("#modalUpdate #gender ").val("1");
    }
    


}

function GetNIK(nik) {
    console.log(nik);
    $(".deletebynik").html(nik);
}


function ChartView(univName) {

    $.ajax({
        //url di dapet dari local api
        /*url: "https://localhost:56213/api/Employees/MasterData",*/

        //url di dapat dari local client
        url: "employees/Getmasterdata",
        success: function (result) {
            console.log(result);


            var countMale = 0;
            var countFamale = 0;
            for (var i = 0; i < result.length; i++) {
                var cowo = result[i].gender;

                if (cowo == "Male")
                    countMale += 1;
                else
                    countFamale += 1;



            }

            var options = {
                series: [countMale, countFamale],
                chart: {
                    width: 350,
                    type: 'pie',
                    toolbar: {
                        show: true,
                        offsetX: 0,
                        offsetY: 0,
                        tools: {
                            download: true,
                            selection: true,
                            zoom: true,
                            zoomin: true,
                            zoomout: true,
                            pan: true,
                            reset: true | '<img src="/static/icons/reset.png" width="20">',
                            customIcons: []
                        }
                    ,
                    export: {
                        csv: {
                            filename: undefined,
                            columnDelimiter: ',',
                            headerCategory: 'category',
                            headerValue: 'value',
                            dateFormatter(timestamp) {
                                return new Date(timestamp).toDateString()
                            }
                        },
                        svg: {
                            filename: undefined,
                        },
                        png: {
                            filename: undefined,
                        }
                    },
                    autoSelected: 'zoom'

                    }
                },
                labels: ['Male', 'Female'],
                responsive: [{
                    breakpoint: 480,
                    options: {
                        chart: {
                            width: 50
                        },
                        legend: {
                            position: 'bottom'
                        }
                    }
                }],

            };


            //GET DATA FOR CHART BAR UNIVERSITY
            var univCount = 0;
            var dataUniv = [];
            var data = [];
            var lengthBaris = test(result);
            console.log(lengthBaris)
            for (var i = 0; i < univName.length; i++) {
                data = [];
                for (var j = 0; j < lengthBaris.length; j++) {

                    if (lengthBaris[j].universityName == univName[i]) {
                        univCount += 1;
                    } else {
                        data.push(lengthBaris[j])
                    }
                }
                test(data);
                lengthBaris = test(data)
                console.log(lengthBaris);
                dataUniv.push(univCount);
                univCount = 0;
            }


            var options2 = {
                series: [{
                    data: dataUniv
                }],
                chart: {
                    type: 'bar',
                    height: 260
                },
                plotOptions: {
                    bar: {
                        borderRadius: 4,
                        horizontal: true,
                    }
                },
                dataLabels: {
                    enabled: false
                },
                xaxis: {
                    categories: univName,
                }
            };

            var chart = new ApexCharts(document.querySelector("#chart"), options);
            var chart2 = new ApexCharts(document.querySelector("#chart2"), options2);
            chart.render();
            chart2.render();


        }

    })

}

function test(data) {
    //console.log(data);
    var obj = data;
    return obj;
}

/*function Login() {
    event.preventDefault();
    console.log("telah login");

    var login = new Object();

    login.email = $("#emailLogin").val();
    login.password = $("#passwordLogin").val();

    console.log(login);

    $.ajax({
        
        url: 'https://localhost:44363/employees/login',
        type: 'POST',
        
        
        data: login,
    }).done((result) => {
        //buat alert pemberitahuan jika success
        console.log(result);
        //window.location.href = "../employees";

    }).fail((error) => {
        //alert pemberitahuan jika gagal
        console.log("gagal");
    })
}*/




