let elapsedDisplay = document.getElementsByClassName("elapsed-display")[0];
let startButton = document.getElementsByClassName("start-button")[0];
let stopButton = document.getElementsByClassName("stop-button")[0];
let updateElapsedInterval;

bindControlsEvents();

function bindControlsEvents() {
    startButton.onclick = onStart;
    stopButton.onclick = onStop;
}

function onStart() {
    elapsedDisplay.innerHTML = "00:00:000";
    startButton.disabled = true;
    stopButton.disabled = false;
    updateElapsedInterval = setInterval(updateElapsed, 1, Date.now())
}

function updateElapsed(startingTime) {
    let elapsedMilliseconds = Date.now() - startingTime;
    let minutes = padWithZeros(Math.floor(elapsedMilliseconds / 60000), 2);
    let seconds = padWithZeros(Math.floor(elapsedMilliseconds % 60000 / 1000), 2);
    let milliseconds = padWithZeros(elapsedMilliseconds % 1000, 3);
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