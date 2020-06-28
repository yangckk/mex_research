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
const privateKey = fs.readFileSync('ssl/privkey.pem', 'utf8');
const certificate = fs.readFileSync('ssl/cert.pem', 'utf8');
const options = {key: privateKey, cert: certificate, requestCert: false, rejectUnauthorized: false};
const httpsServer = https.createServer(options, app);
httpsServer.listen(443, '164.67.195.73');
