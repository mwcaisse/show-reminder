using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShowReminder.Web.ViewModel
{
    public class JsonResponse<T>
    {

        public T Data { get; set; }
        
        public string ErrorMessage { get; set; }

        public bool Valid => string.IsNullOrWhiteSpace(ErrorMessage);
    }
}
