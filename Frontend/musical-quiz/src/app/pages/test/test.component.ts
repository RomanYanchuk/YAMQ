import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { PlaylistsService } from 'src/app/modules/playlists/playlists.service';
@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.scss'],
})
export class TestComponent implements OnInit {
  constructor(private playlistsService: PlaylistsService) {}
  public playlist: any;

  ngOnInit(): void {
    this.retrievePlaylist();
  }

  private retrievePlaylist(): void {
    this.playlistsService.getPosts().subscribe((response) => {
      console.log(response);
      this.playlist = response;
    });
  }

  @ViewChild('audioOption') audioPlayerRef: ElementRef;

  onAudioPlay(track: any) {
    let audio = new Audio();
    audio.src = track.previewUrl;
    audio.load();
    audio.play();
  }
}
