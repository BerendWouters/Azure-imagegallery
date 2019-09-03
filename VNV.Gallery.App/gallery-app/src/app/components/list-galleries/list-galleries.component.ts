import { Component, OnInit } from '@angular/core';
import { BlobService as BlobService } from 'src/app/services/blob.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-list-galleries',
  templateUrl: './list-galleries.component.html',
  styleUrls: ['./list-galleries.component.scss']
})
export class ListGalleriesComponent implements OnInit {

  containerNames = [];
  constructor(private blobService: BlobService, private router: Router) { }

  ngOnInit() {
    this.blobService.listContainer().then( (res) =>

    {
      this.containerNames = res.map(r => new GalleryContainer(r));
    }
    );
  }

  openContainer(containerName: string){
    this.router.navigateByUrl(containerName);
    this.blobService.getBlobs(containerName).then((res) => console.log(res));
  }

}

export class GalleryContainer{
  constructor(name: string) {
    this.name = name;
  }
  name: string;
}
