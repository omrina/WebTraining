import { restart } from '../game-logic/game.js';
import { easyMode, mediumMode, hardMode } from './configs.js';

export let selectedModeSettings = easyMode;
export const tileStates = {
  REVEALED: 0,
  UNMARKED: 1,
  FLAGGED: 2,
  QUESTIONED: 3
};
export const markingClasses = {
  [tileStates.REVEALED]: 'revealed',
  [tileStates.UNMARKED]: 'unmarked',
  [tileStates.FLAGGED]: 'flagged-tile',
  [tileStates.QUESTIONED]: 'questioned-tile'
};

const bindSidebarToggeling = () => {
  document.getElementsByClassName('settings-button')[0].addEventListener('click', toggleSidebar);
  document.getElementsByClassName('close-settings-button')[0].addEventListener('click', toggleSidebar);
};

const toggleSidebar = () => {
  const sidebar = document.getElementsByClassName('settings-sidebar')[0];
  const gameContainer = document.getElementsByClassName('game-container')[0];

  gameContainer.classList.toggle('disabled');
  sidebar.classList.toggle('opened');
};

const bindModeButtons = () => {
  const modeButtons = [...document.getElementsByClassName('mode-button')];
  const [easyButton, mediumButton, hardButton] =
    [modeButtons.find(x => x.classList.contains('easy-button')),
    modeButtons.find(x => x.classList.contains('medium-button')),
    modeButtons.find(x => x.classList.contains('hard-button'))];

  easyButton.addEventListener('click', () => changeMode(easyButton, easyMode, modeButtons));
  mediumButton.addEventListener('click', () => changeMode(mediumButton, mediumMode, modeButtons));
  hardButton.addEventListener('click', () => changeMode(hardButton, hardMode, modeButtons));
};

const changeMode = (modeButton, modeSettings, modesButtons) => {
  const selectedModeClass = 'selected-mode';

  if (selectedModeSettings !== modeSettings) {
    modesButtons.forEach(x => { x.classList.remove(selectedModeClass); });
    modeButton.classList.add(selectedModeClass);
    selectedModeSettings = modeSettings;
    restart();
  }
};

const bindQuestionMarkSwitch = () => {
  document.getElementsByClassName('question-mark-checkbox')[0]
    .addEventListener('change', ({ target: checkboxElement }) => toggleQuestionMarking(checkboxElement));
};

const toggleQuestionMarking = checkboxElement => {
  if (checkboxElement.checked) {
    tileStates.QUESTIONED = Object.keys(tileStates).length;
  } else {
    delete tileStates.QUESTIONED;
  }

  restart();
};

bindSidebarToggeling();
bindModeButtons();
bindQuestionMarkSwitch();