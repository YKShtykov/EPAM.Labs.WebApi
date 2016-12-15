using ClientServer.Auth;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace ClientServer
{
  public class WebApiApplication : System.Web.HttpApplication
  {
    protected void Application_Start()
    {
      Database.SetInitializer(new UsersInitializer());
      GlobalConfiguration.Configure(WebApiConfig.Register);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      BundleConfig.RegisterBundles(BundleTable.Bundles);
    }

    protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
    {
      HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

      if (authCookie != null&&authCookie.Value!="")
      {
        FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

        JavaScriptSerializer serializer = new JavaScriptSerializer();

        UserPrincipalSerializeModel serializeModel = serializer.Deserialize<UserPrincipalSerializeModel>(authTicket.UserData);

        UserPrincipal newUser = new UserPrincipal(authTicket.Name);
        newUser.Login = serializeModel.Login;

        HttpContext.Current.User = newUser;
      }
    }
  }
}
