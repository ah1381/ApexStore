namespace Store.Site.Controllers
{
    internal class PaymentRequest
    {
        public object Amount { get; set; }
        public object Description { get; set; }
        public string CallbackUrl { get; set; }
    }
}