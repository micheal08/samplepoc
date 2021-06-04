using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Rest;
using Microsoft.Rest.Serialization;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.DataFactory;
using Microsoft.Azure.Management.DataFactory.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Adf
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set variables
            string tenantID = "<your tenant ID>";
            string applicationId = "<your application ID>";
            string authenticationKey = "<your authentication key for the application>";
            string subscriptionId = "<your subscription ID where the data factory resides>";
            string resourceGroup = "<your resource group where the data factory resides>";
            string region = "<the location of your resource group>";
            string dataFactoryName =
                "<specify the name of data factory to create. It must be globally unique.>";
            string storageAccount = "<your storage account name to copy data>";
            string storageKey = "<your storage account key>";
            // specify the container and input folder from which all files 
            // need to be copied to the output folder. 
            string inputBlobPath =
                "<path to existing blob(s) to copy data from, e.g. containername/inputdir>";
            //specify the contains and output folder where the files are copied
            string outputBlobPath =
                "<the blob path to copy data to, e.g. containername/outputdir>";

            // name of the Azure Storage linked service, blob dataset, and the pipeline
            string storageLinkedServiceName = "AzureStorageLinkedService";
            string blobDatasetName = "BlobDataset";
            string pipelineName = "Adfv2QuickStartPipeline";


            // Authenticate and create a data factory management client
            var context = new AuthenticationContext("https://login.microsoftonline.com/" + tenantID);
            ClientCredential cc = new ClientCredential(applicationId, authenticationKey);
            AuthenticationResult result = context.AcquireTokenAsync(
                "https://management.azure.com/", cc).Result;
            ServiceClientCredentials cred = new TokenCredentials(result.AccessToken);
            var client = new DataFactoryManagementClient(cred)
            {
                SubscriptionId = subscriptionId
            };

            // Create a data factory
            Console.WriteLine("Creating data factory " + dataFactoryName + "...");
            Factory dataFactory = new Factory
            {
                Location = region,
                Identity = new FactoryIdentity()
            };
            client.Factories.CreateOrUpdate(resourceGroup, dataFactoryName, dataFactory);
            Console.WriteLine(
                SafeJsonConvert.SerializeObject(dataFactory, client.SerializationSettings));

            while (client.Factories.Get(resourceGroup, dataFactoryName).ProvisioningState ==
                   "PendingCreation")
            {
                System.Threading.Thread.Sleep(1000);
            }

            // Create an Azure Storage linked service
            Console.WriteLine("Creating linked service " + storageLinkedServiceName + "...");

            LinkedServiceResource storageLinkedService = new LinkedServiceResource(
                new AzureStorageLinkedService
                {
                    ConnectionString = new SecureString(
                        "DefaultEndpointsProtocol=https;AccountName=" + storageAccount +
                        ";AccountKey=" + storageKey)
                }
            );
            client.LinkedServices.CreateOrUpdate(
                resourceGroup, dataFactoryName, storageLinkedServiceName, storageLinkedService);
            Console.WriteLine(SafeJsonConvert.SerializeObject(
                storageLinkedService, client.SerializationSettings));


            // Create an Azure Blob dataset
            Console.WriteLine("Creating dataset " + blobDatasetName + "...");
            DatasetResource blobDataset = new DatasetResource(
                new AzureBlobDataset
                {
                    LinkedServiceName = new LinkedServiceReference
                    {
                        ReferenceName = storageLinkedServiceName
                    },
                    FolderPath = new Expression { Value = "@{dataset().path}" },
                    Parameters = new Dictionary<string, ParameterSpecification>
                    {
            { "path", new ParameterSpecification { Type = ParameterType.String } }
                    }
                }
            );
            client.Datasets.CreateOrUpdate(
                resourceGroup, dataFactoryName, blobDatasetName, blobDataset);
            Console.WriteLine(
                SafeJsonConvert.SerializeObject(blobDataset, client.SerializationSettings));

            // Create a pipeline with a copy activity
            Console.WriteLine("Creating pipeline " + pipelineName + "...");
            PipelineResource pipeline = new PipelineResource
            {
                Parameters = new Dictionary<string, ParameterSpecification>
    {
        { "inputPath", new ParameterSpecification { Type = ParameterType.String } },
        { "outputPath", new ParameterSpecification { Type = ParameterType.String } }
    },
                Activities = new List<Activity>
    {
        new CopyActivity
        {
            Name = "CopyFromBlobToBlob",
            Inputs = new List<DatasetReference>
            {
                new DatasetReference()
                {
                    ReferenceName = blobDatasetName,
                    Parameters = new Dictionary<string, object>
                    {
                        { "path", "@pipeline().parameters.inputPath" }
                    }
                }
            },
            Outputs = new List<DatasetReference>
            {
                new DatasetReference
                {
                    ReferenceName = blobDatasetName,
                    Parameters = new Dictionary<string, object>
                    {
                        { "path", "@pipeline().parameters.outputPath" }
                    }
                }
            },
            Source = new BlobSource { },
            Sink = new BlobSink { }
        }
    }
            };
            client.Pipelines.CreateOrUpdate(resourceGroup, dataFactoryName, pipelineName, pipeline);
            Console.WriteLine(SafeJsonConvert.SerializeObject(pipeline, client.SerializationSettings));

            // Create a pipeline run
            Console.WriteLine("Creating pipeline run...");
            Dictionary<string, object> parameters = new Dictionary<string, object>
{
    { "inputPath", inputBlobPath },
    { "outputPath", outputBlobPath }
};
            CreateRunResponse runResponse = client.Pipelines.CreateRunWithHttpMessagesAsync(
                resourceGroup, dataFactoryName, pipelineName, parameters: parameters
            ).Result.Body;
            Console.WriteLine("Pipeline run ID: " + runResponse.RunId);

            // Monitor the pipeline run
            Console.WriteLine("Checking pipeline run status...");
            PipelineRun pipelineRun;
            while (true)
            {
                pipelineRun = client.PipelineRuns.Get(
                    resourceGroup, dataFactoryName, runResponse.RunId);
                Console.WriteLine("Status: " + pipelineRun.Status);
                if (pipelineRun.Status == "InProgress" || pipelineRun.Status == "Queued")
                    System.Threading.Thread.Sleep(15000);
                else
                    break;
            }

            // Check the copy activity run details
            Console.WriteLine("Checking copy activity run details...");

            RunFilterParameters filterParams = new RunFilterParameters(
                DateTime.UtcNow.AddMinutes(-10), DateTime.UtcNow.AddMinutes(10));
            ActivityRunsQueryResponse queryResponse = client.ActivityRuns.QueryByPipelineRun(
                resourceGroup, dataFactoryName, runResponse.RunId, filterParams);
            if (pipelineRun.Status == "Succeeded")
                Console.WriteLine(queryResponse.Value.First().Output);
            else
                Console.WriteLine(queryResponse.Value.First().Error);
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
