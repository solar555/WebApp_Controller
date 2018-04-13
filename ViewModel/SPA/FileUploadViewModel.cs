using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebApp_Controller.ViewModels.SPA
{
    public class FileUploadViewModel
    {
        public HttpPostedFileBase fileUpload { get; set; }
    }
}
