
export class ExternalCssJsLoader {
  public static load(urls: string[]) {
    if (urls) {
      for (var i = 0; i < urls.length; i++) {
        let element;
        let url = urls[i];
        if (url.endsWith(".js")) {
          element = document.createElement('script');
          element.src = url;
          element.type = 'text/javascript';
          element.async = true;
        }
        if (url.endsWith(".css")) {
          element = document.createElement('link');
          element.rel = "stylesheet";
          element.href = url;
        }
        document.getElementsByTagName('head')[0].appendChild(element);
      }
    }
  }
}
