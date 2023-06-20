import { Injectable } from "@angular/core";
import {
  BlobServiceClient,
  ServiceListContainersOptions,
} from "@azure/storage-blob";
import { Image } from "./image";
import { Subject } from "rxjs";
@Injectable({
  providedIn: "root",
})
export class BlobService {
  private blobServiceClient: BlobServiceClient;

  account = "storeparkings";
  // tslint:disable-next-line: max-line-length
  accountSas = "";

  private errorSubject = new Subject<string>();
  error$ = this.errorSubject.asObservable();
  constructor() {
    this.blobServiceClient = new BlobServiceClient(
      `https://${this.account}.blob.core.windows.net${this.accountSas}`
    );
  }

  async listContainer() {
    const containerNames = [];
    const listContainerOptions: ServiceListContainersOptions = {
      prefix: "gallery-",
    };
    for await (const container of this.blobServiceClient.listContainers(
      listContainerOptions
    )) {
      containerNames.push(container.name);
    }
    return containerNames.sort();
  }
  async getBlobs(containerName: string): Promise<Image[]> {
    const containerClient =
      this.blobServiceClient.getContainerClient(containerName);
    var items = [];
    for await (const blob of containerClient.listBlobsFlat()) {
      items.push(<Image>{ name: blob.name });
    }
    return items;
  }
}
