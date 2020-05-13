import { stopTime } from '../game-bar/stopwatch.js';

export const displayGameOverMessage = messageElementClass => {
  const coveringMessageElement = document.getElementsByClassName(messageElementClass)[0];
  document.getElementsByClassName('game-board')[0].classList.add('disabled');
  coveringMessageElement.style.display = 'block';

  setTimeout(() => {
    coveringMessageElement.style.transform = 'scale(1.5)';
    coveringMessageElement.style.opacity = '0.7';
  }, 500);

  stopTime();
};

export const hideGameOverMessage = () =>
  document.querySelectorAll('[class$=cover]').forEach(coverElement => {
    coverElement.style.display = 'none';
    coverElement.style.transform = '';
    coverElement.style.opacity = '0';
  });