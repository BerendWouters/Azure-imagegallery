import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FlexLayoutModule} from '@angular/flex-layout';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ListGalleriesComponent } from './components/list-galleries/list-galleries.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatCardModule } from  '@angular/material/card';
import { MatButtonModule} from '@angular/material/button';

import { GalleryContentComponent } from './components/gallery-content/gallery-content.component';
import { LazyLoadImageModule } from 'ng-lazyload-image';
import { SanitizedContainerNamePipe } from './pipes/sanitized-container-name.pipe';

@NgModule({
  declarations: [
    AppComponent,
    ListGalleriesComponent,
    GalleryContentComponent,
    SanitizedContainerNamePipe
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FlexLayoutModule,
    MatCardModule,
    MatButtonModule,
    AppRoutingModule,
    LazyLoadImageModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
