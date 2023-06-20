import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { ListGalleriesComponent } from "./components/list-galleries/list-galleries.component";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";

import { MatLegacyCardModule as MatCardModule } from "@angular/material/legacy-card";
import { MatLegacyButtonModule as MatButtonModule } from "@angular/material/legacy-button";
import { MatLegacyProgressBarModule as MatProgressBarModule } from "@angular/material/legacy-progress-bar";

import { GalleryContentComponent } from "./components/gallery-content/gallery-content.component";
import { LazyLoadImageModule } from "ng-lazyload-image";
import { SanitizedContainerNamePipe } from "./pipes/sanitized-container-name.pipe";
import { CommonModule } from "@angular/common";

@NgModule({
  declarations: [
    AppComponent,
    ListGalleriesComponent,
    GalleryContentComponent,
    SanitizedContainerNamePipe,
  ],
  imports: [
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    MatCardModule,
    MatButtonModule,
    MatProgressBarModule,
    AppRoutingModule,
    LazyLoadImageModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
