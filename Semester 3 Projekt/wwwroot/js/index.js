document.addEventListener('DOMContentLoaded', function () {
    const startButton = document.querySelector('.startBtn');
    const stopButton = document.querySelector('.stopBtn');
    const continueButton = document.querySelector('.continueBtn');
    const alertPopup = document.getElementById('alertPopup'); // Make sure to have this element in your HTML

    function updateStatus(status) {
        var alertPopup = document.getElementById('alertPopup');

        // Remove existing status classes
        alertPopup.classList.remove('running', 'stopped', 'maintenance', 'needsRefill');

        // Set the text and add the class for the new status
        alertPopup.textContent = 'Status: ' + status;
        alertPopup.classList.add(status.toLowerCase());
        alertPopup.style.display = 'block'; // Show the alert popup
    }

    function updateCurrentBatch(batchId) {
        document.getElementById('currentBatch').textContent = batchId;
        // Example
        updateCurrentBatch('1'); // Call this function when the batch changes
    }

    // Event listeners for buttons
    document.addEventListener('DOMContentLoaded', function () {
        var startButton = document.querySelector('.startBtn');
        var stopButton = document.querySelector('.stopBtn');
        var continueButton = document.querySelector('.continueBtn');

        startButton.addEventListener('click', function () {
            updateStatus('Running');
        });

        stopButton.addEventListener('click', function () {
            updateStatus('Stopped');
        });

        continueButton.addEventListener('click', function () {
            updateStatus('Running'); // Assuming 'Continue' should also set the status to 'Running'
        });
    });

    // Example logic to update maintenance bar
    function updateMaintenanceBar() {
        let progress = calculateMaintenanceProgress(); // Implement this function based on your logic
        let maintenanceBar = document.getElementById('maintenanceProgress');
        let maintenanceStatus = document.getElementById('maintenanceStatus');

        maintenanceBar.style.width = progress + '%';

        if (progress >= 100) {
            maintenanceStatus.innerText = 'Maintenance Required';
            maintenanceBar.style.backgroundColor = 'red';
        } else {
            maintenanceStatus.innerText = 'Maintenance Status: Good';
            maintenanceBar.style.backgroundColor = '#4CAF50';
        }
    }

    // Call this function based on specific triggers or intervals
    updateMaintenanceBar();
});
