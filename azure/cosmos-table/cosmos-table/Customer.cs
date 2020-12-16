﻿using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace cosmos_table
{
    class Customer : TableEntity
    {
        public string city { get; set; }

        public Customer()
        {

        }

        public Customer(string pk, string rk, string ci)
        {
            PartitionKey = pk;
            RowKey = rk;
            city = ci;
        }
    }
}
