using System.ComponentModel.DataAnnotations;

namespace ClientMilkTeamPage.DTO
{
    public class Order
    {
        public int OrderID { get; set; }
        [Required(ErrorMessage = "Type of order is required")]
        [Display(Name = "Type of Order")]
        public string TypeOrder { get; set; }

        [Display(Name = "Reason")]
        [StringLength(500, ErrorMessage = "Reason content cannot exceed 500 characters")]
        public string ReasonContent { get; set; }
        public bool? Status { get; set; }
        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [CustomValidation(typeof(Order), nameof(ValidateEndDate))]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Ship address is required")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Ship address must be between 5 and 200 characters")]
        [Display(Name = "Ship Address")]
        public string ShipAddress { get; set; }

        [Required(ErrorMessage = "User is required")]
        public int UserID { get; set; }
        public User User { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<TaskUser> TaskUsers { get; set; }

        public static ValidationResult ValidateEndDate(DateTime endDate, ValidationContext context)
        {
            var instance = (Order)context.ObjectInstance;
            if (endDate < instance.StartDate)
            {
                return new ValidationResult("End date must be after start date");
            }
            return ValidationResult.Success;
        }
    }
}
