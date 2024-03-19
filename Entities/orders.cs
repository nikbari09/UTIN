namespace UTIN.Entities
{
    public class orders
    {
        public int id { get; set; }
        public List<Details> details { get; set; }
        public String time { get; set; }

        public String date { get; set; }
    }

    public class Details
    {
        internal static int ordersid;

        public int id { get; set; }
        public List<Address> address { get; set; }
        public String email { get; set; }
        public int total_cost { get; set; }
        public int count { get; set; }
        public String item_name { get; set; }
        public String status { get; set; }
        public String time { set; get; }
        public String firstname { get; set; }
        public String lastname { get; set; }
        public String payment_mode { get; set; } 
        public String transaction_id { get; set; }

    }

    public class Address
    {
        public int id { get; set; }
        public String street { get; set; }
        public String city { get; set; }
        public String state { get; set; }
        public String pinCode { get; set; }

    }
}
