import { Component, OnInit } from '@angular/core';
import { BlobService as BlobService } from 'src/app/services/blob.service';

@Component({
  selector: 'app-list-galleries',
  templateUrl: './list-galleries.component.html',
  styleUrls: ['./list-galleries.component.scss']
})
export class ListGalleriesComponent implements OnInit {

  constructor(private blobService: BlobService) { }

  ngOnInit() {
    this.blobService.listContainer().then( () => console.log('loaded'));
  }

}
