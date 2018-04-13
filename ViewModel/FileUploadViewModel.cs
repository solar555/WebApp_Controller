using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp_Controller.ViewModels
{
    public class FileUploadViewModel:BaseViewModel
    {
        public HttpPostedFileBase fileUpload { set; get; }
    }
}