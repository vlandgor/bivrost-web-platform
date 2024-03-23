// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$(document).ready(function() {
    // When the button is clicked
    $(".project-button").click(function() {
        // Toggle the visibility of the hidden menu
        $("#projectInfoPanel").toggle();

        // Update the text content of the hidden menu with the project name
        var projectFullName = $(this).data('fullname');
        var projectShortName = $(this).data('shortname')
        var projectId = $(this).data('project-id');
        $("#projectName").text(projectFullName);

        $("#projectLink").attr('href', '/Project?projectId=' + projectId);
    });
});

document.addEventListener("DOMContentLoaded", function() {
    var studentsBtn = document.getElementById("studentsBtn");
    var instructorsBtn = document.getElementById("instructorsBtn");
    var studentsTable = document.getElementById("StudentsSessionTable");
    var instructorsTable = document.getElementById("InstructorsSessionTable");
    var addParticipantButton = document.getElementById("addParticipantBtn");

    studentsBtn.addEventListener("click", function() {
        studentsBtn.classList.add("active-underline");
        instructorsBtn.classList.remove("active-underline");
        studentsTable.style.display = "table";
        instructorsTable.style.display = "none";
        addParticipantButton.textContent = "ADD STUDENT";
    });

    instructorsBtn.addEventListener("click", function() {
        instructorsBtn.classList.add("active-underline");
        studentsBtn.classList.remove("active-underline");
        instructorsTable.style.display = "table";
        studentsTable.style.display = "none";
        addParticipantButton.textContent = "ADD INSTRUCTOR";
    });
});