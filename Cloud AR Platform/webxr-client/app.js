const path = require('path');
const express = require('express');
var favicon = require('serve-favicon');
const fs = require('fs');
const https = require('https');
const net = require('net');


//express
const app = express();
app.use('/src', express.static(path.join(__dirname, '/src')));
app.use('/libs', express.static(path.join(__dirname, '/libs')));
app.use('/dist', express.static(path.join(__dirname, '/dist')));
app.use(favicon(path.join(__dirname, 'static', 'images', 'favicon.ico')));
app.get('/', function (req, res) {
    res.sendFile(path.join(__dirname, 'index.html'))
});


//https server
const privateKey = fs.readFileSync('ssl/server.key', 'utf8');
const certificate = fs.readFileSync('ssl/server.crt', 'utf8');
const options = {key: privateKey, cert: certificate, requestCert: false, rejectUnauthorized: false};
const httpsServer = https.createServer(options, app);
httpsServer.listen(443, '10.0.0.218');


// websockets (socket.io)
const io = require('socket.io').listen(httpsServer);
let socket = null;
let stream = null;
io.sockets.on('connection', (s) => {
    console.info(`Client connected [id=${s.id}]`);
    s.on('log', function (message) {
        console.log(message);
    });
    s.on('error', function (message) {
        console.error(message);
    });
    s.on('echo', function (message) {
        console.log(message);
    });
    s.on('pose', function(pose) {
        if (stream) {
            stream.write(JSON.stringify(pose) + '\n');
        }
        // console.log(pose.position);
    });
    socket = s;
});


//named pipe
const PIPE_PATH = '\\\\.\\pipe\\MECAL-';
const imagePipeServer = net.createServer(function (s) {
    console.log("Image Pipe Server: Connected to Unity");
    s.on('end', function() {
        console.log('Server: on end');
        imagePipeServer.close();
    });
    s.on('data', function (data) {
        if (socket) {
            socket.emit('image', data);
        }
    });
});
const posePipeServer = net.createServer(function (s) {
    console.log("Pose Pipe Server: Connected to Unity");
    s.on('end', function() {
        console.log('Server: on end');
        posePipeServer.close();
    });
    stream = s;
});

imagePipeServer.listen(PIPE_PATH + 'Image', function () {
    console.log('Image Pipe Server: Listening');
});
posePipeServer.listen(PIPE_PATH + 'Pose', function () {
    console.log('Pose Pipe Server: Listening');
});

