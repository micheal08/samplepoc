const { StorageSharedKeyCredential, 
        SASProtocol, 
        generateBlobSASQueryParameters, 
        generateAccountSASQueryParameters,
        AccountSASPermissions,
        AccountSASResourceTypes,
        AccountSASServices,
        ContainerSASPermissions,        
        BlobServiceClient } = require('@azure/storage-blob');

module.exports = async function (context, req) {
        
    const account = "michealdevdiag";
    const accountKey = "DefaultEndpointsProtocol=https;AccountName=michealdevdiag;AccountKey=jCOTaL4WEYWojyf0sotrhifh9NAoORqxIM3ckACXjQNHzbMyACFVhoNluTSk9KvwFU6SIxbJjnjkrsEPjv91lA==;EndpointSuffix=core.windows.net";
    const containerName = "testpd";

    let sharedKeyCredential = new StorageSharedKeyCredential(account, accountKey);
    var accountSAS = getaccoutSAS(sharedKeyCredential);

    const blobSasUrl = getBlobSAS(containerName, sharedKeyCredential);

    context.log(blobSasUrl)

    context.res = {
        body: accountSAS
    }
    context.done();
}

function getBlobSAS(containerName, sharedKeyCredential) {
    return generateBlobSASQueryParameters({
        containerName,
        permissions: ContainerSASPermissions.parse("racwdl"),
        startsOn: new Date(),
        expiresOn: new Date(new Date().valueOf() + 86400),
        ipRange: { start: "0.0.0.0", end: "255.255.255.255" },
        protocol: SASProtocol.Https,
        version: "2020-03-25" // Optional
    }, sharedKeyCredential // StorageSharedKeyCredential - `new StorageSharedKeyCredential(account, accountKey)`
    ).toString();
}

function getaccoutSAS(sharedKeyCredential) {

    const signedpermissions = 'rwdlac';
    const signedservice = 'b';
    const signedresourcetype = 'sco';
    const signedversion = '2020-03-28';
    let startDate = new Date();
    let expiryDate = new Date();
    startDate.setTime(startDate.getTime() - 5*60*1000);
    expiryDate.setTime(expiryDate.getTime() + 24*60*60*1000);
    expiryDate.setTime(expiryDate.getTime() + 24*60*60*1000);

    return generateAccountSASQueryParameters({
        expiresOn: expiryDate,
        permissions: AccountSASPermissions.parse(signedpermissions),
        protocol: SASProtocol.Https,
        resourceTypes: AccountSASResourceTypes.parse(signedresourcetype).toString(),
        services: AccountSASServices.parse(signedservice).toString(),
        startsOn: startDate,
        version: signedversion,
    }, sharedKeyCredential).toString();
}
