function makeFullscreen(divId) {
    var element = document.getElementById(divId);
    element.classList.toggle('block-fullscreen');
}

function showActiveSessions() {
    document.getElementById('activeSessionsTable').style.display = 'table';
    document.getElementById('finishedSessionsTable').style.display = 'none';
    document.getElementById('activeSessionsBtn').classList.add('active-menu-button');
    document.getElementById('finishedSessionsBtn').classList.remove('active-menu-button');
}

function showFinishedSessions() {
    document.getElementById('activeSessionsTable').style.display = 'none';
    document.getElementById('finishedSessionsTable').style.display = 'table';
    document.getElementById('activeSessionsBtn').classList.remove('active-menu-button');
    document.getElementById('finishedSessionsBtn').classList.add('active-menu-button');
}