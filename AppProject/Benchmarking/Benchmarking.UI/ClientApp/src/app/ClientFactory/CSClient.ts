import { IClient } from "../Contracts/IClient"
import { ExternalCssJsLoader } from "./ExternalCssJsLoader";
export class CSclient implements IClient {
  _ResoucesUrl: string;
  constructor(resourceUrl: string) {
    this._ResoucesUrl = resourceUrl;
  }
  LoadCSS(pageName: string) {
    switch (pageName) {
      case 'login':
        let urls: string[] = [];
        urls.push(this._ResoucesUrl+"\Content\CS\default.css");
        ExternalCssJsLoader.load(urls);
    }
  }
}
