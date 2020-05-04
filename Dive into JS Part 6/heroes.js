function getHeroes() {
  const heroesData = jsonLoader.sync("ow.json");
  const names = heroesData.names;
  const roles = heroesData.roles;
  const hps = heroesData.hp;

  return names.reduce((heroes, name, index) => {
    heroes.push({
      name: name,
      role: roles[index],
      hp: hps[index],
    });

    return heroes;
  }, []);
}

function groupBy(heroes, category = roleCategory) {
  return heroes.reduce((groupedHeroes, hero) => {
    if (!groupedHeroes.hasOwnProperty(hero[category])) {
      groupedHeroes[hero[category]] = [];
    }

    groupedHeroes[hero[category]].push(hero);

    return groupedHeroes;
  }, {});
}

function getByRoles(heroes, ...roles) {
  return heroes.filter(hero => roles.includes(hero[roleCategory]));
}

const makeHeroesNice = (function () {
  function sayHello() {
    console.log(`Hi! My name is ${this.name}, nice to meet you!`);
  }

  return function (heroes) {
    heroes.forEach((hero) => {
      hero.sayHello = sayHello;
    });

    return heroes;
  };
})();

import jsonLoader from 'load-json-file';
const roleCategory = "role";
const heroes = getHeroes();

console.log("Here are all the heroes:");
console.table(heroes);
console.log("\nThe heroes grouped by default (role):");
console.log(groupBy(heroes));
console.log("\nThe heroes grouped by hp:");
console.log(groupBy(heroes, "hp"));
console.log("\nThe heroes with roles of offense or tank:");
console.table(getByRoles(heroes, "Offense", "Tank"));

console.log("\nThe second hero says hello:");
const niceHeroes = makeHeroesNice(heroes);
niceHeroes[1].sayHello();