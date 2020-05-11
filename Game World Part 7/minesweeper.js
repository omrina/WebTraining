import { settings } from './gameSettings.js' ;
{
  const initializeBoard = () => {
    const [isRevealed, isMine, markingStates] = [false, false, [...settings.markingStateClasses]];

    return Array(settings.rowsCount).fill().map(() =>
              Array(settings.columnsCount).fill().map(() =>
                 Object.assign({}, { isRevealed,
                                     isMine,
                                     markingStates,
                                     currentMarkState () { return markingStates[0]; },
                                     isMarked () { return currentMarkState() !== ''; }
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
    const tileLocation = getLocation(tile);
    const {currentMarkState, markingStates} = { ...board[tileLocation[0]][tileLocation[1]] };

    tile.classList.remove(currentMarkState())
    markingStates.push(markingStates.shift())
    tile.classList.add(currentMarkState())
  }

  let board = initializeBoard();
  // Add ('draw') the board (all tiles) in the html?
  // Bind click events!!!
  [...document.getElementsByClassName('tile')].forEach(tile => tile.oncontextmenu = event => {
    event.preventDefault();
    onRightClick(event.target, board);
  });
  // Bind click events!!!
}