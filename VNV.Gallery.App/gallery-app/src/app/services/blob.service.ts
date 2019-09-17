import { Injectable } from '@angular/core';
import { AnonymousCredential, StorageURL, ServiceURL, Aborter, ContainerURL, IServiceListContainersSegmentOptions } from '@azure/storage-blob';

@Injectable({
  providedIn: 'root'
})
export class BlobService {
  account = 'storeparkings';
  // tslint:disable-next-line: max-line-length
  accountSas = '?sv=2018-03-28&ss=b&srt=sco&sp=rwdlac&se=2021-09-11T03:45:08Z&st=2019-09-10T19:45:08Z&spr=https&sig=gEGdyLWBjn2Ig%2FJ4EXz2%2FwZkNyIPxjIRbJ2a7ZmX1I4%3D';


  constructor() {

   }

   async listContainer() {

  // Use AnonymousCredential when url already includes a SAS signature
    const anonymousCredential = new AnonymousCredential();

  // Use sharedKeyCredential, tokenCredential or anonymousCredential to create a pipeline
    const pipeline = StorageURL.newPipeline(anonymousCredential);

  // List containers
    const serviceURL = new ServiceURL(
    `https://${this.account}.blob.core.windows.net${this.accountSas}`,
    pipeline
  );

    let marker: string;
    const containerNames = [];
    const options =  {
      prefix: 'gallery-'
    } as IServiceListContainersSegmentOptions;
    do {
      const listContainersResponse = await serviceURL.listContainersSegment(
        Aborter.none,
        marker,
        options
      );

      marker = listContainersResponse.nextMarker;
      for (const container of listContainersResponse.containerItems) {
        containerNames.push(container.name);
      }
    } while (marker);
    return containerNames;
   }
   async getBlobs(containerName: string) {
     // Use AnonymousCredential when url already includes a SAS signature
    const anonymousCredential = new AnonymousCredential();

    // Use sharedKeyCredential, tokenCredential or anonymousCredential to create a pipeline
    const pipeline = StorageURL.newPipeline(anonymousCredential);

    // List containers
    const serviceURL = new ServiceURL(
      `https://${this.account}.blob.core.windows.net${this.accountSas}`,
      pipeline
    );

    const containerUrl = ContainerURL.fromServiceURL(serviceURL, containerName);
    const metaData = await containerUrl.listBlobFlatSegment(Aborter.none);
    return metaData.segment.blobItems;
   }
}
