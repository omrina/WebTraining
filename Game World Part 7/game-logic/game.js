import { selectedModeSettings as modeSetttings } from '../game-settings/game-settings.js';
import { startTime, stopTime } from './stopwatch.js';
import { Board } from './board.js';

export const startGame = () => {
  board = new Board(modeSetttings.rowsCount, modeSetttings.columnsCount, modeSetttings.minesCount);
  stopTime();
  startTime();
};

export const setFlagsDisplay = () => {
  document.getElementsByClassName('marked-flags')[0].textContent =
    `${board.flaggedTilesCount}/${modeSetttings.minesCount}`;
};

export const displayGameOverMessage = messageElementClass => {
  const messageElement = document.getElementsByClassName(messageElementClass)[0];
  
  board.element.classList.add('disabled');
  messageElement.style.display = 'block';
  setTimeout(() => {
    messageElement.classList.add('shown');
  }, 500);
  
  stopTime();
};

export const hideGameOverMessage = () =>
document.querySelectorAll('[class*=cover]').forEach(messageElement => {
  messageElement.style.display = 'none';
  messageElement.classList.remove('shown');
});

export const onRestart = () => {
  resetBoardElement(board.element);
  hideGameOverMessage();
  startGame();
  setFlagsDisplay();
};

const resetBoardElement = () => {
  board.element.classList.remove('disabled');
  board.element.textContent = '';
};

let board;
document.getElementsByClassName('restart-button')[0].addEventListener('click', onRestart);
startGame();