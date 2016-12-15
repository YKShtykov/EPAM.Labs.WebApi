using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using WebApi2.Auth;

namespace WebApi2.Controllers
{
    public class HomeController : Controller
    {
    public readonly static ServerRepository repository = new ServerRepository();
    // GET: Home
    public ActionResult Index()
        {
            return View();
        }

    [HttpPost]
    public ActionResult Login(string codeNumber, string password)
    {
      var server = repository.FindByPredicate(s=>s.CodeNumber == codeNumber && s.Password == password);
      //if (ReferenceEquals(server, null)) return View();

      ServerPrincipalSerializeModel serializeModel = new ServerPrincipalSerializeModel();
      serializeModel.codeNumber = server.CodeNumber;

      JavaScriptSerializer serializer = new JavaScriptSerializer();

      string serverData = serializer.Serialize(serializeModel);

      FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
               1,
               codeNumber,
               DateTime.Now,
               DateTime.Now.AddDays(1),
               false,
               serverData);

      string encTicket = FormsAuthentication.Encrypt(authTicket);
      HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
      Response.Cookies.Add(faCookie);
      return Json(new { faCookie.Value });
    }
    public ActionResult Logout()
    {
      var httpCookie = HttpContext.Response.Cookies[".ASPXAUTH"];
      if (httpCookie != null)
      {
        httpCookie.Value = null;
      }
      return View("Index");
    }
  }
}