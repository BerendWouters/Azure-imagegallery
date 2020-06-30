import { Injectable } from '@angular/core';
import { BlobServiceClient, ServiceListContainersOptions } from '@azure/storage-blob';
import { Image } from './image';
import { Subject } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class BlobService {
  private blobServiceClient: BlobServiceClient;

  account = 'storeparkings';
  // tslint:disable-next-line: max-line-length
 accountSas =
    '?sv=2018-03-28&ss=b&srt=sco&sp=rwdlac&se=2021-09-11T03:45:08Z&st=2019-09-10T19:45:08Z&spr=https&sig=gEGdyLWBjn2Ig%2FJ4EXz2%2FwZkNyIPxjIRbJ2a7ZmX1I4%3D';

  private errorSubject = new Subject<string>();
  error$ = this.errorSubject.asObservable();
  constructor() {
    this.blobServiceClient = new BlobServiceClient(
      `https://${this.account}.blob.core.windows.net${this.accountSas}`
    );
   }

   async listContainer() {

    const containerNames = [];
    const listContainerOptions: ServiceListContainersOptions ={
      prefix: 'gallery-'
    };
    for await (const container of this.blobServiceClient.listContainers(listContainerOptions)) {
      containerNames.push(container.name);
    }
    return containerNames.sort();
   }
   async getBlobs(containerName: string): Promise<Image[]>{
    const containerClient = this.blobServiceClient.getContainerClient(containerName);
    var items = [];
    for await(const blob of containerClient.listBlobsFlat()){
      items.push(<Image>{name: blob.name});
    };
    return items;
   }
}

