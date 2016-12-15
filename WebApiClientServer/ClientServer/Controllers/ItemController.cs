using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using ClientServer.Models;
using System.IO;
using Newtonsoft.Json;
using System.Web.Http.Description;
using ClientServer.Auth;
using System.Web;

namespace ClientServer.Controllers
{
  public class ItemController : ApiController
  {
    public static readonly string url;
    protected virtual new UserPrincipal User
    {
      get { return HttpContext.Current.User as UserPrincipal; }
    }
    static ItemController()
    {
      url = "http://localhost:10000/api/Items/";
    }
    [Authorize]
    public IQueryable<Item> GetItems()
    {
      var sender = new RequestSender(url);
      string result = sender.SendGetRequest();
      var jsonArray = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(result);

      return jsonArray.ToObject<List<Item>>().AsQueryable();
    }

    // GET: api/Items/5
    [Authorize]
    [ResponseType(typeof(Item))]
    public IHttpActionResult GetItem(int id)
    {
      var sender = new RequestSender(url + id);
      string result = sender.SendGetRequest();
      var json = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(result);
      Item item = json.ToObject<Item>();

      if (item == null)
      {
        return NotFound();
      }

      return Ok(item);
    }

    // PUT: api/Items/5
    [Authorize]
    [ResponseType(typeof(void))]
    public IHttpActionResult PutItem(int id, Item item)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != item.Id)
      {
        return BadRequest();
      }

      var sender = new RequestSender(url + id);
      string result = sender.SendPutRequest(item);

      return StatusCode(HttpStatusCode.NoContent);
    }

    // POST: api/Items
    [Authorize]
    [ResponseType(typeof(Item))]
    public IHttpActionResult PostItem(Item item)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var sender = new RequestSender(url);
      string result = sender.SendPostRequest(item);

      return CreatedAtRoute("DefaultApi", new { id = item.Id }, item);
    }

    // DELETE: api/Items/5
    [Authorize]
    [ResponseType(typeof(Item))]
    public IHttpActionResult DeleteItem(int id)
    {
      var sender = new RequestSender(url + id);
      string result = sender.SendGetRequest();
      var json = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(result);
      Item item = json.ToObject<Item>();
      if (item == null)
      {
        return NotFound();
      }

      result = sender.SendDeleteRequest();

      return Ok(item);
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
    }

  }
}
