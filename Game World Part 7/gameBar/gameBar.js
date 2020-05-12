import { startGame } from '../gameLogic.js';
import { settings } from '../gameSettings.js' ;

const onRestart = settings => {
    resetBoardElement();
    hideGameOverMessage();
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