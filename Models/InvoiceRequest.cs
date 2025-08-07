using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class InvoiceRequest
    {
        [JsonPropertyName("bookingId")]
        public long BookingId { get; set; }

        [JsonPropertyName("turfId")]
        public long TurfId { get; set; }

        [JsonPropertyName("paymentMethod")]
        public string PaymentMethod { get; set; }

        [JsonPropertyName("amountPaid")]
        public double AmountPaid { get; set; }
    }
}
