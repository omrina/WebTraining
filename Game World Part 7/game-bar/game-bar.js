import { startGame } from '../game-logic/game.js';
import { settings } from '../game-settings/game-settings.js';
import { hideGameOverMessage } from '../game-logic/game-over-message.js';

export const updateFlagsDisplay = board => {
  const minesCount = board.flat().filter(x => x.isMine).length;
  const flaggedTilesCount = board.flat().filter(x => x.isFlagged()).length;
  setFlagsDisplay(minesCount, flaggedTilesCount);
};

const setFlagsDisplay = (minesCount, flaggedTilesCount = 0) => {
  document.getElementsByClassName('marked-flags')[0].textContent = `${flaggedTilesCount}/${minesCount}`;
};

export const onRestart = settings => {
  resetBoardElement();
  hideGameOverMessage();
  setFlagsDisplay(settings.minesCount);
  startGame(settings);
};

const resetBoardElement = () => {
  const boardElement = document.getElementsByClassName('game-board')[0];
  boardElement.classList.remove('disabled');
  boardElement.textContent = '';
};

document.getElementsByClassName('restart-button')[0].onclick = () => onRestart(settings);