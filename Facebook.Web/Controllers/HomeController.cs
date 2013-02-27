using Facebook.Web.Models;
using Facebook.Web.Models.Facebook;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Facebook.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult IndexBatch()
        {
            HomeIndexModel model = new HomeIndexModel();
            try
            {
                bool needLogin = false;
                if (Session["access_token"] == null || Session["access_token_received"] == null || Session["access_token_expires"] == null)
                {
                    needLogin = true;
                }
                else
                {
                    DateTime accessTokenReceived = (DateTime)Session["access_token_received"];
                    int secondsUntilExpired = (int)Session["access_token_expires"];
                    // Add 5 minutes buffer
                    if (DateTime.Now.AddMinutes(5) > (accessTokenReceived.AddSeconds(secondsUntilExpired)))
                    {
                        needLogin = true;
                    }
                }

                if (needLogin)
                {
                    //MD5 md5 = MD5.Create();
                    //Session["state"] = md5.ComputeHash(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())).ToString();
                    //string clientId = ConfigurationManager.AppSettings["FacebookAppId"];
                    //// Always use same redirection url for all api calling 
                    //// ref. http://stackoverflow.com/questions/5189292/facebook-server-side-flow-problem-for-access-token-error-validating-verificati
                    //string facebookRedirectionUrl = ConfigurationManager.AppSettings["FacebookRedirectionUrl"];
                    //string scope = ConfigurationManager.AppSettings["FacebookScope"];
                    //var facebookLoginUrl = string.Format("https://www.facebook.com/dialog/oauth?client_id={0}&redirect_uri={1}&state={2}&scope={3}", clientId, Server.UrlEncode(facebookRedirectionUrl), Session["state"], scope);
                    //return Redirect(facebookLoginUrl);
                }
                else
                {

                    string accessToken = Session["access_token"].ToString();
                    string expiry = Session["access_token_expires"].ToString();
                    string fbGraphUrl = "https://graph.facebook.com";
                    string meFields = "id,name,first_name,middle_name,last_name,gender,locale,languages,link,username,age_range,third_party_id,installed,timezone,updated_time,verified,bio,birthday,cover,currency,devices,education,email,hometown,interested_in,location,political,payment_pricepoints,favorite_athletes,favorite_teams,quotes,relationship_status,religion,security_settings,significant_other,video_upload_limits,website,work,picture.width(200).height(200)";
                    string friendFields = "bio,name,location,quotes,picture.width(200).height(200)";
                    string mePath = string.Format("{0}/me/?access_token={1}&fields={2}", fbGraphUrl, accessToken, meFields);
                    string meFriendsPath = string.Format("{0}/me/friends?access_token={1}&fields={2}", fbGraphUrl, accessToken, friendFields);
                    string batchMePath = string.Format("me/?fields={0}", meFields);
                    string batchMeFriendsPath = string.Format("me/friends?fields={0}", friendFields);

                    // Batch Request
                    // ATTENTION! There is a limit of 50 APIs called using batch method
                    {
                        FacebookBatchRequest batchRequest = new FacebookBatchRequest();
                        batchRequest.access_token = accessToken;
                        //Response.Write(accessToken);
                        batchRequest.batch = new List<Batch>();
                        batchRequest.batch.Add(new Batch() { method = "GET", relative_url = batchMeFriendsPath });
                        batchRequest.batch.Add(new Batch() { method = "GET", relative_url = batchMePath });

                        StringBuilder sb = new StringBuilder();
                        sb.Append("access_token=" + batchRequest.access_token);
                        sb.Append("&batch=" + JsonConvert.SerializeObject(batchRequest.batch));
                        string postDataString = sb.ToString();
                        //Response.Write(postDataString);
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(fbGraphUrl);
                        request.Method = "POST";
                        request.ContentType = "application/json";
                        using (Stream stream = request.GetRequestStream())
                        {
                            byte[] postDataBytes = UTF8Encoding.UTF8.GetBytes(postDataString);
                            stream.Write(postDataBytes, 0, postDataBytes.Length);
                        }

                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        string responseString;
                        using (Stream stream = request.GetResponse().GetResponseStream())
                        {
                            using (StreamReader streamReader = new StreamReader(stream))
                            {
                                responseString = streamReader.ReadToEnd();
                            }
                        }
                        //Response.Write(responseString);
                        List<FacebookBatchResponse> facebookBatchResponseList = JsonConvert.DeserializeObject<List<FacebookBatchResponse>>(responseString);
                        if (facebookBatchResponseList[0].code.Equals("200"))
                        {
                            model = JsonConvert.DeserializeObject<HomeIndexModel>(facebookBatchResponseList[0].body);
                        }
                        if (facebookBatchResponseList[1].code.Equals("200"))
                        {
                            model.user = JsonConvert.DeserializeObject<User>(facebookBatchResponseList[1].body);
                            foreach (var friend in model.data)
                            {
                                User significantOtherFromFriendsList = model.data.FirstOrDefault(f => f.id.Equals(model.user.significant_other.id));
                                if (significantOtherFromFriendsList != null)
                                {
                                    model.user.significant_other.picture = significantOtherFromFriendsList.picture;
                                }
                            }
                        }
                    }

                    //// Get Friends List
                    //{
                    //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(meFriendsPath);
                    //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    //    string responseString;
                    //    using (Stream stream = request.GetResponse().GetResponseStream())
                    //    {
                    //        using (StreamReader streamReader = new StreamReader(stream))
                    //        {
                    //            responseString = streamReader.ReadToEnd();
                    //        }
                    //    }
                    //    Response.Write(responseString);
                    //    model = JsonConvert.DeserializeObject<HomeIndexModel>(responseString);
                    //}
                    //// Get Personal Data
                    //{
                    //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(mePath);
                    //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    //    string responseString;
                    //    using (Stream stream = request.GetResponse().GetResponseStream())
                    //    {
                    //        using (StreamReader streamReader = new StreamReader(stream))
                    //        {
                    //            responseString = streamReader.ReadToEnd();
                    //        }
                    //    }
                    //    Response.Write(responseString);
                    //    model.user = JsonConvert.DeserializeObject<User>(responseString);
                    //    model.user.access_token = accessToken;
                    //    model.user.access_token_expiry = expiry;

                    //    // Get the significant other picture
                    //    foreach (var friend in model.data)
                    //    {
                    //        User significantOtherFromFriendsList = model.data.FirstOrDefault(f => f.id.Equals(model.user.significant_other.id));
                    //        if (significantOtherFromFriendsList != null)
                    //        {
                    //            model.user.significant_other.picture = significantOtherFromFriendsList.picture;
                    //        }
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.StackTrace);
            }
            return View(model);
        }

        public ActionResult Index()
        {
            HomeIndexFqlModel model = new HomeIndexFqlModel();
            try
            {
                bool needLogin = false;
                if (Session["access_token"] == null || Session["access_token_received"] == null || Session["access_token_expires"] == null)
                {
                    needLogin = true;
                }
                else
                {
                    DateTime accessTokenReceived = (DateTime)Session["access_token_received"];
                    int secondsUntilExpired = (int)Session["access_token_expires"];
                    // Add 5 minutes buffer
                    if (DateTime.Now.AddMinutes(5) > (accessTokenReceived.AddSeconds(secondsUntilExpired)))
                    {
                        needLogin = true;
                    }
                }

                if (needLogin)
                {
                    //MD5 md5 = MD5.Create();
                    //Session["state"] = md5.ComputeHash(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())).ToString();
                    //string clientId = ConfigurationManager.AppSettings["FacebookAppId"];
                    //// Always use same redirection url for all api calling 
                    //// ref. http://stackoverflow.com/questions/5189292/facebook-server-side-flow-problem-for-access-token-error-validating-verificati
                    //string facebookRedirectionUrl = ConfigurationManager.AppSettings["FacebookRedirectionUrl"];
                    //string scope = ConfigurationManager.AppSettings["FacebookScope"];
                    //var facebookLoginUrl = string.Format("https://www.facebook.com/dialog/oauth?client_id={0}&redirect_uri={1}&state={2}&scope={3}", clientId, Server.UrlEncode(facebookRedirectionUrl), Session["state"], scope);
                    //return Redirect(facebookLoginUrl);
                }
                else
                {

                    string accessToken = Session["access_token"].ToString();
                    string expiry = Session["access_token_expires"].ToString();
                    string fbGraphUrl = "https://graph.facebook.com";

                    // FQL
                    {
                        string myDataFieldName = "my_data";
                        string myFriendsDataFieldName = "my_friends";
                        string significantOtherDataFieldName = "my_significant_other";
                        string profilePicFieldName = "profile_pics";
                        string fqlFields = "uid, name, current_location, pic_big, quotes, about_me, status, significant_other_id, relationship_status";
                        string profilePicFqlFields = "id, width, height, url, is_silhouette, real_width, real_height";
                        string fqlRequest = string.Format("{0}/fql?q={{"
                            + "\"{3}\":\"SELECT {2} FROM user WHERE uid = me()\""
                            + ",\"{4}\":\"SELECT {2} FROM user WHERE uid IN (SELECT uid1 FROM friend WHERE uid2 = me())\""
                            + ",\"{5}\":\"SELECT {2} FROM user WHERE uid IN (SELECT significant_other_id FROM user WHERE uid = me())\""
                            + ",\"{6}\":\"SELECT {7} FROM profile_pic WHERE id IN (SELECT uid1 FROM friend WHERE uid2 = me()) AND width = 200 AND height = 200\""
                            + "}}&access_token={1}", fbGraphUrl, accessToken, fqlFields, myDataFieldName, myFriendsDataFieldName, significantOtherDataFieldName, profilePicFieldName, profilePicFqlFields);
                        //string fqlRequest = string.Format("{0}/fql?q=SELECT uid1 FROM friend WHERE uid2 = me()&access_token={1}", fbGraphUrl, accessToken);
                        //string fqlRequest = string.Format("{0}/fql?q=SELECT uid, name, current_location, pic_big, quotes, about_me, status FROM user WHERE uid = me()&access_token={1}", fbGraphUrl, accessToken);
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(fqlRequest);
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        string responseString;
                        using (Stream stream = request.GetResponse().GetResponseStream())
                        {
                            using (StreamReader streamReader = new StreamReader(stream))
                            {
                                responseString = streamReader.ReadToEnd();
                            }
                        }
                        //Response.Write(responseString);
                        UserFqlBatchResponse userFqlBatchResponse = JsonConvert.DeserializeObject<UserFqlBatchResponse>(responseString);
                        foreach (var item in userFqlBatchResponse.data)
                        {
                            if (item.name.Equals(myDataFieldName))
                            {
                                model.user = item.fql_result_set.FirstOrDefault();
                            }
                            else if (item.name.Equals(myFriendsDataFieldName))
                            {
                                model.friends = item.fql_result_set;
                            }
                            else if (item.name.Equals(significantOtherDataFieldName))
                            {
                                model.significant_other = item.fql_result_set.FirstOrDefault();
                            }

                            //else if (item.name.Equals(profilePicFieldName))
                            //{
                            //    List<ProfilePicFql> pictures = JsonConvert.DeserializeObject<List<ProfilePicFql>>(item.fql_result_set);
                            //    foreach (var friend in model.friends)
                            //    {
                            //        friend.pic = pictures.FirstOrDefault(p => p.id.Equals(friend.uid)).url;
                            //    }
                            //}

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
                //Response.Write(ex.StackTrace);
            }
            return View(model);
        }
    }
}
