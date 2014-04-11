// ============================================================================
// <copyright file="UrlHelper.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================
namespace MvcAuthentication.Helpers
{
    using System.Web;

    public static class UrlHelper
    {
        public static string Resolve(string resource)
        {
            return string.Format("{0}://{1}{2}{3}",
            HttpContext.Current.Request.Url.Scheme,
            HttpContext.Current.Request.ServerVariables["HTTP_HOST"],
            (HttpContext.Current.Request.ApplicationPath.Equals("/")) ?
            string.Empty : HttpContext.Current.Request.ApplicationPath,
            resource);
        }
    }
}