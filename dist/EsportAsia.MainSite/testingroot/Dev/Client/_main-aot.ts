import './polyfills.ts';

import { platformBrowser } from '@angular/platform-browser';
import { enableProdMode } from '@angular/core';
import { environment } from './environments/environment';
import { RootModuleNgFactory } from './aot/app/bootstrap/root.module.ngfactory';

if (environment.production) {
  enableProdMode();
}

platformBrowser().bootstrapModuleFactory(RootModuleNgFactory);
