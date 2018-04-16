using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo.Products.CustomFilters
{
    public class AuthLogAttribute : AuthorizeAttribute
    {
        public string View { get; set; }

        public AuthLogAttribute()
        {
            View = "AuthorizeFailed";
        }
        
        // check for authorization
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            IsUserAuthorized(filterContext);
        }

        private void IsUserAuthorized(AuthorizationContext filterContext)
        {
            if (filterContext.Result == null)
                return;

            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var vr = new ViewResult();
                vr.ViewName = View;

                ViewDataDictionary dict = new ViewDataDictionary();
                dict.Add("Message", "Sorry you are not authorized to perform this Action");

                vr.ViewData = dict;

                var result = vr;

                filterContext.Result = result;
            }

        }
    }
}