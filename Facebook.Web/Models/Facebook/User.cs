using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facebook.Web.Models.Facebook
{
    public class User
    {
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string gender { get; set; }
        public string birthday { get; set; }
        public string link { get; set; }
        public string username { get; set; }
        public Hometown hometown { get; set; }
        public Location location { get; set; }
        public string quotes { get; set; }
        public string timezone { get; set; }
        public string locale { get; set; }
        public List<Language> languages { get; set; }
        public bool verified { get; set; }
        public string updated_time { get; set; }
        public AgeRange age_range { get; set; }
        public string third_party_id { get; set; }
        public bool installed { get; set; }
        public string bio { get; set; }
        public Cover cover { get; set; }
        public Currency currency { get; set; }
        public List<Device> devices { get; set; }
        public Education education { get; set; }
        public List<string> interested_in { get; set; }
        public string political { get; set; }
        public PaymentPricePoints payment_pricepoints { get; set; }
        public List<FavoriteAthlete> favorite_athletes { get; set; }
        public List<FavoriteTeam> favorite_teams { get; set; }
        public Picture picture { get; set; }
        public string relationship_status { get; set; }
        public string religion { get; set; }
        public SecuritySettings security_settings { get; set; }
        public SignificantOther significant_other { get; set; }
        public VideoUploadLimits video_upload_limits { get; set; }
        public string website { get; set; }
        public List<Work> work { get; set; }
        public List<Friend> friends { get; set; }

        public Statuses statuses { get; set; }

        public string access_token { get; set; }
        public string access_token_expiry { get; set; }
    }

    public class Hometown
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Location
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Language
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class AgeRange
    {
        public string min { get; set; }
        public string max { get; set; }
    }

    public class Installed
    {
        public string type { get; set; }
        public string id { get; set; }
        public bool installed { get; set; }
    }

    public class Cover
    {
        public string id { get; set; }
        public string source { get; set; }
        public string offset_y { get; set; }
    }

    public class Currency
    {
        public CurrencyDetails currency { get; set; }
        public string id { get; set; }
    }

    public class CurrencyDetails
    {
        public string user_currency { get; set; }
        public decimal currency_exchange { get; set; }
        public decimal currency_exchange_inverse { get; set; }
        public decimal currency_offset { get; set; }
    }

    public class Device
    {
        public string os { get; set; }
        public string hardware { get; set; }
    }

    public class Education
    {
        public int year { get; set; }
        public string type { get; set; }
        public School school { get; set; }
    }

    public class School
    {
        public string name { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public int year { get; set; }
        public string degree { get; set; }
        public List<string> concentration { get; set; }
        public List<string> classes { get; set; }
        public List<string> with { get; set; }
    }

    public class PaymentPricePoints
    {
        public string user_price { get; set; }
        public string credits { get; set; }
        public string local_currency { get; set; }
    }

    public class FavoriteAthlete
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class FavoriteTeam
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class SecuritySettings
    {
        public SecureBrowsing secure_browsing  { get; set; }
    }

    public class SecureBrowsing
    {
        public bool enabled { get; set; }
    }

    public class SignificantOther
    {
        public string id { get; set; }
        public string name { get; set; }
        /// <summary>
        /// This will not be populated from the Facebook API, you have to populate this manually
        /// </summary>
        public Picture picture { get; set; }
    }

    public class VideoUploadLimits
    {
        public string length { get; set; }
        public string size { get; set; }
    }

    public class Work
    {
        public string employer { get; set; }
        public string position { get; set; }
        public string location { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
    }

    public class Friend
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Picture
    {
        public PictureData data { get; set; }
    }

    public class PictureData
    {
        public string url { get; set; }
        public bool is_silhouette { get; set; }
    }

    public class Statuses
    {
        public List<Status> data { get; set; }
        public Paging paging { get; set; }
    }

    public class Status
    {
        public string id { get; set; }
        public string message { get; set; }
        public string updated_time { get; set; }
        public StatusFrom from { get; set; }
        public Tags tags { get; set; }
        public Likes likes { get; set; }
    }

    public class StatusFrom
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Tags
    {
        public List<Tag> data { get; set; }
        public Paging paging { get; set; }
    }

    public class Tag
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Likes
    {
        public List<Like> data { get; set; }
        public Paging paging { get; set; }
    }

    public class Like
    {
        public string id { get; set; }
        public string data { get; set; }
    }

    public class Paging
    {
        public string previous { get; set; }
        public string next { get; set; }
    }
}