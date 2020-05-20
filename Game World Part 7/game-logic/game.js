import { tileStates, selectedModeSettings as modeSetttings } from '../game-settings/game-settings.js';
import { startTime, stopTime } from './stopwatch.js';
import { Board } from './board.js';

const startGame = () => {
  startTime();

  return new Board(modeSetttings.rowsCount, modeSetttings.columnsCount, modeSetttings.minesCount);
};

export const updateFlagsData = (previousMarking, currentMarking) => {
  if (previousMarking === tileStates.FLAGGED) {
    board.flaggedTilesCount--;
  } else if (currentMarking === tileStates.FLAGGED) {
    board.flaggedTilesCount++;
  }

  updateFlagsDisplay();
};

const updateFlagsDisplay = () => {
  const flagsDisplayElement = document.getElementsByClassName('marked-flags')[0];

  flagsDisplayElement.textContent = `${board.flaggedTilesCount}/${modeSetttings.minesCount}`;
};

export const revealTile = tile => {
  const { neighborTiles, minedNeighborsCount } = board.getNeighborsInfo(tile);

  tile.setAsRevealed(minedNeighborsCount);

  if (!minedNeighborsCount) {
    neighborTiles.filter(({ isUnmarked }) => isUnmarked())
      .forEach(revealTile);
  }

  if (board.isGameWon()) {
    winGame();
  }
};

export const onDoubleClick = tile => {
  const { neighborTiles, minedNeighborsCount, flaggedNeighborsCount } = board.getNeighborsInfo(tile);

  if (flaggedNeighborsCount === minedNeighborsCount) {
    neighborTiles.forEach(({ element }) => { element.click(); });
  }
};

export const loseGame = () => {
  const loseMessageClass = 'lose-cover';

  board.revealAllMines();
  finishGame(loseMessageClass);
};

const winGame = () => {
  const winMessageClass = 'win-cover';

  finishGame(winMessageClass);
};

const finishGame = messageElementClass => {
  stopTime();
  displayGameOverMessage(messageElementClass);
};

const displayGameOverMessage = messageElementClass => {
  const messageElement = document.getElementsByClassName(messageElementClass)[0];

  board.element.classList.add('disabled');
  messageElement.style.display = 'block';
  setTimeout(() => {
    messageElement.classList.add('shown');
  }, 500);
};

export const restart = () => {
  stopTime();
  hideGameOverMessage();
  board = startGame();
  updateFlagsDisplay();
};

const hideGameOverMessage = () =>
  document.querySelectorAll('[class*=cover]').forEach(messageElement => {
    messageElement.style.display = 'none';
    messageElement.classList.remove('shown');
  });

let board = startGame();
updateFlagsDisplay();
document.getElementsByClassName('restart-button')[0].addEventListener('click', restart);
