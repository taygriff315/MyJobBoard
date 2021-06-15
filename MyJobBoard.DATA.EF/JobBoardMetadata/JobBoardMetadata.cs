using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.DATA.EF/*.JobBoardMetadata*/
{
   
    public class UserDetailMetadata
    {
        [StringLength(128, ErrorMessage ="The user ID cannot contain more than 128 characters")]
        [Display(Name ="Username")]
        [Required(ErrorMessage ="A username is required")]
        public string UserId { get; set; }

        [StringLength(50,ErrorMessage ="First name cannont contain more than 50 characters")]
        [Display(Name ="First Name")]
        [Required(ErrorMessage ="You must enter your first name")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Last name cannont contain more than 50 characters")]
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You must enter your last name")]
        public string LastName { get; set; }

        [StringLength(75,ErrorMessage ="File name cannot contain more than 75 characters")]
        [Display(Name ="Resume' File Name ")]
        public string ResumeFileName { get; set; }

        [StringLength(75, ErrorMessage ="File name cannon contain more than 75 characters")]
        [Display(Name ="Profile Picture")]
        public string ProfilePicture { get; set; }

    }


    [MetadataType(typeof(UserDetailMetadata))]
    public partial class UserDetail
    {
        [Display(Name="Name")]
        public string Name
        {
            get { return FirstName + " " + LastName; }
        }

    }

    public class ApplicationMetadata
    {
        [Required(ErrorMessage ="The date of the application is required")]
        [Display(Name ="Application Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ApplicationDate { get; set; }

        [StringLength(2000, ErrorMessage ="The note cannot contain more than 2000 characters")]
        [Display(Name ="Manager Notes")]
        [UIHint("Multiline Text")]
        public string ManagerNotes { get; set; }

        [Required(ErrorMessage ="Your resume' is required")]
        [Display(Name ="Resume' File Name")]
        [StringLength(75, ErrorMessage = "File name cannot contain more than 75 characters")]
        public string ResumeFileName { get; set; }
    }

    [MetadataType(typeof(ApplicationMetadata))]
    public partial class Application
    {

    }

    public class ApplicationStatusMetadata
    {
        [Display(Name ="Status")]
        [Required(ErrorMessage ="The name of the status is required")]
        [StringLength(50, ErrorMessage ="The name of the status cannot be more that 50 characters")]
        public string StatusName { get; set; }

        [Display(Name ="Status Description")]
        [StringLength(250,ErrorMessage ="The description cannot be more than 250 characters")]
        [UIHint("Multiline Text")]
        public string StatusDescription { get; set; }
    }
    
    [MetadataType(typeof(ApplicationStatusMetadata))]
    public partial class ApplicationStatus
    {

    }

    public class PositionMetadata
    {
        [Required(ErrorMessage ="The title of the job is required")]
        [Display(Name ="Job Title")]
        [StringLength(50,ErrorMessage ="The job title cannot be longer than 50 characters")]
        public string Title { get; set; }

        [Display(Name ="Job Description")]
        [UIHint("Multiline Text")]
        public string JobDescription { get; set; }
       

    }

    [MetadataType(typeof(PositionMetadata))]
    public partial class Position
    {

    }

    public class OpenPositionMetadata
    {

        [Display(Name ="Salary")]
        [DisplayFormat(NullDisplayText = "[-N/A-]", DataFormatString = "{0:c}")]
        [Range(0, double.MaxValue, ErrorMessage = "Value must be a valid number. 0 or larger")]
        [Required(ErrorMessage ="The salary of the open position is required")]
        public decimal Pay { get; set; }

    }


    [MetadataType(typeof(OpenPositionMetadata))]
    public partial class OpenPosition
    {
        
        public bool HasApplied { get; set; }

    }

    public class LocationMetadata
    {

        [Required(ErrorMessage ="The store number is required")]
        [Range(0,9999999999)]
        public int StoreNumber { get; set; }

        [Required(ErrorMessage ="The City field is required")]
        [StringLength(50, ErrorMessage ="The City name cannot be more than 50 charcters long")]
        public string City { get; set; }

        [Required(ErrorMessage = "The State field is required")]
        [StringLength(50, ErrorMessage = "The State name cannot be more than 50 charcters long")]
        public string State { get; set; }

        [Required(ErrorMessage ="The manager ID is required")]
        [StringLength(128,ErrorMessage ="Manager ID cannot contain more than 128 characters")]
        [Display(Name ="Manager ID")]
        public int ManagerId { get; set; }

    }

    [MetadataType(typeof(LocationMetadata))]
    public partial class Location
    {
        [Display(Name = "Location")]
        public string Address
        {
            get { return City + ", " + State; }
        }
    }


}
