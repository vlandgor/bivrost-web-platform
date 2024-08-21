// Function to initialize SignalR connection
function initializeSignalRConnection(hubUrl) {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl(hubUrl)
        .build();

    return connection;
}

// Function to handle the "LockStudent" event
function handleLockStudentEvent(connection, tableSelector) {
    connection.on("LockStudent", function (studentId) {
        const existingRow = document.querySelector(`${tableSelector} tbody tr[data-student-id="${studentId}"]`);

        if (existingRow) {
            // Assuming the lock status is in the first column (index 0)
            existingRow.cells[0].style.opacity = '1';
        } else {
            console.error(`Student row with ID ${studentId} not found.`);
        }
    });
}

// Function to handle the "UpdateStudentProgress" event
function handleUpdateStudentProgressEvent(connection, tableSelector) {
    connection.on("UpdateStudentProgress", function (studentId, studentProgress) {
        const existingRow = document.querySelector(`${tableSelector} tbody tr[data-student-id="${studentId}"]`);

        if (existingRow) {
            existingRow.cells[3].textContent = studentProgress;
        } else {
            console.error(`Student row with ID ${studentId} not found.`);
        }
    });
}

// Function to start the connection
function startConnection(connection) {
    connection.start()
        .then(function () {
            console.log("SignalR connection established.");
        })
        .catch(function (err) {
            console.error("SignalR connection error:", err);
        });
}