// index.js
const { BlobServiceClient } = require('@azure/storage-blob');
// Now do something interesting with BlobServiceClient

const createContainerButton = document.getElementById(
  "create-container-button"
);
const deleteContainerButton = document.getElementById(
  "delete-container-button"
);
const selectButton = document.getElementById("select-button");
const fileInput = document.getElementById("file-input");
const listButton = document.getElementById("list-button");
const deleteButton = document.getElementById("delete-button");
const downloadButton = document.getElementById("download-button");
const status = document.getElementById("status");
const fileList = document.getElementById("file-list");
const accountName = "michealdevdiag";
const key = "";

const reportStatus = (message) => {
  status.innerHTML += `${message}<br/>`;
  status.scrollTop = status.scrollHeight;
};

// Update <placeholder> with your Blob service SAS URL string

// let sasUrl = getBlobSasUrl();
// console.log(sasUrl);
// const blobSasUrl =
//   "https://michealdevdiag.blob.core.windows.net/?sv=2019-10-10&ss=bfqt&srt=sco&sp=rwdlacupx&se=2020-05-07T18:05:57Z&st=2020-05-07T10:05:57Z&spr=https&sig=jEP3UvkPm78xWDqsnZrDA72twCYZqWFYjU8W0MUxzXA%3D";

const blobSasUrl =
  "https://michealdevdiag.blob.core.windows.net/?sv=2019-10-10&ss=bfqt&srt=sco&sp=rwdlacupx&se=2020-05-08T19:28:13Z&st=2020-05-08T11:28:13Z&spr=https&sig=jBkVc%2FZdBFUvoGqJwEgPWW1Ah0kNAO3kIxQn%2Bt2aEAo%3D";

// Create a new BlobServiceClient
const blobServiceClient = new BlobServiceClient(blobSasUrl);

console.log("connection established");

const containerName = "testpd";

// Get a container client from the BlobServiceClient
const containerClient = blobServiceClient.getContainerClient(containerName);
console.log("ContainerClinet initialized");

const createContainer = async () => {
  try {
    reportStatus(`Creating container "${containerName}"...`);
    if(!containerClient.exists())
    {
        await containerClient.create();
    }
    else {
        reportStatus(`Container already exists`);
    }
    reportStatus(`Done.`);
  } catch (error) {
    reportStatus(error.message);
  }
};

const deleteContainer = async () => {
  try {
    reportStatus(`Deleting container "${containerName}"...`);
    if(containerClient.exists())
    {
        await containerClient.delete();
    }
    else {
        reportStatus(`Container not exists`);
    }
    reportStatus(`Done.`);
  } catch (error) {
    reportStatus(error.message);
  }
};

createContainerButton.addEventListener("click", createContainer);
deleteContainerButton.addEventListener("click", deleteContainer);

const listFiles = async () => {
  fileList.size = 0;
  fileList.innerHTML = "";
  try {
    reportStatus("Retrieving file list...");
    let iter = containerClient.listBlobsFlat();
    let blobItem = await iter.next();
    while (!blobItem.done) {
      fileList.size += 1;
      fileList.innerHTML += `<option>${blobItem.value.name}</option>`;
      blobItem = await iter.next();
    }
    if (fileList.size > 0) {
      reportStatus("Done.");
    } else {
      reportStatus("The container does not contain any files.");
    }
  } catch (error) {
    reportStatus(error.message);
  }
};

listButton.addEventListener("click", listFiles);

const uploadFiles = async () => {
  try {
    reportStatus("Uploading files...");
    const promises = [];
    for (const file of fileInput.files) {
      const blockBlobClient = containerClient.getBlockBlobClient(file.name);
      blockBlobClient.exists
      promises.push(blockBlobClient.uploadBrowserData(file));
    }
    await Promise.all(promises);
    reportStatus("Done.");
    listFiles();
    // const formData = new FormData();
    // formData.append('username', 'abc123');
    // formData.append('avatar', fileInput.files[0]);
    // fetch('http://localhost:7071/api/GetBlobSasUrl', {
    // method: 'POST',
    // body: formData
    // })
    // .then(response => response.json())
    // .then(result => {
    // console.log('Success:', result);
    // })
    // .catch(error => {
    // console.error('Error:', error);
    // });
  } catch (error) {
    reportStatus(error.message);
  }
};

selectButton.addEventListener("click", () => fileInput.click());
fileInput.addEventListener("change", uploadFiles);

const deleteFiles = async () => {
  try {
    if (fileList.selectedOptions.length > 0) {
      reportStatus("Deleting files...");
      for (const option of fileList.selectedOptions) {
        await containerClient.deleteBlob(option.text);
      }
      reportStatus("Done.");
      listFiles();
    } else {
      reportStatus("No files selected.");
    }
  } catch (error) {
    reportStatus(error.message);
  }
};

deleteButton.addEventListener("click", deleteFiles);

const downloadFile = async () => {
    try {
        let fileName = "CostEstimation_CeltisOdtuRouter.xlsx";
        const blockBlobClient = containerClient.getBlockBlobClient(fileName);
        
        const downloadRespone = blockBlobClient.download(0);
        window.location.href = (await downloadRespone)._response.request.url;
        console.log((await downloadRespone)._response.request.url);
        //console.log(downloadRespone);
        reportStatus("Done.");
    } catch (error) {
      reportStatus(error.message);
    }
  };
  
  downloadButton.addEventListener("click", downloadFile);
