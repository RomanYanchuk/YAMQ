import { OverlayContainer } from '@angular/cdk/overlay';
import { Component, HostBinding, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  @HostBinding('class') className = '';
  public isDarkModeEnabled = false;

  constructor(
    private overlay: OverlayContainer,
    translateService: TranslateService
  ) {
    if (localStorage.getItem('language')) {
      translateService.setDefaultLang(localStorage.getItem('language'));
      translateService.use(localStorage.getItem('language'));
    } else {
      const browserLang = translateService.getBrowserLang();
      translateService.use(
        browserLang.match(/uk|en/) ? browserLang : environment.defaultLocale
      );
    }
  }

  public toggleDarkMode() {
    this.isDarkModeEnabled = !this.isDarkModeEnabled;
    const darkClassName = 'darkMode';
    this.className = this.isDarkModeEnabled ? darkClassName : '';
    if (this.isDarkModeEnabled) {
      this.overlay.getContainerElement().classList.add(darkClassName);
    } else {
      this.overlay.getContainerElement().classList.remove(darkClassName);
    }
  }
}
