// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function Login() {
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

        if (result.tokenJWT != null) {
            
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Login Success',
                showConfirmButton: false,
                timer: 1500
            }).then((result) => {
                window.location.href = "../employees";
            })


        } else {
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: result.message,
                showConfirmButton: false,
                timer: 1500
            })
            console.log(result.message)
        }

    }).fail((error) => {
        //alert pemberitahuan jika gagal
        console.log("gagal");
    })
}




