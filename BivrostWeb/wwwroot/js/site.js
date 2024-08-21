function makeFullscreen(divId) {
    var element = document.getElementById(divId);
    element.classList.toggle('block-fullscreen');
}

function toggleSection(element) {
    const sectionContent = element.nextElementSibling;
    const arrow = element.querySelector('.query-arrow');

    // Collapse all sections
    const allSections = document.querySelectorAll('.panel-section .section-content');
    allSections.forEach(content => {
        if (content !== sectionContent) {
            content.style.display = 'none';
            content.style.maxHeight = '0';
            content.previousElementSibling.querySelector('.query-arrow').style.transform = 'rotate(0deg)';
        }
    });

    // Expand the clicked section
    if (sectionContent.style.display === 'block') {
        sectionContent.style.display = 'none';
        sectionContent.style.maxHeight = '0';
        arrow.style.transform = 'rotate(0deg)';
    } else {
        sectionContent.style.display = 'block';
        sectionContent.style.minHeight = '100vh';
        sectionContent.style.maxHeight = '100vh'; /* Set max-height to allow full expansion */
        arrow.style.transform = 'rotate(180deg)';
    }
}

function toggleTable(event) {
    const allTables = document.querySelectorAll('table');
    const allButtons = document.querySelectorAll('.project-menu-button');
    const targetTableId = event.currentTarget.getAttribute('data-table');

    allTables.forEach(table => {
        table.style.display = 'none';
    });

    allButtons.forEach(button => {
        button.classList.remove('active-menu-button');
    });

    document.getElementById(targetTableId).style.display = 'table';
    event.currentTarget.classList.add('active-menu-button');
}

// JavaScript to handle button click event and show modal
document.getElementById('createModalButton').addEventListener('click', function () {
    $('#createModalPanel').modal('show');
});
document.getElementById('cancelButton').addEventListener('click', function () {
    $('#createModalPanel').modal('hide');
});