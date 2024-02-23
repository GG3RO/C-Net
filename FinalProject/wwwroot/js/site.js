// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function calculateBMI() {
    var weight = parseFloat(document.getElementById("weight").value);
    var height = parseFloat(document.getElementById("height").value);

    if (isNaN(weight) || isNaN(height) || weight <= 0 || height <= 0) {
        document.getElementById("result").innerText = "Please enter valid weight and height.";
        return;
    }

    var bmi = weight / ((height / 100) * (height / 100));
    document.getElementById("result").innerText = "Your BMI is: " + bmi.toFixed(2);
}




    document.addEventListener("DOMContentLoaded", function() {
        const loginForm = document.getElementById("loginForm");
        const registerForm = document.getElementById("registerForm");

        loginForm.addEventListener("submit", function(event) {
            if (!loginForm.checkValidity()) {
                event.preventDefault(); // Prevent form submission
                alert("Please fill the fields correctly."); // Display alert message
            }
        });

        registerForm.addEventListener("submit", function(event) {
            if (!registerForm.checkValidity()) {
                event.preventDefault(); // Prevent form submission
                alert("Please fill the fields correctly."); // Display alert message
            }
        });
    });

