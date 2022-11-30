using System;

namespace FP.Monitoring.Trace.UI.Models
{
    public class OrderViewModel
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public string Customer { get; set; }

        public string CardType { get; set; }

        public string CardNumber { get; set; }
    }
}
