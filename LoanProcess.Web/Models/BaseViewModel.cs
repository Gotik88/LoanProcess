using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelBindingContext = System.Web.ModelBinding.ModelBindingContext;

namespace MvcAuthentication.Models
{
    public partial class BaseViewModel
    {
        public virtual void BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
        }


        public virtual int Id { get; set; }
    }
}