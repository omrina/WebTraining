import { restart } from '../game-logic/game.js';
import { easyMode, mediumMode, hardMode, unmarkedClass, flaggedClass, questionMarkingClass } from './configs.js';

export let selectedModeSettings = easyMode;
export const markingClasses = { unmarkedClass, flaggedClass, questionMarkingClass };

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
    markingClasses.questionMarkingClass = questionMarkingClass;
  } else {
    delete markingClasses.questionMarkingClass;
  }

  restart();
};

bindSidebarToggeling();
bindModeButtons();
bindQuestionMarkSwitch();
