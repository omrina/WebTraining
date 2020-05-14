export const stopTime = () => clearInterval(updateElapsedInterval);

export const startTime = () => {
  const elapsedElement = document.getElementsByClassName('elapsed-time')[0];
  elapsedElement.innerHTML = '00:00';
  updateElapsedInterval = setInterval(updateElapsed, 1000, Date.now(), elapsedElement);
};

const updateElapsed = (startingTime, elapsedElement) => {
  const elapsedTime = new Date(Date.now() - startingTime);
  const minutes = padWithZeros(elapsedTime.getMinutes(), 2);
  const seconds = padWithZeros(elapsedTime.getSeconds(), 2);
  elapsedElement.innerHTML = minutes + ':' + seconds;
};

const padWithZeros = (number, minimumLength) => number.toString().padStart(minimumLength, '0');

let updateElapsedInterval;