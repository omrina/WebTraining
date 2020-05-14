import { settings } from '../game-settings/game-settings.js';
import { startTime, stopTime } from './stopwatch.js';
import { Board } from './board.js';

export const startGame = () => {
  board = new Board(settings.rowsCount, settings.columnsCount, settings.minesCount);
  stopTime();
  startTime();
};

export const updateFlagsDisplay = board => {
  const flaggedTilesCount = board.flat().filter(({ isFlagged }) => isFlagged()).length;

  setFlagsDisplay(flaggedTilesCount);
};

const setFlagsDisplay = (flaggedTilesCount = 0) => {
  document.getElementsByClassName('marked-flags')[0].textContent =
    `${flaggedTilesCount}/${settings.minesCount}`;
};

export const onRestart = () => {
  resetBoardElement(board.element);
  hideGameOverMessage();
  setFlagsDisplay();
  startGame(settings);
};

const resetBoardElement = boardElement => {
  boardElement.classList.remove('disabled');
  boardElement.textContent = '';
};

export const displayGameOverMessage = (boardElement, messageElementClass) => {
  const coveringMessageElement = document.getElementsByClassName(messageElementClass)[0];

  boardElement.classList.add('disabled');
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

let board;
document.getElementsByClassName('restart-button')[0].addEventListener('click', onRestart);
startGame();