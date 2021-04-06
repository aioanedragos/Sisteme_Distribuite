#!/usr/bin/env node
const axios = require('axios')

var WebSocketServer = require('websocket').server;
var http = require('http');

var server = http.createServer(function(request, response) {
    console.log((new Date()) + ' Received request for ' + request.url);
    response.writeHead(404);
    response.end();
});
server.listen(8080, function() {
    console.log((new Date()) + ' Server is listening on port 8080');
});

wsServer = new WebSocketServer({
    httpServer: server,
    // You should not use autoAcceptConnections for production
    // applications, as it defeats all standard cross-origin protection
    // facilities built into the protocol and the browser.  You should
    // *always* verify the connection's origin and decide whether or not
    // to accept it.
    autoAcceptConnections: false
});

function originIsAllowed(origin) {
  // put logic here to detect whether the specified origin is allowed.
  return true;
}

wsServer.on('request', function(request) {
    if (!originIsAllowed(request.origin)) {
      // Make sure we only accept requests from an allowed origin
      request.reject();
      console.log((new Date()) + ' Connection from origin ' + request.origin + ' rejected.');
      return;
    }
    
    var connection = request.accept(request.origin);
    console.log((new Date()) + ' Connection accepted.');
    connection.on('message', function(message) {
        if (message.type === 'utf8') {
            

            var response = JSON.parse(message.utf8Data);

            if(response.option == 1) {
                var name = response.name;
                var number = response.number;
                // console.log(name)
                axios.get("https://us-central1-igneous-impulse-280111.cloudfunctions.net/insert?name=" + name + "-" + number)
                .then(res=> {
                    console.log(res.data);
                    connection.sendUTF("Inserted the record succesfully");
                }).catch(error => console.log(error));
            } else if(response.option == 2) {

                var name = response.name;

                axios.get("https://us-central1-igneous-impulse-280111.cloudfunctions.net/get_phone_number?name=" + name)
                .then(res=> {
                    console.log(res.data);
                    connection.sendUTF("The number for " + name + " is " + res.data);
                }).catch(error => console.log(error));
            }

            

        }
        else if (message.type === 'binary') {
            console.log('Received Binary Message of ' + message.binaryData.length + ' bytes');
            connection.sendBytes(message.binaryData);
        }
    });
    connection.on('close', function(reasonCode, description) {
        console.log((new Date()) + ' Peer ' + connection.remoteAddress + ' disconnected.');
    });
});