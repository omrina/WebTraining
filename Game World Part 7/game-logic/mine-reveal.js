import { displayGameOverMessage } from './game-over-message.js';
import { revealIfNotMarked } from './tile-reveal.js';
import { getTileElement, getLocationByData } from './board-info.js';

export const onMineReveal = (tile, board) => {
  revealIfNotMarked(tile, board, () => {
    revealAllMines(board);
    displayGameOverMessage('lose-cover');
  });
};

const revealAllMines = board => {
  const mineClass = 'mined-tile';
  const minedTiles = board.flat().filter(x => x.isMine);
  const mineElements = minedTiles.map(tile => getTileElement(...getLocationByData(tile, board)));

  mineElements.forEach(x => {
    x.classList.add(mineClass);
    x.classList.remove('unrevealed-tile');
  });
};