namespace ShoeInventory.Models
{
 
    public class TransactionRecord
    {
    
        private int _id;
        private int _productId;
        private string _productName;
        private string _action;     
        private int _quantity;
        private int _stockBefore;
        private int _stockAfter;
        private DateTime _dateTime;
        private string _performedBy; 

      
        public int Id
        {
            get => _id;
            private set => _id = value;
        }

        public int ProductId
        {
            get => _productId;
            private set => _productId = value;
        }

        public string ProductName
        {
            get => _productName;
            private set => _productName = value ?? string.Empty;
        }

        public string Action
        {
            get => _action;
            private set
            {
                if (value != "Restock" && value != "Deduct")
                    throw new ArgumentException("Action must be 'Restock' or 'Deduct'.");
                _action = value;
            }
        }

        public int Quantity
        {
            get => _quantity;
            private set
            {
                if (value <= 0)
                    throw new ArgumentException("Transaction quantity must be greater than zero.");
                _quantity = value;
            }
        }

        public int StockBefore
        {
            get => _stockBefore;
            private set => _stockBefore = value;
        }

        public int StockAfter
        {
            get => _stockAfter;
            private set => _stockAfter = value;
        }

        public DateTime DateTime
        {
            get => _dateTime;
            private set => _dateTime = value;
        }

        public string PerformedBy
        {
            get => _performedBy;
            private set => _performedBy = value ?? "system";
        }

     
        public TransactionRecord(int id, int productId, string productName, string action,
                                  int quantity, int stockBefore, int stockAfter, string performedBy)
        {
            Id = id;
            ProductId = productId;
            ProductName = productName;
            Action = action;
            Quantity = quantity;
            StockBefore = stockBefore;
            StockAfter = stockAfter;
            DateTime = DateTime.Now;
            PerformedBy = performedBy;
        }

        public override string ToString()
        {
            string actionSymbol = Action == "Restock" ? "▲" : "▼";
            return $"[{Id}] {DateTime:MM/dd/yyyy HH:mm} | {actionSymbol} {Action} " +
                   $"| Product: {ProductName} (ID:{ProductId}) " +
                   $"| Qty: {Quantity} | Stock: {StockBefore} → {StockAfter} " +
                   $"| By: {PerformedBy}";
        }
    }
}
