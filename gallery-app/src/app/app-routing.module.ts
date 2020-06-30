import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ListGalleriesComponent } from './components/list-galleries/list-galleries.component';
import { GalleryContentComponent } from './components/gallery-content/gallery-content.component';

const routes: Routes = [
  {
    path: '',
    component: ListGalleriesComponent
  },
  {
    path: ':name',
    component: GalleryContentComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {useHash: true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
