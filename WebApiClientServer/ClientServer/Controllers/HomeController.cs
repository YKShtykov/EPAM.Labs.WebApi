using ClientServer.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace ClientServer.Controllers
{
  public class HomeController : Controller
  {
    public readonly static UserRepository repository = new UserRepository();

    protected virtual new UserPrincipal User
    {
      get { return HttpContext.User as UserPrincipal; }
    }
    // GET: Home
    public ActionResult Index()
    {
      return View();
    }
    [HttpGet]
    public ActionResult Login()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Login(string login, string password)
    {
      var user = repository.FindByPredicate(u => u.Login==login &&u.Password==password);
      if (ReferenceEquals(user, null)) return View();

      UserPrincipalSerializeModel serializeModel = new UserPrincipalSerializeModel();
      serializeModel.Login = user.Login;

      JavaScriptSerializer serializer = new JavaScriptSerializer();

      string userData = serializer.Serialize(serializeModel);

      FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
               1,
               login,
               DateTime.Now,
               DateTime.Now.AddDays(1),
               false,
               userData);

      string encTicket = FormsAuthentication.Encrypt(authTicket);
      HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
      Response.Cookies.Add(faCookie);

      return RedirectToAction("Index");
    }
    public ActionResult Logout()
    {
      var httpCookie = HttpContext.Response.Cookies[".ASPXAUTH"];
      if (httpCookie != null)
      {
        httpCookie.Value = null;
      }
      return RedirectToAction("Login");
    }
  }
}