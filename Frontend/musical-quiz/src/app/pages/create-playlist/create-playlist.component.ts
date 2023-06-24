import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { PlaylistsService } from 'src/app/modules/playlists/playlists.service';
import { PostPlaylist } from 'src/app/modules/playlists/post-playlist';
import { Track } from 'src/app/modules/playlists/track';

@Component({
  selector: 'app-create-playlist',
  templateUrl: './create-playlist.component.html',
  styleUrls: ['./create-playlist.component.scss'],
})
export class CreatePlaylistComponent implements OnInit {
  public searchValue = '';
  public searchResultList: Track[] = [];
  public tracks: Track[] = [];
  public audioPlayer: HTMLAudioElement;

  firstFormGroup = this.formBuilder.group({
    name: ['', Validators.required],
    description: [''],
  });

  public get name() {
    return this.firstFormGroup.get('name');
  }

  public get description() {
    return this.firstFormGroup.get('description');
  }

  constructor(
    private formBuilder: FormBuilder,
    public playlistsService: PlaylistsService
  ) {}

  ngOnInit(): void {}

  public search() {
    if (this.searchValue) {
      this.playlistsService.findTracksByName(this.searchValue).subscribe(
        (result) => {
          console.log(result);
          this.searchResultList = result;
        },
        () => {
          /*TODO: To handle errors*/
        }
      );
    }
  }

  public addToTracks(track: Track) {
    this.tracks.push(track);
  }

  public deleteTrack(track: Track) {
    const index = this.tracks.indexOf(track);
    if (index !== -1) {
      this.tracks.splice(index, 1);
    }
  }

  public doesTrackAddedToList = (trackId: string) =>
    this.tracks.some((t) => t.id === trackId);

  public onAudioPlay(track: any) {
    if (this.audioPlayer) {
      this.audioPlayer.pause();
    }
    this.audioPlayer = new Audio();
    this.audioPlayer.src = track.previewUrl;
    this.audioPlayer.load();
    this.audioPlayer.play();
  }

  public create() {
    const playlist = new PostPlaylist(
      this.name.value,
      this.description.value,
      this.tracks.map((t) => t.id)
    );
    this.playlistsService.create(playlist).subscribe((response) => {
      console.log(response),
        (error) => {
          console.error(error);
        };
    });
  }
}
