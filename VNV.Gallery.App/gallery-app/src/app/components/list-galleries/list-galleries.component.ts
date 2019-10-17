import { Component, OnInit } from '@angular/core';
import { BlobService } from 'src/app/services/blob.service';
import { Router } from '@angular/router';
import { GalleryContainer } from '../../models/gallery-container.model';

@Component({
  selector: 'app-list-galleries',
  templateUrl: './list-galleries.component.html',
  styleUrls: ['./list-galleries.component.scss']
})
export class ListGalleriesComponent implements OnInit {
  containerNames = [];
  errorOccurred: boolean;
  errorMessage: string;
  constructor(private blobService: BlobService, private router: Router) {}

  ngOnInit() {
    this.blobService.error$.subscribe((res) => {
      this.displayError(res);
    })
    this.blobService.listContainer().then(res => {
      this.containerNames = res.map(r => new GalleryContainer(r));
    });
  }

  openContainer(containerName: string) {
    this.router.navigateByUrl(containerName);
    this.blobService.getBlobs(containerName).then(res => console.log(res));
  }

  private displayError(error: string){
    this.errorOccurred = true;
    this.errorMessage = error;
  }
}


