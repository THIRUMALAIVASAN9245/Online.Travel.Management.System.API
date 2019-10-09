namespace Online.Travel.Management.System.API.Model
{
    public class BookingRequest
    {
        public string Operation { get; set; }

        public string FilterByStatus { get; set; }

        public int Id { get; set; }
    }
}