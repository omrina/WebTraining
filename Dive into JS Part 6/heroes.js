import heroesData from './ow.json';

const getHeroes = () => {
  const { names, roles, hp } = heroesData;

  return names.reduce((heroes, name, index) => {
    heroes.push({ name, role: roles[index], hp: hp[index] });

    return heroes;
  }, []);
};

const groupBy = (heroes, category = 'role') =>
  heroes.reduce((groupedHeroes, hero) => {
    (groupedHeroes[hero[category]] = groupedHeroes[hero[category]] || []).push(hero);

    return groupedHeroes;
  }, {});

const getByRoles = (heroes, ...roles) =>
  heroes.filter(hero => roles.includes(hero.role));

const makeHeroesNice = heroes =>
  heroes.map(hero =>
    ({ ...hero, sayHello: () => { console.log(`Hi! My name is ${hero.name}, nice to meet you!`); } })
  );

const heroes = getHeroes();
const niceHeroes = makeHeroesNice(heroes);

console.log('Here are all the heroes:');
console.table(heroes);
console.log('\nThe heroes grouped by default (role):');
console.log(groupBy(heroes));
console.log('\nThe heroes grouped by hp:');
console.log(groupBy(heroes, 'hp'));
console.log('\nThe heroes with roles of offense or tank:');
console.table(getByRoles(heroes, 'Offense', 'Tank'));
console.log('\nThe second hero says hello:');
niceHeroes[1].sayHello();