using MyJobBoard.DATA.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyJobBoard.UI.MVC.Models
{
    public class AppViewModel
    {

        
        public Application Application { get; set; }

        public AppViewModel(Application application)
        {
            
            Application = application;

        }
    }
}