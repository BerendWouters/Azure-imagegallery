import { Injectable } from '@angular/core';
import { BlobServiceClient } from '@azure/storage-blob';
import { Image } from './image';

@Injectable({
  providedIn: 'root'
})
export class BlobService {
  private blobServiceClient: BlobServiceClient;

  account = 'storeparkings';
  // tslint:disable-next-line: max-line-length
  accountSas = '?sv=2019-10-10&ss=b&srt=sco&sp=rlx&se=2021-06-24T03:06:09Z&st=2020-06-23T19:06:09Z&spr=https,http&sig=2T78vaaPJnAVGz2Lrw7Uf8RfUSiCuApVyiYWKeAvcyE%3D';


  constructor() {
    this.blobServiceClient = new BlobServiceClient(
      `https://${this.account}.blob.core.windows.net${this.accountSas}`
    );
   }

   async listContainer() {

    const containerNames = [];
    for await (const container of this.blobServiceClient.listContainers()) {
      containerNames.push(container.name);
    }
    return containerNames;
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

