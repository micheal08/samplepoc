import { Component } from '@angular/core';
import { BlobServiceClient, AnonymousCredential, newPipeline, ContainerClient } from '@azure/storage-blob';
import { environment } from 'src/environments/environment';
// import * as CryptoJS from 'crypto-js';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'azure-connection';
  currentFile: File = null;
  onFileChange(event) {
    console.log(environment.accountName);
    console.log(event);
    this.currentFile = event.target.files[0];
    console.log(event.target.value);
    this.uploadFile();
  }

  uploadWithFilePath() {

  }

  async uploadFile() {
    const blobServiceClient = new BlobServiceClient(environment.sasTokenUrl);
    console.log('connection established');

    const contClient = await blobServiceClient.getContainerClient(environment.containerName);
    console.log('ContainerClient initialized');
    console.log(contClient);
    const response = await contClient.create();
    console.log('Container created successfully' + response.errorCode);
    const client = contClient.getBlockBlobClient(this.currentFile.name);
    const promises = [];
    promises.push(client.uploadBrowserData(this.currentFile));
    await Promise.all(promises);
    // await client.uploadBrowserData(this.currentFile, {
    //   blockSize: 4 * 1024 * 1024, // 4MB block size
    //   concurrency: 20, // 20 concurrency
    //   onProgress: ev => console.log(ev),
    //   blobHTTPHeaders: { blobContentType: this.currentFile.type },
    // });
    console.log('Blob uploaded successfully');
    // if (!containerClient.exists()) {
    //   console.log('the container does not exit');
    //   await containerClient.create();
    // }
  }

  // async Upload() {
  //   // generate account sas token
  //   const accountName = environment.accountName;
  //   const key = environment.key;
  //   const start = new Date(new Date().getTime() - 15 * 60 * 1000);
  //   const end = new Date(new Date().getTime() + 30 * 60 * 1000);
  //   const signedpermissions = 'rwdlac';
  //   const signedservice = 'b';
  //   const signedresourcetype = 'sco';
  //   const signedexpiry =
  //     end.toISOString().substring(0, end.toISOString().lastIndexOf('.')) + 'Z';
  //   const signedProtocol = 'https';
  //   const signedversion = '2020-05-05';

  //   const StringToSign =
  //     accountName +
  //     '\n' +
  //     signedpermissions +
  //     '\n' +
  //     signedservice +
  //     '\n' +
  //     signedresourcetype +
  //     '\n' +
  //     '\n' +
  //     signedexpiry +
  //     '\n' +
  //     '\n' +
  //     signedProtocol +
  //     '\n' +
  //     signedversion +
  //     '\n';
  //   // const crypto = require('crypto');
  //   // const sig = crypto
  //   //   .createHmac('sha256', Buffer.from(key, 'base64'))
  //   //   .update(StringToSign, 'utf8')
  //   //   .digest('base64');

  //   const str = CryptoJS.HmacSHA256(StringToSign, CryptoJS.enc.Base64.parse(key));
  //   const sig = CryptoJS.enc.Base64.stringify(str);
  //   const sasToken = `sv=${signedversion}&ss=${signedservice}&srt=${signedresourcetype}&sp=${signedpermissions}&se=${encodeURIComponent(
  //     signedexpiry
  //   )}&spr=${signedProtocol}&sig=${encodeURIComponent(sig)}`;
  //   const containerName = environment.containerName;
  //   console.log(containerName);
  //   const pipeline = newPipeline(new AnonymousCredential(), {
  //     retryOptions: { maxTries: 4 }, // Retry options
  //     userAgentOptions: { userAgentPrefix: 'AdvancedSample V1.0.0' }, // Customized telemetry string
  //     keepAliveOptions: {
  //       // Keep alive is enabled by default, disable keep alive by setting false
  //       enable: false,
  //     },
  //   });
  //   console.log('Pipeline intialized');
  //   const blobServiceClient = new BlobServiceClient(
  //     `https://${accountName}.blob.core.windows.net?${sasToken}`,
  //     pipeline
  //   );

  //   console.log('Connection established using sas token');
  //   const containerClient = await blobServiceClient.getContainerClient(containerName);
  //   console.log('ContainerClient Initialized');
  //   await containerClient.create();
  //   console.log('Container created');
  //   if (!containerClient.exists()) {
  //     console.log('the container does not exit');
  //     // await containerClient.create();
  //   }
  //   const client = containerClient.getBlockBlobClient(this.currentFile.name);
  //   const response = await client.uploadBrowserData(this.currentFile, {
  //     blockSize: 4 * 1024 * 1024, // 4MB block size
  //     concurrency: 20, // 20 concurrency
  //     onProgress: (ev) => console.log(ev),
  //     blobHTTPHeaders: { blobContentType: this.currentFile.type },
  //   });
  //   console.log(response._response.status);
  // }
}
