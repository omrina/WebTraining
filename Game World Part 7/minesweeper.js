import { settings } from './gameSettings.js' ;
{
  const initializeBoard = () => {
    const [isRevealed, isMine, markingStates] = [false, false, [...settings.markingStateClasses]];

    return Array(settings.rowsCount).fill().map(() =>
              Array(settings.columnsCount).fill().map(() =>
                 Object.assign({}, { isRevealed,
                                     isMine,
                                     markingStates: [...markingStates],
                                     currentMarkState () { return this.markingStates[0]; },
                                     isMarked () { return this.currentMarkState() !== 'unmarked'; }
                                    })));
  };

  const generateMinesLocations = minesAmount => {
    const locations = [];

    while (locations.length < minesAmount) {
        const newLocation = [Math.floor(Math.random() * settings.rowsCount), Math.floor(Math.random() * settings.columnsCount)]
        
        if (!locations.some(location => location[0] === newLocation[0] && location[1] === newLocation[1])){
            locations.push(newLocation);
        } 
    }

    return locations;
  };

  const assignMinesOnBoard = (board, minesLocations) => {
      minesLocations.forEach(location => {
          board[location[0]][location[1]].isMine = true;
      });
  }

  const getLocation = tile => {
    const parentRow = tile.parentNode;

    return [[...parentRow.parentNode.children].indexOf(parentRow),
            [...parentRow.children].indexOf(tile)];
  }

  const onRightClick = (tile, board) => {
    const [row, col] = getLocation(tile);
    const tileData = board[row][col];

    tile.classList.remove(tileData.currentMarkState())
    tileData.markingStates.push(tileData.markingStates.shift())
    tile.classList.add(tileData.currentMarkState())
  }

  const revealIfNotMarked = (tile, board, revealFunction) => {
    const [row, col] = getLocation(tile);

    if (!board[row][col].isMarked()) {
      revealFunction();
    }
  }

  const revealTile = (tile, board) => {
    let tilesToReveal = [tile];

    while (tilesToReveal.length) {
      const tileElement = tilesToReveal.shift();
      const neighborsData = getNeighborsData([...getLocation(tileElement)], board);
      const minesCount = neighborsData.filter(x => x.isMine).length;
      revealIfNotMarked(tileElement, board, () => {
        revealNumber(tileElement, board, minesCount);
        
        if (!minesCount) {
          tilesToReveal = [...new Set([...tilesToReveal, 
                                        ...getNeighborsElements(neighborsData.filter(x => !x.isRevealed),
                                                                board)])];
        }
      });  
    }
  }

  const revealNumber = (tile, board, minesCount) => {
    const numbersClasses = ['zero', 'number-one', 'number-two', 'number-three', 'number-four', 'number-five', 'number-six', 'number-seven', 'number-eight'];
    const [row, col] = getLocation(tile);

    tile.classList.add(numbersClasses[minesCount]);
    tile.classList.remove('unrevealed-tile');
    board[row][col].isRevealed = true;
    rebindClicksAsRevealedTile(tile, board)
  }

  const rebindClicksAsRevealedTile = (tile, board) => {
    tile.onclick = null;
    tile.oncontextmenu = null;
    tile.ondblclick = () => onDoubleClick(tile, board);
  }

  const onDoubleClick = (tile, board) => {
    const neighborsData = getNeighborsData(getLocation(tile), board);

    if (isNeighborFlagsAsMineCount(neighborsData)) {
      const minedTileData = neighborsData.filter(x => x.isMine && !x.isMarked()).shift();

      if (minedTileData) {
        onMineReveal(board); 
      }
      else {
        getNeighborsElements(neighborsData, board).forEach(x => revealTile(x, board));
      }
    }
  }

  const isNeighborFlagsAsMineCount = neighborsData => {
    const minesCount = neighborsData.filter(x => x.isMine).length;

    return neighborsData.filter(x => x.currentMarkState() === 'flagged-tile').length === minesCount;
}

  const getNeighborsElements = (neighborsData, board) =>
  neighborsData.reduce((neighborsElements, neighborData) => 
    [...neighborsElements, getTileElement(...getLocationByData(neighborData, board))], []);

  const getLocationByData = (tileData, board) => {
    const rowIndex = board.findIndex(row => row.includes(tileData));
    const colIndex = board[rowIndex].indexOf(tileData);

    return [rowIndex, colIndex];
  }

  const getTileElement = (row, col) => {
    const boardElement = document.getElementsByClassName('game-board')[0];

    return boardElement.children[row].children[col];
  }

  const onMineReveal = board => {
      revealAllMines(board);
      displayLoseMessage();
  }

  const revealAllMines = board => {
    const mineClass = 'mined-tile';
    const minedTiles = board.flat().filter(x => x.isMine);
    const mineElements = minedTiles.map(tile => getLocationByData(tile, board))
                                   .map(location => getTileElement(...location));
    mineElements.forEach(x => {
      x.classList.add(mineClass);
      x.classList.remove('unrevealed-tile');
    })
  }

  const displayLoseMessage = () => {
    const loseCoverElement = document.getElementsByClassName('lose-cover')[0];
    document.getElementsByClassName('game-board')[0].classList.add('disabled')
    loseCoverElement.style.display = 'block';
    
    setTimeout(() => {
      loseCoverElement.style.transform = 'scale(1.5)';
      loseCoverElement.style.opacity = '0.7';
    }, 1000);
  }  

  const getNeighborsData = ([row, col], board) => {
    return board.slice(row === 0 ? 0 : row-1, row+2).reduce((neighbors, currentRow) => 
            [...neighbors, ...currentRow.slice(col === 0 ? 0 : col-1, col+2)], [])
            .filter(x => x !== board[row][col]);
  }

  let board = initializeBoard();
  assignMinesOnBoard(board, generateMinesLocations(settings.minesCount));
  // Add ('draw') the board (all tiles) in the html?
  // Bind click events!!!

  // binding right click
  [...document.getElementsByClassName('tile')].forEach(tile => tile.oncontextmenu = event => {
    event.preventDefault();
    onRightClick(event.target, board);
  });

  // binding left click
  [...document.getElementsByClassName('tile')].forEach(tile => tile.onclick = event => {
    revealTile(event.target, board);
  });

  // bind revealMine to the mined locations
  [...document.getElementsByClassName('tile')].filter(tile => {
    const [row, col] = getLocation(tile);

    return board[row][col].isMine;
  }).forEach(minedTile => minedTile.onclick = event => {
    revealIfNotMarked(event.target, board, () => onMineReveal(board));
  });

  // Bind click events!!!
}