"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var EVSClient_1 = require("./EVSClient");
var CSClient_1 = require("./CSClient");
var DBClient_1 = require("./DBClient");
var Clients_1 = require("../Enums/Clients");
var environment_1 = require("../../environments/environment");
function getClientInstance(type) {
    var resourceBaseUrl;
    if (environment_1.environment.production) {
        resourceBaseUrl = document.getElementsByTagName('base')[0].href + "Benchmarking/";
    }
    else {
        //need to be worked on
        resourceBaseUrl = '/src/';
    }
    console.log(resourceBaseUrl);
    var _client;
    switch (type.toString()) {
        case 'CS':
            _client = new CSClient_1.CSclient(resourceBaseUrl);
            break;
        case 'DB':
            _client = new DBClient_1.DBclient(resourceBaseUrl);
            break;
        case 'EVS':
            _client = new EVSClient_1.EVSclient(resourceBaseUrl);
            break;
        default:
            _client = new EVSClient_1.EVSclient(resourceBaseUrl);
            break;
    }
    return _client;
}
exports.client = getClientInstance(Clients_1.Clients.EVS);
//# sourceMappingURL=ClientFactory.js.map