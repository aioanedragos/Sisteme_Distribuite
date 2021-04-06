#!/usr/bin/env node
var WebSocketClient = require('websocket').client;

// const readline = require("readline");
// const rl = readline.createInterface({
//     input: process.stdin,
//     output: process.stdout
// });

const readline = require('readline-sync');

var client = new WebSocketClient();


client.on('connectFailed', function(error) {
    console.log('Connect Error: ' + error.toString());
});



client.on('connect', function(connection) {
    console.log('WebSocket Client Connected');
    connection.on('error', function(error) {
        console.log("Connection Error: " + error.toString());
    });
    connection.on('close', function() {
        console.log('echo-protocol Connection Closed');
    });
    connection.on('message', function(message) {
        if (message.type === 'utf8') {
            console.log("Received: '" + message.utf8Data + "'");
        }
    });
    
    function sendNumber() {
        if (connection.connected) {

            let option = readline.question("1: insert name+number\n2: get number by name.\n>");
            if(option == "1") {
                let name = readline.question("What is your name?");
                let number = readline.question("What is your number?");

                console.log("Hi " + name + ", nice to meet you.");
                
                connection.sendUTF(JSON.stringify({
                    name: name,
                    number: number,
                    option: 1
                }));
            } else if(option == "2") {
                let name = readline.question("What is the name for which you want the number?");

                console.log("Searching number for " + name + " ...");

                connection.sendUTF(JSON.stringify({
                    name: name,
                    option: 2
                }));
            }
            setTimeout(sendNumber, 1000);
        }
    }
    sendNumber();
});

client.connect('ws://34.116.172.9:80');