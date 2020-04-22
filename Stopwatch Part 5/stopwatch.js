const MINUTES_CLASS_NAME = "minutes";
const SECONDS_CLASS_NAME = "seconds";
const MILLISECONDS_CLASS_NAME = "milliseconds";
const START_BUTTON_CLASS_NAME = "start-button";
const STOP_BUTTON_CLASS_NAME = "stop-button";
let displayElapsedInterval;

bindControlsEvents();

function bindControlsEvents() {
    document.getElementsByClassName("start-button")[0].onclick = onStart;
    document.getElementsByClassName("stop-button")[0].onclick = onStop;
}

function onStart() {
    resetTime()
    this.disabled = true;
    document.getElementsByClassName("stop-button")[0].disabled = false;
    displayElapsedInterval = setInterval(displayElapsed, 1, Date.now())
}

function displayElapsed(startingTime) {
    let elapsedMilliseconds = Date.now() - startingTime;
    document.getElementsByClassName(MILLISECONDS_CLASS_NAME)[0].innerHTML =
        (elapsedMilliseconds % 1000).toString().padStart(3, '0');
    document.getElementsByClassName(SECONDS_CLASS_NAME)[0].innerHTML =
        (Math.floor(elapsedMilliseconds % 60000 / 1000)).toString().padStart(2, '0');
    document.getElementsByClassName(MINUTES_CLASS_NAME)[0].innerHTML = 
        (Math.floor(elapsedMilliseconds / 60000)).toString().padStart(2, '0');
}

function onStop() {
    clearInterval(displayElapsedInterval);
    this.disabled = true;
    document.getElementsByClassName("start-button")[0].disabled = false;
}

function resetTime() {
    document.getElementsByClassName(MINUTES_CLASS_NAME)[0].innerHTML = "00";
    document.getElementsByClassName(SECONDS_CLASS_NAME)[0].innerHTML = "00";
    document.getElementsByClassName(MILLISECONDS_CLASS_NAME)[0].innerHTML = "000";
}