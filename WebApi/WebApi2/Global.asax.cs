﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using WebApi2.Auth;
using WebApi2.Models;

namespace WebApi2
{
  public class WebApiApplication : System.Web.HttpApplication
  {
    protected void Application_Start()
    {
      GlobalConfiguration.Configure(WebApiConfig.Register);
      Database.SetInitializer(new ItemDbInitializer());
      Database.SetInitializer(new ServerDbInitializer());
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      BundleConfig.RegisterBundles(BundleTable.Bundles);
    }

    protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
    {
      HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

      if (authCookie != null && authCookie.Value != "")
      {
        FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

        JavaScriptSerializer serializer = new JavaScriptSerializer();

        ServerPrincipalSerializeModel serializeModel = serializer.Deserialize<ServerPrincipalSerializeModel>(authTicket.UserData);

        ServerPrincipal newServer = new ServerPrincipal(authTicket.Name);
        newServer.CodeNumber= serializeModel.codeNumber;

        HttpContext.Current.User = newServer;
      }
    }
  }
}
