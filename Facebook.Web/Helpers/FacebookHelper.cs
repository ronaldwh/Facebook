using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Facebook.Web.Helpers
{
    public class FacebookHelper
    {
        public static string Scope()
        {
            return string.IsNullOrEmpty(ConfigurationManager.AppSettings["FacebookScope"]) ? string.Empty : ConfigurationManager.AppSettings["FacebookScope"];
        }
    }
}