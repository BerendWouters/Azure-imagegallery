import { Injectable } from '@angular/core';
import { AnonymousCredential, StorageURL, ServiceURL, Aborter, ContainerURL } from '@azure/storage-blob';

@Injectable({
  providedIn: 'root'
})
export class BlobService {
  account = 'storeparkings';
  // tslint:disable-next-line: max-line-length
  accountSas = '?sv=2018-03-28&ss=bfqt&srt=sco&sp=rwdlacup&se=2019-09-04T03:14:50Z&st=2019-09-03T19:14:50Z&spr=https&sig=rmkAQ5VRFto3Cn0KymLqd2WCWG006eiweFLnk7sgYmQ%3D';


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
    do {
      const listContainersResponse = await serviceURL.listContainersSegment(
        Aborter.none,
        marker
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
