namespace Exercise1
{
    internal class Order
    {
        private Side side;
        private double price;
        private int quantity;

        public Order(Side buy, double price, int quantity)
        {
            this.side = buy;
            this.price = price;
            this.quantity = quantity;
        }
        public Side Side { get { return side; } }

        public double Price { get { return price; } }

        public int Quantity { get { return quantity; } }
    }
}