import { getNeighborsElements, getNeighborsData, getLocation } from './board-info.js';
import { displayGameOverMessage } from './game-over-message.js';
import { onDoubleClick } from './double-click.js';

export const revealTile = (tile, board) => {
  let tilesToReveal = [tile];

  while (tilesToReveal.length) {
    const tileElement = tilesToReveal.shift();
    const neighborsData = getNeighborsData([...getLocation(tileElement)], board);
    const neighborMinesCount = neighborsData.filter(x => x.isMine).length;
    revealIfNotMarked(tileElement, board, () => {
      revealNumber(tileElement, board, neighborMinesCount);

      if (!neighborMinesCount) {
        tilesToReveal = [...tilesToReveal,
          ...getNeighborsElements(neighborsData.filter(x => !x.isRevealed), board)];
        tilesToReveal = [...new Set(tilesToReveal)];
      }
    });
  }
};

export const revealIfNotMarked = (tile, board, revealFunction) => {
  const [row, col] = getLocation(tile);

  if (!board[row][col].isMarked()) {
    revealFunction();
  }
};

const revealNumber = (tile, board, minesCount) => {
  const numbersClasses = ['zero', 'number-one', 'number-two', 'number-three', 'number-four',
    'number-five', 'number-six', 'number-seven', 'number-eight'];
  const [row, col] = getLocation(tile);

  tile.classList.add(numbersClasses[minesCount]);
  tile.classList.remove('unrevealed-tile');
  board[row][col].isRevealed = true;
  rebindClicksAsRevealedTile(tile, board);

  if (isGameWon(board)) {
    displayGameOverMessage('win-cover');
  }
};

const rebindClicksAsRevealedTile = (tile, board) => {
  tile.onclick = null;
  tile.oncontextmenu = null;
  tile.ondblclick = () => onDoubleClick(tile, board);
};

const isGameWon = board => {
  const allTiles = board.flat();
  const minesCount = allTiles.filter(x => x.isMine).length;
  const revealedTilesCount = allTiles.filter(x => x.isRevealed).length;

  return allTiles.length - revealedTilesCount === minesCount;
};