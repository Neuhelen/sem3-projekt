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
});
