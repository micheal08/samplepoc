const { BlobServiceClient } = require('@azure/storage-blob');
module.exports = async function (context, req) {
    context.log('JavaScript HTTP trigger function processed a request.');
    
    const AZURE_STORAGE_CONNECTION_STRING = process.env["AZURE_STORAGE_CONNECTION_STRING"];
    
    const blobServiceClient = await BlobServiceClient.fromConnectionString(AZURE_STORAGE_CONNECTION_STRING);

    const containerName = process.env["CONTAINER_NAME"];
    context.log('\t', containerName);


    const containerClient = await blobServiceClient.getContainerClient(containerName);

    const createContainerResponse = await containerClient.create()
    context.log("Container was created successfully. requestId: ", createContainerResponse.requestId);


    const blobName = 'quickstart.txt';

    // Get a block blob client
    const blockBlobClient = containerClient.getBlockBlobClient(blobName);

    console.log('\nUploading to Azure storage as blob:\n\t', blobName);

    // Upload data to the blob
    const data = 'Hello, World!';
    const uploadBlobResponse = await blockBlobClient.upload(data, data.length);
    console.log("Blob was uploaded successfully. requestId: ", uploadBlobResponse.requestId);

    const name = (req.query.name || (req.body && req.body.name));
    const responseMessage = name
        ? "Hello, " + name + ". This HTTP triggered function executed successfully."
        : "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response.";

    context.res = {
        // status: 200, /* Defaults to 200 */
        body: responseMessage
    };
}