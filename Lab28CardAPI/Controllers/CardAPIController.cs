using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Lab28CardAPI.Controllers
{
    public class CardAPIController : Controller
    {
        // GET: CardAPI
        const string userAgent = "Mozilla / 5.0(Windows NT 6.1; Win64; x64; rv: 47.0) Gecko / 20100101 Firefox / 47.0";
        // GET: API
        public ActionResult GetDeckId()
        {
            HttpWebRequest request = WebRequest.CreateHttp("https://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1");
            request.UserAgent = userAgent;
            //this is where we'd add a key and password for the API (if we needed one);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                StreamReader data = new StreamReader(response.GetResponseStream());
                JObject dataObject = JObject.Parse(data.ReadToEnd());
                ViewBag.RawData = data.ReadToEnd();
                var deckID = dataObject["deck_id"];
                ViewBag.deckId = deckID;
            }
            return View();
        }

        public ActionResult DrawFive()
        {
            HttpWebRequest request = WebRequest.CreateHttp("https://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1");
            request.UserAgent = userAgent;
            //this is where we'd add a key and password for the API (if we needed one);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                StreamReader data = new StreamReader(response.GetResponseStream());
                JObject dataObject = JObject.Parse(data.ReadToEnd());
                ViewBag.RawData = data.ReadToEnd();
                var deckID = dataObject["deck_id"];
                ViewBag.deckId = deckID;


                HttpWebRequest DrawRequest = WebRequest.CreateHttp("https://deckofcardsapi.com/api/deck/" + deckID + "/draw/?count=5");
                HttpWebResponse DrawResponse = (HttpWebResponse)DrawRequest.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader draw = new StreamReader(DrawResponse.GetResponseStream());
                    JObject drawObject = JObject.Parse(draw.ReadToEnd());
                    ViewBag.drawData = drawObject;
                }
            }
            TempData["DeckID"] = TempData["DeckID"];

            return View();
        }

        public ActionResult FiveMore()
        {
            TempData["DeckID"] = TempData["DeckID"];
            return RedirectToAction("DrawFive");
        }
    }


}
