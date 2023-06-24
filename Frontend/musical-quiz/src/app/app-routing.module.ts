import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TestComponent } from './pages/test/test.component';
import { CreatePlaylistComponent } from './pages/create-playlist/create-playlist.component';
import { GamePlayComponent } from './components/game-play/game-play.component';

const routes: Routes = [
  {
    path: 'test',
    component: TestComponent,
  },
  {
    path: 'playlists/create',
    component: CreatePlaylistComponent,
  },
  {
    path: 'playlists/:id/play',
    component: GamePlayComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
