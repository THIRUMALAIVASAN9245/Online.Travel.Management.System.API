namespace Online.Travel.Management.System.API.Model
{
    public class BookingResponse
    {
        ///<Summary>
        /// BookingDetails
        ///</Summary>
        public Booking BookingDetails { get; set; }

        ///<Summary>
        /// CustomerDetails
        ///</Summary>
        public UserModel CustomerDetails { get; set; }

        ///<Summary>
        /// EmployeeDetails
        ///</Summary>
        public UserModel EmployeeDetails { get; set; }
    }
}