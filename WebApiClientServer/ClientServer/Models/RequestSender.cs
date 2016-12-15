using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ClientServer.Models
{
  public class RequestSender
  {
    private string cookieValue;
    public string Url { get; set; }
    public RequestSender(string url)
    {
      Url = url;
    }
    public string SendGetRequest()
    {
      var request = (HttpWebRequest)WebRequest.Create(Url);
      CheckAutorization();
      request.CookieContainer = new CookieContainer();
      request.CookieContainer.Add(new Uri("http://localhost:32893/Home/Index"),new Cookie(".ASPXAUTH", cookieValue));
      using (var response = (HttpWebResponse)request.GetResponse())
      {
        using (var reader = new StreamReader(response.GetResponseStream()))
        {
          return reader.ReadToEnd();
        }
      }
    }

    public string SendPutRequest(Item item)
    {
      var request = (HttpWebRequest)WebRequest.Create(Url);
      request.ContentType = "application/json";
      request.Method = "PUT";
      CheckAutorization();
      request.CookieContainer.Add(new Cookie(".ASPXAUTH", cookieValue));
      using (var streamWriter = new StreamWriter(request.GetRequestStream()))
      {
        string json = JsonConvert.SerializeObject(item);

        streamWriter.Write(json);
        streamWriter.Flush();
        streamWriter.Close();
      }
      using (var response = (HttpWebResponse)request.GetResponse())
      {
        using (var reader = new StreamReader(response.GetResponseStream()))
        {
          return reader.ReadToEnd();
        }
      }
    }

    public string SendPostRequest(Item item)
    {
      var request = (HttpWebRequest)WebRequest.Create(Url);
      request.ContentType = "application/json";
      request.Method = "POST";
      CheckAutorization();
      request.CookieContainer.Add(new Cookie(".ASPXAUTH", cookieValue));
      using (var streamWriter = new StreamWriter(request.GetRequestStream()))
      {
        string json = JsonConvert.SerializeObject(item);

        streamWriter.Write(json);
        streamWriter.Flush();
        streamWriter.Close();
      }
      using (var response = (HttpWebResponse)request.GetResponse())
      {
        using (var reader = new StreamReader(response.GetResponseStream()))
        {
          return reader.ReadToEnd();
        }
      }
    }

    public string SendDeleteRequest()
    {
      var request = (HttpWebRequest)WebRequest.Create(Url);
      request.Method = "DELETE";
      CheckAutorization();
      request.CookieContainer.Add(new Cookie(".ASPXAUTH", cookieValue));
      using (var response = (HttpWebResponse)request.GetResponse())
      {
        using (var reader = new StreamReader(response.GetResponseStream()))
        {
          return reader.ReadToEnd();
        }
      }
    }
    private void CheckAutorization()
    {
      if (!string.IsNullOrEmpty(cookieValue)) return;

      Autorize();
    }

    private void Autorize()
    {
      var request = (HttpWebRequest)WebRequest.Create("http://localhost:10000/Home/Login");
      request.ContentType = "application/json";
      request.Method = "POST";

      using (var streamWriter = new StreamWriter(request.GetRequestStream()))
      {
        string json = JsonConvert.SerializeObject(new { codeNumber = "1111", password = "admin" });

        streamWriter.Write(json);
        streamWriter.Flush();
        streamWriter.Close();
      }
      using (var response = (HttpWebResponse)request.GetResponse())
      {
        using (var reader = new StreamReader(response.GetResponseStream()))
        {
          string result = reader.ReadToEnd();
          dynamic data = JObject.Parse(result);
          cookieValue = data.Value;
        }
      }
    }
  }
}