import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { BlobService } from "../../services/blob.service";

@Component({
  selector: "app-gallery-content",
  templateUrl: "./gallery-content.component.html",
  styleUrls: ["./gallery-content.component.scss"],
})
export class GalleryContentComponent implements OnInit {
  images = [];
  gallery: string;
  accountName: string;

  constructor(
    private route: ActivatedRoute,
    private blobService: BlobService
  ) {}

  ngOnInit() {
    const containerName = this.route.snapshot.paramMap.get("name");
    this.gallery = containerName;
    this.accountName = this.blobService.account;
    this.blobService.getBlobs(containerName).then((res) => {
      this.images = res;
    });
  }
  getImage(imageName: string): string {
    return `https://${this.accountName}.blob.core.windows.net/${this.gallery}/${imageName}`;
  }
}
