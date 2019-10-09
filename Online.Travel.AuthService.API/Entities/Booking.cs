namespace Online.Travel.AuthService.API.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Booking
    {
        ///<Summary>
        /// Id
        ///</Summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        ///<Summary>
        /// CustomerId
        ///</Summary>
        public int CustomerId { get; set; }

        ///<Summary>
        /// EmployeeId
        ///</Summary>
        public int EmployeeId { get; set; }

        ///<Summary>
        /// PickupLocation
        ///</Summary>
        public string PickupLocation { get; set; }

        ///<Summary>
        /// DropLocation
        ///</Summary>
        public string DropLocation { get; set; }

        ///<Summary>
        /// PickupDate
        ///</Summary>
        public DateTime PickupDate { get; set; }

        ///<Summary>
        /// PickupTime
        ///</Summary>
        public string PickupTime { get; set; }

        ///<Summary>
        /// Amount
        ///</Summary>
        public string Amount { get; set; }

        ///<Summary>
        /// Status
        ///</Summary>
        public string Status { get; set; }
    }
}
