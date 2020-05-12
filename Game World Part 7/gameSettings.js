import { onRestart } from './gameBar/gameBar.js';

const easyMode = { rowsCount: 9, columnsCount: 9, minesCount: 10 };
const mediumMode = { rowsCount: 16, columnsCount: 16, minesCount: 40 };
const hardMode = { rowsCount: 16, columnsCount: 30, minesCount: 99 };
const availableMarkingClasses = ['unmarked', 'flagged-tile', 'questioned-tile'];
const [unmarkedClass, flaggedClass, questionMarkingClass] = [...availableMarkingClasses];
export let settings = {...easyMode, availableMarkingClasses, flaggedClass, unmarkedClass};

const bindSidebarToggeling = () => {
    const sidebar = document.getElementsByClassName('settings-sidebar')[0];
    const gameContainer = document.getElementsByClassName('game-container')[0];
    document.getElementsByClassName('settings-button')[0].onclick =
        () => openSidebar(gameContainer, sidebar);
    document.getElementsByClassName('close-settings-button')[0].onclick =
        () => closeSidebar(gameContainer, sidebar);
}

const openSidebar = (gameContainer, sidebar) => {
    const openTransformValue = 0;

    if (!gameContainer.classList.contains('disabled')) {
        toggleSidebar(sidebar, openTransformValue);
        gameContainer.classList.add('disabled');
    }
}

const closeSidebar = (gameContainer, sidebar) => {
    const closeTransformValue = 100;
    toggleSidebar(sidebar, closeTransformValue);
    gameContainer.classList.remove('disabled');
}

const toggleSidebar = (sidebar, transformValue) => {
    sidebar.style.transform = `translateX(${transformValue}%)`;
}

const bindModeButtons = () => {
    const modeButtons = [...document.getElementsByClassName('mode-button')];
    const easyButton = modeButtons[modeButtons.findIndex(x => x.classList.contains('easy-button'))];
    const mediumButton = modeButtons[modeButtons.findIndex(x => x.classList.contains('medium-button'))];
    const hardButton = modeButtons[modeButtons.findIndex(x => x.classList.contains('hard-button'))];
    easyButton.onclick = () => changeMode(easyButton, easyMode, modeButtons);
    mediumButton.onclick = () => changeMode(mediumButton, mediumMode, modeButtons);
    hardButton.onclick = () => changeMode(hardButton, hardMode, modeButtons);
}

const changeMode = (modeButton, modeSettings, modeButtons) => {
    const activeModeClass = 'active-mode';
     
    if (!modeButton.classList.contains(activeModeClass)){
        modeButtons.forEach(x => x.classList.remove(activeModeClass));
        modeButton.classList.add(activeModeClass);
        settings = {...settings, ...modeSettings};
        onRestart(settings);
    }
}

bindSidebarToggeling();
bindModeButtons();