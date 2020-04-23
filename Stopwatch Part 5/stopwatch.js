(function () {
    function bindControlsEvents() {
        startButton.onclick = onStart;
        stopButton.onclick = onStop;
    }

    function onStart() {
        startButton.disabled = true;
        stopButton.disabled = false;
        updateElapsedInterval = setInterval(updateElapsed, 1, Date.now())
    }

    function updateElapsed(startingTime) {
        const elapsedTime = new Date(Date.now() - startingTime);
        const minutes = padWithZeros(elapsedTime.getMinutes(), 2);
        const seconds = padWithZeros(elapsedTime.getSeconds(), 2);
        const milliseconds = padWithZeros(elapsedTime.getMilliseconds(), 3);
        elapsedDisplay.innerHTML = minutes + ":" + seconds + ":" + milliseconds;
    }

    function padWithZeros(number, minimumLength) {
        return number.toString().padStart(minimumLength, '0');
    }

    function onStop() {
        clearInterval(updateElapsedInterval);
        stopButton.disabled = true;
        startButton.disabled = false;
    }

    const elapsedDisplay = document.getElementsByClassName("elapsed-display")[0];
    const startButton = document.getElementsByClassName("start-button")[0];
    const stopButton = document.getElementsByClassName("stop-button")[0];
    let updateElapsedInterval;

    bindControlsEvents();
})();