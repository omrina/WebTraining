import { startGame } from '../gameLogic.js';
import { settings } from '../gameSettings.js';

export const updateFlagsDisplay = board => {
    const minesCount = board.flat().filter(x => x.isMine).length;
    const flaggedTilesCount = board.flat().filter(x => x.isFlagged()).length;

    setFlagsDisplay(minesCount, flaggedTilesCount);
}

const setFlagsDisplay = (minesCount, flaggedTilesCount = 0) => {
    document.getElementsByClassName('marked-flags')[0].textContent =
        `${flaggedTilesCount}/${minesCount}`;
}

const onRestart = settings => {
    resetBoardElement();
    hideGameOverMessage();
    setFlagsDisplay(settings.minesCount);
    startGame(settings);
}

const resetBoardElement = () => {
    const boardElement = document.getElementsByClassName('game-board')[0];
    boardElement.classList.remove('disabled');
    boardElement.textContent = '';
}

const hideGameOverMessage = () => {
    document.querySelectorAll("[class$=cover]")
        .forEach(coverElement => {
            coverElement.style.display = 'none';
            coverElement.style.transform = '';
            coverElement.style.opacity = '0';
        });
}

document.getElementsByClassName('restart-button')[0].onclick = () => onRestart(settings);
setFlagsDisplay(settings.minesCount);