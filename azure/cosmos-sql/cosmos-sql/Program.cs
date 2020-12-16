using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cosmos_sql
{
    class Program
    {
        static string database = "appdb";
        static string containername = "customer";
        static string endpoint = "https://democosmosmicheal.documents.azure.com:443/";
        static string accountkeys = "ApiMWET8h5V0dVY4AMikbUpWYbexnQi5VBMCvRlbk40dPiqEylKuEjMrlrDwtL16c3DQgXuvKPDogVZWh8KAVQ==";
        static string cosmosDbConnectionString = "";

        static async Task Main(string[] args)
        {
            //await CreateNewItem();
            //await ReadItems();
            //await ReadAndReplaceItem();
            //await DeleteItem();
            //await CreateNewItemWithEmbeddedData();
            //await CreateNewItemWithSp();
            await ReadItemFromDifferentRegion();

            Console.WriteLine("Hello World!");
        }

        private static async Task ReadItemFromDifferentRegion()
        {
            CosmosClient cosmos = new CosmosClient(cosmosDbConnectionString,
                new CosmosClientOptions() { ApplicationRegion = Regions.WestEurope });

            Database db = cosmos.GetDatabase(database);
            Container cont = db.GetContainer(containername);

            string id = "8024a2ec-6ca9-4c51-9427-2602dc8b8f30";

            PartitionKey pk = new PartitionKey("New York");
            ItemResponse<dynamic> response = await cont.ReadItemAsync<dynamic>(id, pk);
            string outputString = JValue.Parse(response.Diagnostics.ToString()).ToString(Formatting.Indented);
            Console.WriteLine(outputString);
            Console.WriteLine("Operation Completed");
        }

        private static async Task CreateNewItemWithSp()
        {
            using (CosmosClient cosmos = new CosmosClient(endpoint, accountkeys))
            {
                Database db = cosmos.GetDatabase(database);
                Container cont = db.GetContainer(containername);

                var sps = cont.Scripts;
                dynamic[] obj = new dynamic[]
                {
                    new  {id =  Guid.NewGuid().ToString(),customerid = 5, customername = "Mani", city = "Miami" }
                };
                PartitionKey pk = new PartitionKey(obj[0].city);

                var response = await sps.ExecuteStoredProcedureAsync<string>("createitem", pk, obj);

                Console.WriteLine(response.Resource);
            }
        }

        private static async Task CreateNewItemWithEmbeddedData()
        {
            using (CosmosClient cosmos = new CosmosClient(endpoint, accountkeys))
            {
                Database db = cosmos.GetDatabase(database);
                Container cont = db.GetContainer(containername);

                Customer cust = new Customer { CustomerId = 4, CustomerName = "William", City = "Miami" };
                cust.Id = Guid.NewGuid().ToString();

                List<Orders> orders = new List<Orders>()
                {
                    new Orders(1002,4),
                    new Orders(1003,3)
                };
                cust.Orders = orders;

                ItemResponse<Customer> response = await cont.CreateItemAsync(cust);
                Console.WriteLine("Request charge is {0}", response.RequestCharge);
                Console.WriteLine("Customer Added");
            }
        }

        private static async Task DeleteItem()
        {
            using (CosmosClient cosmos = new CosmosClient(endpoint, accountkeys))
            {
                Database db = cosmos.GetDatabase(database);
                Container cont = db.GetContainer(containername);

                PartitionKey pk = new PartitionKey("New York");
                string id = "8024a2ec-6ca9-4c51-9427-2602dc8b8f30";

                ItemResponse<Customer> response = await cont.DeleteItemAsync<Customer>(id,pk);

                Console.WriteLine("Request charge is {0}", response.RequestCharge);
                Console.WriteLine("Item Deleted");
            }
        }

        private static async Task ReadAndReplaceItem()
        {
            using (CosmosClient cosmos = new CosmosClient(endpoint, accountkeys))
            {
                Database db = cosmos.GetDatabase(database);
                Container cont = db.GetContainer(containername);

                PartitionKey pk = new PartitionKey("Miami");
                string id = "c8a806cc-4c79-4756-ad15-537c12925762";

                ItemResponse<Customer> response = await cont.ReadItemAsync<Customer>(id, pk);
                Customer cust = response.Resource;
                cust.CustomerName = "David";

                response = await cont.ReplaceItemAsync(cust, id, pk);

                Console.WriteLine("Request charge is {0}", response.RequestCharge);
                Console.WriteLine("Item updated");
            }
        }

        private static async Task ReadItems()
        {
            using (CosmosClient cosmos = new CosmosClient(endpoint, accountkeys))
            {
                Database db = cosmos.GetDatabase(database);
                Container cont = db.GetContainer(containername);

                string sql = "select c.customerid,c.customername,c.city from c";

                QueryDefinition query = new QueryDefinition(sql);
                FeedIterator<Customer> feed = cont.GetItemQueryIterator<Customer>(sql);

                while (feed.HasMoreResults)
                {
                    FeedResponse<Customer> feedResponse = await feed.ReadNextAsync();
                    Console.WriteLine("Request charge is {0}", feedResponse.RequestCharge);
                    foreach (var item in feedResponse)
                    {
                        Console.WriteLine("Customer Id is {0}", item.CustomerId);
                        Console.WriteLine("Customer Name is {0}", item.CustomerName);
                        Console.WriteLine("Customer City is {0}", item.City);
                    }
                }

            }
        }

        private static async Task CreateNewItem()
        {
            using (CosmosClient cosmos = new CosmosClient(endpoint, accountkeys))
            {
                Database db = cosmos.GetDatabase(database);
                Container cont = db.GetContainer(containername);

                Customer cust = new Customer { CustomerId = 2, CustomerName = "James", City = "Miami"};
                cust.Id = Guid.NewGuid().ToString();

                ItemResponse<Customer> response = await cont.CreateItemAsync(cust);
                Console.WriteLine("Request charge is {0}", response.RequestCharge);
                Console.WriteLine("Customer Added");
            }
        }
    }
}