using System;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ketum.Web.Areas.Identity.Pages.Account.Manage
{
    public static class ManageNavPages
    {
        public static string Index => "Index";

        public static string ChangePassword => "ChangePassword";

        public static string ExternalLogins => "ExternalLogins";

        public static string PersonalData => "PersonalData";

        public static string TwoFactorAuthentication => "TwoFactorAuthentication";

        public static string IndexNavClass(ViewContext viewContext)
        {
            return PageNavClass(viewContext, Index);
        }

        public static string ChangePasswordNavClass(ViewContext viewContext)
        {
            return PageNavClass(viewContext, ChangePassword);
        }

        public static string ExternalLoginsNavClass(ViewContext viewContext)
        {
            return PageNavClass(viewContext, ExternalLogins);
        }

        public static string PersonalDataNavClass(ViewContext viewContext)
        {
            return PageNavClass(viewContext, PersonalData);
        }

        public static string TwoFactorAuthenticationNavClass(ViewContext viewContext)
        {
            return PageNavClass(viewContext, TwoFactorAuthentication);
        }

        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                             ?? Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
