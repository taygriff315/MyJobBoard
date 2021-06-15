using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyJobBoard.UI.MVC.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "The name field is required.. This isn't rocket science, ya know?")]
        public string Name { get; set; }

        public string Subject { get; set; }
        [Required(ErrorMessage = "The message is required.. This isn't rocket science, ya know?")]
        [UIHint("MultilineText")] //Creates the input field as a TextArea rather than a textbox
        public string Message { get; set; }
        [Required(ErrorMessage = "The email address is required.. This isn't rocket science, ya know?")]
        [Display(Name = "Your Email")]
        [EmailAddress(ErrorMessage = "Email address was an improper format.. This isn't rocket science, ya know?")]
        public string EmailAddress { get; set; }


    }
}