import { settings } from './gameSettings.js' ;
{
  const initializeBoard = () => {
    const [isRevealed, isMarked, isMine] = [false, false, false];

    return Array(settings.rowsCount).fill().map(() =>
     Array(settings.columnsCount).fill().map(() => Object.assign({}, { isRevealed, isMarked, isMine })));
  };

  const generateMinesLocations = minesAmount => {
    const locations = [];

    while (locations.length < minesAmount) {
        let newLocation = [Math.floor(Math.random() * settings.rowsCount), Math.floor(Math.random() * settings.columnsCount)]
        
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
}