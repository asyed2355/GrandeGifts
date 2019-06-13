namespace GrandeGifts.ViewModels.Address
{
    public class AddressCustomerEditViewModel
    {
        //public Guid AddressId { get; set; }
        public string StreetAddress { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public int Postcode { get; set; }
        public string AddressType { get; set; }
        public bool PreferredShippingAddress { get; set; }
    }
}
