import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewEncapsulation,
} from '@angular/core';
import { MatSelectChange } from '@angular/material/select';
import { TranslateService } from '@ngx-translate/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-top-menu',
  templateUrl: './top-menu.component.html',
  styleUrls: ['./top-menu.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class TopMenuComponent implements OnInit {
  public selectedLanguage: string;
  public languages: string[];
  @Input() public isDarkModeEnabled: boolean;
  @Output() toggleDarkMode = new EventEmitter();

  constructor(private translateService: TranslateService) {
    this.selectedLanguage = translateService.currentLang;
    this.languages = environment.locales;
  }
  ngOnInit() {}

  public onLanguageChanged(event: MatSelectChange) {
    this.translateService.use(event.value);
    localStorage.setItem('language', event.value);
    window.location.reload();
  }
}
