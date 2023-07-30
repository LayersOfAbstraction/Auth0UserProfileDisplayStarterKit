namespace Auth0UserProfileDisplayStarterKit.ViewModels
{
    public class User
    {
        public int ID { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "* First Name be between 2 to 20 characters.")]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        [Column("UserFirstName")]
        public string UserFirstName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "* Last Name be between 2 to 30 characters.")]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        [Column("UserLastName")]
        public string UserLastName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Email address must be between 3 to 30 characters.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [Column("UserContactEmail")]
        public string UserContactEmail { get; set; }

        // [Required(AllowEmptyStrings = true)]
        [Display(Name = "Phone Number")]
        [Phone()]
        [Column("UserPhoneNumber")]
        public string UserPhoneNumber { get; set; }

        [StringLength(37, ErrorMessage = "Address cannot be longer than 37 characters.")]
        [DataType(DataType.Text)]
        [Display(Name = "Address")]
        [Column("UserAddress")]
        public string UserAddress { get; set; }

        //This regular expression allows valid postcodes and not just USA Zip codes.        
        [Display(Name = "Post Code")]
        [Column("UserPostCode")]
        [DataType(DataType.PostalCode)]
        public string UserPostCode { get; set; }

        [StringLength(15, ErrorMessage = "Country cannot be longer than 15 characters.")]
        [DataType(DataType.Text)]
        [Display(Name = "Country")]
        [Column("UserCountry")]
        public string UserCountry { get; set; }

        [Phone()]
        [Display(Name = "Mobile Number")]
        [Column("UserMobileNumber")]
        public string UserMobileNumber { get; set; }

        [StringLength(3, ErrorMessage = "State cannot be longer than 3 characters.")]
        [DataType(DataType.Text)]
        [Display(Name = "State")]
        [Column("UserState")]
        public string UserState { get; set; }

        public string UserFullname => string.Format("{0} {1}", UserFirstName, UserLastName);
    }
}