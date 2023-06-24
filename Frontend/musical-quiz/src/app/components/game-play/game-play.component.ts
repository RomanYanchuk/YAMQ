import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Playlist } from 'src/app/modules/playlists/playlist';
import { PlaylistsService } from 'src/app/modules/playlists/playlists.service';

@Component({
  selector: 'app-game-play',
  templateUrl: './game-play.component.html',
  styleUrls: ['./game-play.component.scss'],
})
export class GamePlayComponent implements OnInit {
  public id: string;
  public state: number = 0;
  public playlist: Playlist;
  public interval: NodeJS.Timer;
  public time: number;
  public timeLeft: number;
  public currentTrackIndex: number = 0;
  public answer = '';
  public showHint: boolean = false;
  public audioPlayer: HTMLAudioElement;
  public numberOfRights = 0;
  public numberOfWrongs = 0;
  public answeredRight = false;
  public isTimeout = false;
  private onTimerIncrease: () => void = undefined;
  private onTimerEnd: () => void = undefined;

  constructor(
    private activatedRoute: ActivatedRoute,
    private playlistsService: PlaylistsService
  ) {}

  ngOnInit(): void {
    this.id = this.activatedRoute.snapshot.params['id'];
    if (this.id) {
      this.playlistsService.get(this.id).subscribe((response) => {
        this.playlist = response;
        this.changeState();
      });
    }
  }

  public resetAndRestart() {
    this.state = 1;
    this.currentTrackIndex = 0;
    this.answer = '';
    this.numberOfRights = 0;
    this.numberOfWrongs = 0;
    this.answeredRight = false;
    this.isTimeout = false;
    this.onTimerIncrease = undefined;
    this.onTimerEnd = undefined;
  }

  public changeState() {
    if (this.state === 1) {
      this.onTimerEnd = this.changeState;
      this.startTimer(5);
    }
    if (this.state === 2) {
      this.initializeGame();
    }
    if (this.state === 3) {
    }
    this.state += 1;
  }

  public initializeGame() {
    this.currentTrackIndex = -1;
    this.numberOfRights = 0;
    this.numberOfWrongs = 0;
    this.playNextTrack();
  }

  public onAnswerChange() {
    if (
      this.answer.toLowerCase() ===
      this.playlist.tracks[this.currentTrackIndex].name.toLowerCase()
    ) {
      this.answeredRight = true;
      this.numberOfRights += 1;
      this.pauseTimer();
      this.showHint = true;
      setTimeout(() => this.playNextTrack(), 5000);
    }
  }

  public playNextTrack() {
    this.answer = '';
    this.answeredRight = false;
    this.showHint = false;
    this.isTimeout = false;
    this.audioPlayer?.pause();
    if (this.currentTrackIndex + 1 < this.playlist.tracks.length) {
      this.currentTrackIndex += 1;
      this.onAudioPlay(this.playlist.tracks[this.currentTrackIndex]);
      this.onTimerIncrease = this.showHintIfAvailable;
      this.onTimerEnd = this.onTimeout;
      this.startTimer(30);
    } else {
      this.changeState();
    }
  }

  private showHintIfAvailable() {
    if (this.timeLeft === 10) {
      this.showHint = true;
    }
  }

  private onAudioPlay(track: any) {
    if (this.audioPlayer) {
      this.audioPlayer.pause();
    }
    this.audioPlayer = new Audio();
    this.audioPlayer.src = track.previewUrl;
    this.audioPlayer.load();
    this.audioPlayer.play();
  }

  private onTimeout() {
    this.isTimeout = true;
    this.numberOfWrongs += 1;
    this.pauseTimer();
    setTimeout(() => this.playNextTrack(), 5000);
  }

  public startTimer = (time: number) => {
    this.time = time;
    this.timeLeft = time;
    clearInterval(this.interval);
    setTimeout(() => (this.interval = setInterval(this.ticker, 1000)), 500);
  };

  private ticker = () => {
    if (this.timeLeft > 0) {
      if (this.onTimerIncrease) {
        this.onTimerIncrease();
      }
      this.timeLeft--;
    } else {
      if (this.onTimerEnd) {
        this.onTimerEnd();
      }
      this.pauseTimer();
    }
  };

  private pauseTimer() {
    clearInterval(this.interval);
  }

  public getTimerPercent() {
    return (this.timeLeft / this.time) * 100;
  }
}
