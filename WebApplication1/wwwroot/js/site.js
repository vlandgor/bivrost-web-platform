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

        $("#sessionLink").attr('href', '/Session?projectId=' + projectId);
    });
});
