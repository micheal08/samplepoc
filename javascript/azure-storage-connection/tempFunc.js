const accountName = "michealdevdiag";
const key = "";

function getBlobSasUrl() {
    // generate account sas token
    
    const start = new Date(new Date().getTime() - (15 * 60 * 1000));
    const end = new Date(new Date().getTime() + (30 * 60 * 1000));
    const signedpermissions = "rwdlacupx";
    const signedservice = "b";
    const signedresourcetype = "sco";
    const signedstart =
      end.toISOString().substring(0, start.toISOString().lastIndexOf(".")) + "Z";
    const signedexpiry =
      end.toISOString().substring(0, end.toISOString().lastIndexOf(".")) + "Z";
    const signedProtocol = "https";
    const signedversion = "2020-05-05";
  
    const StringToSign =
      accountName +
      "\n" +
      signedpermissions +
      "\n" +
      signedservice +
      "\n" +
      signedresourcetype +
      "\n" +
      signedstart +
      "\n" +
      signedexpiry +
      "\n" +    
      signedProtocol +
      "\n" +
      signedversion +
      "\n";
    const crypto =require('crypto')
     const sig = crypto.createHmac('sha256', key).update(StringToSign, 'utf8').digest('base64');
    const sasToken = `sv=${signedversion}&ss=${signedservice}&srt=${signedresourcetype}&sp=${signedpermissions}&se=${encodeURIComponent(signedexpiry)}&st=${encodeURIComponent(signedstart)}&spr=${signedProtocol}&sig=${encodeURIComponent(sig)}`;
    return `$https://${accountName}.blob.core.windows.net/?${sasToken}`;
  }