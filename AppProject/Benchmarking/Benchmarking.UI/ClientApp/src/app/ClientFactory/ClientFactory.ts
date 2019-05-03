import { EVSclient } from "./EVSClient";
import { CSclient } from "./CSClient";
import { DBclient } from "./DBClient";
import { Clients } from "../Enums/Clients";
import { IClient } from "../Contracts/IClient";
import { environment } from '../../environments/environment';


function getClientInstance(type: Clients): IClient {
  let resourceBaseUrl: string;
  if (environment.production) {
    resourceBaseUrl = document.getElementsByTagName('base')[0].href + "Benchmarking/";
  }
  else {
    //need to be worked on
    resourceBaseUrl = '/src/';
  }
  console.log(resourceBaseUrl);
  let _client: IClient;
  switch (type.toString()) {
    case 'CS':
      _client = new CSclient(resourceBaseUrl);
      break;
    case 'DB':
      _client = new DBclient(resourceBaseUrl);
      break;
      case 'EVS':
      _client = new EVSclient(resourceBaseUrl);
      break;
    default:
      _client = new EVSclient(resourceBaseUrl);
      break;
  }
  return _client;
}
export const client: IClient = getClientInstance(Clients.EVS);


