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

function enableModalPanel(panelId, enable) {
    if(enable) {
        $('#' + panelId).modal('show');
    }
    else {
        $('#' + panelId).modal('hide');
    }
}

function makeTableSortable(tableId) {
    const table = document.getElementById(tableId);
    const headers = table.querySelectorAll('.sortable-header');
    let isAscending = true;

    headers.forEach(header => {
        header.addEventListener('click', () => {
            const tbody = table.tBodies[0];
            const rows = Array.from(tbody.rows);
            const columnIndex = Array.from(header.parentNode.children).indexOf(header);
            const isNumeric = header.classList.contains('numeric');

            const sortedRows = rows.sort((a, b) => {
                const cellA = a.cells[columnIndex].textContent.trim();
                const cellB = b.cells[columnIndex].textContent.trim();

                if (isNumeric) {
                    return isAscending ? parseFloat(cellA) - parseFloat(cellB) : parseFloat(cellB) - parseFloat(cellA);
                } else {
                    return isAscending ? cellA.localeCompare(cellB) : cellB.localeCompare(cellA);
                }
            });

            isAscending = !isAscending;
            sortedRows.forEach(row => tbody.appendChild(row));
        });
    });
}

function filterTable(tableId, inputSelector, searchableColumns = []) {
    const input = document.querySelector(inputSelector);
    const filter = input.value.toLowerCase();
    const table = document.getElementById(tableId);
    const rows = table.getElementsByTagName('tr');

    for (let i = 1; i < rows.length; i++) { // Start from 1 to skip the header row
        const cells = rows[i].getElementsByTagName('td');
        let shouldDisplay = false;

        searchableColumns.forEach(index => {
            let cellText = cells[index].textContent || cells[index].innerText;
            if (cellText.toLowerCase().indexOf(filter) > -1) {
                shouldDisplay = true;
            }
        });

        rows[i].style.display = shouldDisplay ? '' : 'none';
    }
}