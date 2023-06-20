import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { GalleryContainer } from "../../models/gallery-container.model";
import { BlobService } from "../../services/blob.service";

@Component({
  selector: "app-list-galleries",
  templateUrl: "./list-galleries.component.html",
  styleUrls: ["./list-galleries.component.scss"],
})
export class ListGalleriesComponent implements OnInit {
  containerNames = [{ name: "dummy" } as GalleryContainer];
  errorOccurred = false;
  errorMessage: string;
  loading: boolean;
  constructor(private blobService: BlobService, private router: Router) {}

  ngOnInit() {
    this.blobService.error$.subscribe((res) => {
      this.displayError(res);
    });
    this.loading = true;
    this.blobService.listContainer().then((res) => {
      this.containerNames = res.map((r) => new GalleryContainer(r));
      this.loading = false;
    });
  }

  openContainer(containerName: string) {
    this.router.navigateByUrl(containerName);
  }

  private displayError(error: string) {
    this.errorOccurred = true;
    this.errorMessage = error;
  }
}
