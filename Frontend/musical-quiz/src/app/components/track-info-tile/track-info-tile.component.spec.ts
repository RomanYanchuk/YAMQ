import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TrackInfoTileComponent } from './track-info-tile.component';
import { TranslateModule } from '@ngx-translate/core';
import { Track } from 'src/app/modules/playlists/track';

describe('TrackInfoTileComponent', () => {
  let component: TrackInfoTileComponent;
  let fixture: ComponentFixture<TrackInfoTileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TrackInfoTileComponent],
      imports: [TranslateModule.forRoot()],
    }).compileComponents();

    fixture = TestBed.createComponent(TrackInfoTileComponent);
    component = fixture.componentInstance;
    component.track = new Track(
      'id',
      'name',
      'album',
      'author',
      'imageUrl',
      'previewUrl'
    );
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
