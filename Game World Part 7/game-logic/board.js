import { Tile } from './tile.js'
import { Location } from './location.js'

export class Board {
    constructor (rowsCount, colsCount, minesCount) {
        this.rowsCount = rowsCount;
        this.colsCount = colsCount;
        this.minesCount = minesCount;
        this.flaggedTilesCount = 0;
        this.element = document.getElementById('game-board');
        this.data = this.initializeBoard(rowsCount, colsCount);
        this.bindFirstTileClick();
    }
    initializeBoard = (rowsCount, colsCount) => {
        const board = [];

        for (let row = 0; row < rowsCount; row++) {
            const rowElement = this.createRowElement(colsCount);
            this.element.appendChild(rowElement);
            board[row] = [];

            for (let col = 0; col < colsCount; col++) {
                board[row][col] = new Tile(this, rowElement.children[col]);         
            }
        }

        return board;
    }
    createRowElement = tileCount => {
        const rowElement = document.createElement('div');

        rowElement.classList.add('row');

        for (let i = 0; i < tileCount; i++) {
            rowElement.appendChild(this.createTileElement())
        }

        return rowElement;
    }
    createTileElement = () => {
        const tileElement = document.createElement('div');

        tileElement.classList.add('tile');
        tileElement.classList.add('unrevealed-tile');

        return tileElement;
    }
    bindFirstTileClick = () => {
        this.element.addEventListener('click', event => {
            const tiles = this.data.flat();
            const tilesElements = tiles.map(({ element }) => element);

            if (tilesElements.includes(event.target)){
                const clickedTile = tiles.find(tile => event.target === tile.element);
                
                this.assignMinesToBoard(this.getLocation(clickedTile));
            }
            else {
                this.bindFirstTileClick();
            }
        }, { once: true , capture: true});
    }
    getLocation = tile => {
        const rowIndex = this.data.findIndex(row => row.includes(tile));
        const colIndex = this.data[rowIndex].indexOf(tile);
        
        return new Location(rowIndex, colIndex);
    }
    assignMinesToBoard = firstClickedLocation => {
        const minesLocations = this.generateMinesLocations(firstClickedLocation);

        minesLocations.forEach(location => {
            this.data[location.row][location.col].isMine = true;
        });
    }
    generateMinesLocations = firstClickedLocation => {
      const locations = [];
    
      while (locations.length < this.minesCount) {
        const newLocation = new Location (Math.floor(Math.random() * this.rowsCount),
                                          Math.floor(Math.random() * this.colsCount));
    
        if (!(this.areLocationsTheSame(newLocation, firstClickedLocation)) &&
            !(locations.some(x => this.areLocationsTheSame(x, newLocation)))) {
          locations.push(newLocation);
        }
      }
    
      return locations;
    }
    areLocationsTheSame = ({ row: firstRow, col: firstCol }, { row: secondRow, col: secondCol }) => {
      return firstRow === secondRow && firstCol === secondCol;
    }
    getNeighborTiles = tile => {
        const {row, col} = this.getLocation(tile);
        const [rowStart, rowEnd] = [row === 0 ? 0 : row - 1, row + 2];
        const [colStart, colEnd] = [col === 0 ? 0 : col - 1, col + 2]

        return this.data.slice(rowStart, rowEnd)
                         .reduce((neighbors, currentRow) => {
                             return [...neighbors, ...currentRow.slice(colStart, colEnd)];
                         }, [])
                         .filter(x => x !== tile);
    }
    isGameWon = () => {
        const allTiles = this.data.flat();
        const revealedTilesCount = allTiles.filter(({ isRevealed }) => isRevealed).length;
        
        return allTiles.length - revealedTilesCount === this.minesCount;
    }
    isNeighborFlagsMatchTileNumber = tile => {
        const neighborsTiles = this.getNeighborTiles(tile);
        const minesCount = neighborsTiles.filter(({ isMine }) => isMine).length;
        const flaggedNeighborsCount = neighborsTiles.filter(({ isFlagged }) => isFlagged()).length;

        return flaggedNeighborsCount === minesCount;
    }
    revealAllMines = () => {
        const mineElements = this.data.flat().filter(({ isMine }) => isMine)
                                              .map(({ element }) => element);

        mineElements.forEach(x => {
            x.classList.remove('unrevealed-tile');
            x.classList.add('mined-tile');
        });
    }
}