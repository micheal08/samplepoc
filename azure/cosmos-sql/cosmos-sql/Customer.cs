using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace cosmos_sql
{
    class Customer
    {
        [JsonProperty(PropertyName ="id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "customerid")]
        public int CustomerId { get; set; }
        [JsonProperty(PropertyName = "customername")]
        public string CustomerName { get; set; }
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "orders")]
        public List<Orders> Orders;
    }
}
