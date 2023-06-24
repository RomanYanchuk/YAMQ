import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Track } from 'src/app/modules/playlists/track';

@Component({
  selector: 'app-track-info-tile',
  templateUrl: './track-info-tile.component.html',
  styleUrls: ['./track-info-tile.component.scss'],
})
export class TrackInfoTileComponent implements OnInit {
  @Input() public track: Track;
  @Input() public doesTrackAddedToList: boolean;
  @Output() onAudioPlay = new EventEmitter();
  @Output() addToTracks = new EventEmitter();
  @Output() deleteTrack = new EventEmitter();

  constructor() {}

  ngOnInit(): void {}
}
