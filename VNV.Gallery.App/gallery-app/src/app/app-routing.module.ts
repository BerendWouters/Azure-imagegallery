import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ListGalleriesComponent } from './components/list-galleries/list-galleries.component';

const routes: Routes = [
  {
    path: '',
    component: ListGalleriesComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
