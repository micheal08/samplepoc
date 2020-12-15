using System;
using System.Collections.Generic;
using System.Text;

namespace cosmos_sql
{
    class Orders
    {
        public int orderid { get; set; }

        public int quantity { get; set; }

        public Orders(int p_orderId, int p_quantity)
        {
            orderid = p_orderId;
            quantity = p_quantity;
        }
    }
}
