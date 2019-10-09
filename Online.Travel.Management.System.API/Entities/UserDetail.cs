namespace Online.Travel.Management.System.API.Entities
{
    using global::System;
    using global::System.ComponentModel.DataAnnotations;
    using global::System.ComponentModel.DataAnnotations.Schema;

    public class UserDetail
    {
        ///<Summary>
        /// Id
        ///</Summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        ///<Summary>
        /// FirstName
        ///</Summary>
        public string FirstName { get; set; }

        ///<Summary>
        /// LastName
        ///</Summary>
        public string LastName { get; set; }

        ///<Summary>
        /// Gender
        ///</Summary>
        public string Gender { get; set; }

        ///<Summary>
        /// PhoneNumber
        ///</Summary>
        public int phoneNumber { get; set; }

        ///<Summary>
        /// DateOfBirth
        ///</Summary>
        public DateTime DateOfBirth { get; set; }

        ///<Summary>
        /// Email
        ///</Summary>
        public string Email { get; set; }

        ///<Summary>
        /// Password
        ///</Summary>
        public string Password { get; set; }

        ///<Summary>
        /// ConfirmPassword
        ///</Summary>
        public string ConfirmPassword { get; set; }

        ///<Summary>
        /// RoleId
        ///</Summary>
        public int RoleId { get; set; }

        ///<Summary>
        /// VehicleNumber
        ///</Summary>
        public string VehicleNumber { get; set; }

        ///<Summary>
        /// License
        ///</Summary>
        public string License { get; set; }

        ///<Summary>
        /// Insurance
        ///</Summary>
        public string Insurance { get; set; }

        ///<Summary>
        /// Permit
        ///</Summary>
        public string Permit { get; set; }

        ///<Summary>
        /// Registration
        ///</Summary>
        public string Registration { get; set; }

        ///<Summary>
        /// Description
        ///</Summary>
        public string Description { get; set; }

        ///<Summary>
        /// Status
        ///</Summary>
        public string Status { get; set; }
    }
}