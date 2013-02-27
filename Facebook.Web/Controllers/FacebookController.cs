using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Facebook.Controllers
{
    public class FacebookController : Controller
    {
        //
        // GET: /Facebook/
        public ActionResult ProcessResponse()
        {
            var code = Request["code"];
            var state = Request["state"];

            //if (code != null && Session["state"] != null && Session["state"] == state)
            //if (code != null && (Session["access_token"] == null || Session["access_token_received"] == null || Session["access_token_expires"] == null))
            if (code != null)
            {
                string clientId = ConfigurationManager.AppSettings["FacebookAppId"];
                string appSecret = ConfigurationManager.AppSettings["FacebookAppSecret"];
                // Always use same redirection url for all api calling 
                // ref. http://stackoverflow.com/questions/5189292/facebook-server-side-flow-problem-for-access-token-error-validating-verificati
                string facebookRedirectionUrl = ConfigurationManager.AppSettings["FacebookRedirectionUrl"];
                var facebookGetAccessTokenUrl = string.Format("https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}", clientId, Server.UrlEncode(facebookRedirectionUrl), appSecret, code);

                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(facebookGetAccessTokenUrl);
                    request.Method = "GET";
                    request.ContentType = "";
                    using (Stream stream = request.GetResponse().GetResponseStream())
                    {
                        using (StreamReader streamReader = new StreamReader(stream))
                        {
                            NameValueCollection response = HttpUtility.ParseQueryString(streamReader.ReadToEnd());
                            Session["access_token"] = response["access_token"];
                            Session["access_token_received"] = DateTime.Now;
                            Session["access_token_expires"] = Convert.ToInt32(response["expires"]);
                            //Response.Write("<br />access_token" + Session["access_token"]);
                            //Response.Write("<br />access_token_received" + Session["access_token_received"]);
                            //Response.Write("<br />access_token_expires" + Session["access_token_expires"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Response.Write(ex.Message);
                    //Response.Write("<br />");
                    //Response.Write(ex.StackTrace);
                }

                //Response.Write("<br />");
                //Response.Write(facebookGetAccessTokenUrl);
            }
            //return View();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ProcessJSResponse(string accessToken, int expiresIn, string signedRequest, string userID)
        {
            MD5 md5 = MD5.Create();
            Session["state"] = md5.ComputeHash(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())).ToString();
            string clientId = ConfigurationManager.AppSettings["FacebookAppId"];
            // Always use same redirection url for all api calling 
            // ref. http://stackoverflow.com/questions/5189292/facebook-server-side-flow-problem-for-access-token-error-validating-verificati
            string facebookRedirectionUrl = ConfigurationManager.AppSettings["FacebookRedirectionUrl"];
            string scope = ConfigurationManager.AppSettings["FacebookScope"];
            var facebookLoginUrl = string.Format("https://www.facebook.com/dialog/oauth?client_id={0}&redirect_uri={1}&state={2}&scope={3}", clientId, Server.UrlEncode(facebookRedirectionUrl), Session["state"], scope);
            return Redirect(facebookLoginUrl);

            //Session["access_token"] = accessToken;
            //Session["access_token_received"] = DateTime.Now;
            //Session["access_token_expires"] = expiresIn;
            ////return View();
            //return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            MD5 md5 = MD5.Create();
            Session["state"] = md5.ComputeHash(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())).ToString();
            string clientId = ConfigurationManager.AppSettings["FacebookAppId"];
            // Always use same redirection url for all api calling 
            // ref. http://stackoverflow.com/questions/5189292/facebook-server-side-flow-problem-for-access-token-error-validating-verificati
            string facebookRedirectionUrl = ConfigurationManager.AppSettings["FacebookRedirectionUrl"];
            string scope = ConfigurationManager.AppSettings["FacebookScope"];
            var facebookLoginUrl = string.Format("https://www.facebook.com/dialog/oauth?client_id={0}&redirect_uri={1}&state={2}&scope={3}", clientId, Server.UrlEncode(facebookRedirectionUrl), Session["state"], scope);
            return Redirect(facebookLoginUrl);
        }

        public ActionResult GetUserDetails(string userId)
        {
            string accessToken = Session["access_token"].ToString();
            string expiry = Session["access_token_expires"].ToString();
            string fbGraphUrl = "https://graph.facebook.com";
            string friendFields = "bio,name,location,quotes,picture.width(200).height(200),statuses.limit(1).fields(message)";
            string meFriendsPath = string.Format("{0}/{1}?access_token={2}&fields={3}", fbGraphUrl, userId, accessToken, friendFields);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(meFriendsPath);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string responseString;
            using (Stream stream = request.GetResponse().GetResponseStream())
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    responseString = streamReader.ReadToEnd();
                }
            }

            return Content(responseString);
        }
    }
}
