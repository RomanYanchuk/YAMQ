import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Track } from './track';
import { PostPlaylist } from './post-playlist';
import { Playlist } from './playlist';

@Injectable({
  providedIn: 'root',
})
export class PlaylistsService {
  constructor(private http: HttpClient) {}

  public getPosts(): Observable<object> {
    return this.http.get<object>(environment.serviceApi + 'playlists');
  }

  public findTracksByName(name: string): Observable<Track[]> {
    return this.http.get<Track[]>(environment.serviceApi + 'tracks/search', {
      params: { searchFilter: name },
    });
  }

  public create(playlist: PostPlaylist): Observable<string> {
    return this.http.post<string>(
      environment.serviceApi + 'playlists',
      playlist
    );
  }

  public get(id: string): Observable<Playlist> {
    return this.http.get<Playlist>(environment.serviceApi + 'playlists/' + id);
  }
}
