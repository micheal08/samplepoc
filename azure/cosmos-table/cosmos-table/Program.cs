using Microsoft.Azure.Cosmos.Table;
using System;
using System.Threading.Tasks;

namespace cosmos_table
{
    class Program
    {
        static string cosmosdbConnectionString = "";
        static string tableName = "Customer";
        static async void Main(string[] args)
        {

            await NewItem();
            await ReadItem();
        }

        private static async Task ReadItem()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(cosmosdbConnectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference(tableName);

            string pk = "1";
            string rowkey = "John";

            TableOperation operation = TableOperation.Retrieve(pk, rowkey);
            TableResult result = await table.ExecuteAsync(operation);

            Customer obj = (Customer)result.Result;

            Console.WriteLine(obj.PartitionKey);
            Console.WriteLine(obj.RowKey);
            Console.WriteLine(obj.city);
        }

        private static async Task NewItem()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(cosmosdbConnectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference(tableName);

            Customer custObj = new Customer("1", "John", "New York");
            TableOperation operation = TableOperation.Insert(custObj);
            TableResult result = await table.ExecuteAsync(operation);

            Console.WriteLine("Entity added");
        }
    }
}
