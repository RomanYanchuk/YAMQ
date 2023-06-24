import {
  MissingTranslationHandler,
  MissingTranslationHandlerParams,
} from '@ngx-translate/core';

export class MissingTranslationService implements MissingTranslationHandler {
  handle(params: MissingTranslationHandlerParams) {
    console.warn(
      `WARN: '${params.key}' is missing in '${params.translateService.currentLang}' locale`
    );
    return params.key;
  }
}
