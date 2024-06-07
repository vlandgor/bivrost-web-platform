function makeFullscreen(divId) {
    var element = document.getElementById(divId);
    element.classList.toggle('block-fullscreen');
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