import { settings } from '../game-settings/game-settings.js';
import { initializeBoard, assignMinesOnBoard } from './board-initializer.js';
import { displayBoard } from './board-displayer.js';
import { onRightClick } from './right-click.js';
import { revealTile } from './tile-reveal.js';
import { onMineReveal } from './mine-reveal.js';
import { startTime, stopTime } from '../game-bar/stopwatch.js';
import { getLocation } from './board-info.js';

export const startGame = settings => {
  const board = initializeBoard(settings);
  displayBoard(settings.rowsCount, settings.columnsCount);
  bindTilesClicks(board, settings);
  stopTime();
  startTime();
};

const bindTilesClicks = (board, settings) => {
  const tilesElements = [...document.getElementsByClassName('tile')];
  bindFirstOnceClick(tilesElements, board, settings);
  bindRightClick(tilesElements, board);
  bindOneClick(tilesElements, board);
};

const bindFirstOnceClick = (tilesElements, board, settings) => {
  document.getElementsByClassName('game-board')[0].addEventListener('click', event => {
    if (tilesElements.includes(event.target)){
      assignMinesOnBoard(getLocation(event.target), board, settings);
      bindMineClick(tilesElements, board);
    }
    else {
      bindFirstOnceClick(tilesElements, board, settings);
    }
  }, { once: true , capture: true});
};

const bindRightClick = (tilesElements, board) => {
  tilesElements.forEach(tile => tile.oncontextmenu = event => {
    event.preventDefault();
    onRightClick(tile, board);
  });
};

const bindOneClick = (tilesElements, board) => {
  tilesElements.forEach(tile => tile.onclick = () => {
    revealTile(tile, board);
  });
};

const bindMineClick = (tilesElements, board) => {
  tilesElements.filter(tile => {
    const [row, col] = getLocation(tile);

    return board[row][col].isMine;
  }).forEach(minedTile => minedTile.onclick = () => onMineReveal(minedTile, board));
};

startGame(settings);