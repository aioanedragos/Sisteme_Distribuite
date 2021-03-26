const moment = require('moment')
const fs = require('fs')

function generateRandomFromArray(array) {
  return array[Math.floor(Math.random() * array.length)]
}

function generateRandomInt(min, max) {
  return Math.floor((Math.random() * (max - min)) + min)
}

function generateRandomDate() {
  const maxNrOfDays = 365
  return moment().subtract(Math.floor(Math.random() * maxNrOfDays), 'day')
}

function pubToString(publication) {
  let str = '{'
  for (let prop of Object.keys(publication)) {
    if (typeof publication[prop] === 'string') {
      str += `(${prop},"${publication[prop]}");`
    }
    else if (moment.isMoment(publication[prop])) {
      str += `(${prop},${publication[prop].format('D.MM.YYYY')});`
    }
    else {
      str += `(${prop},${publication[prop]});`
    }
  }
  str = str.slice(0, -1) + '}'
  return str
}

fs.writeFile('publications.txt', '', function (err, data) {
  if (err) {
    console.log(err)
  }
})

const nrOfPublications = 50;
const citiesArray = ['Iasi', 'Bucuresti', 'Cluj', 'Arad', 'Timisoara', 'Galati']
const windDirectionsArray = ['N', 'NE', 'E', 'SE', 'S', 'SW', 'W', 'NW']
let publications = []
for (let i = 0; i < nrOfPublications; i++) {
  let newPub = {
    stationid: generateRandomInt(0, 25),
    city: generateRandomFromArray(citiesArray),
    temp: generateRandomInt(-10, 25),
    rain: Number(Math.random().toFixed(4)),
    wind: generateRandomInt(0, 20),
    direction: generateRandomFromArray(windDirectionsArray),
    date: generateRandomDate()
  }
  const strPub = pubToString(newPub)
  fs.appendFile('publications.txt', strPub + '\n', function (err, data) {
    if (err) {
      console.log(err)
    }
  })
}