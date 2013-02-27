using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facebook.Web.Models.Facebook
{
    public class FqlUser
    {
        //TODO: add all fields. Please refer to http://developers.facebook.com/docs/reference/fql/user

        /// <summary>
        /// More information about the user being queried
        /// </summary>
        public string about_me { get; set; }
        /// <summary>
        /// The user's activities
        /// </summary>
        public string activities { get; set; }
        /// <summary>
        /// The user's birthday. The format of this date varies based on the user's locale
        /// </summary>
        public string birthday { get; set; }
        /// <summary>
        /// The user's birthday in MM/DD/YYYY format
        /// </summary>
        public string birthday_date { get; set; }
        /// <summary>
        /// A public string containing the user's primary Facebook email address. 
        /// If the user shared his or her primary email address with you, this address also appears in the email field (see below). 
        /// Facebook recommends you query the email field to get the email address shared with your application
        /// </summary>
        public string contact_email { get; set; }
        /// <summary>
        /// The current address of the user
        /// </summary>
        public Address current_address { get; set; }
        /// <summary>
        /// The user's current location
        /// </summary>
        public Address current_location { get; set; }
        /// <summary>
        /// A public string containing the user's primary Facebook email address or the user's proxied email address, whichever address the user granted your application. 
        /// Facebook recommends you query this field to get the email address shared with your application
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// The user's first name
        /// </summary>
        public string first_name { get; set; }
        /// <summary>
        /// Count of all the user's friends
        /// </summary>
        public string friend_count { get; set; }
        /// <summary>
        /// The user's number of outstanding friend requests
        /// </summary>
        public string friend_request_count { get; set; }
        /// <summary>
        /// The user's hometown (and state)
        /// </summary>
        Address hometown_location { get; set; }
        /// <summary>
        /// The user's last name
        /// </summary>
        public string last_name { get; set; }
        /// <summary>
        /// Count of all the pages this user has liked
        /// </summary>
        public string likes_count { get; set; }
        /// <summary>
        /// The two-letter language code and the two-letter country code representing the user's [locale](http://www.facebook.com/translations/FacebookLocales.xml). 
        /// Country codes are taken from the [ISO 3166 alpha 2 code](http://www.iso.org/iso/iso-3166-1_decoding_table.htmllist)
        /// </summary>
        public string locale { get; set; }
        /// <summary>
        /// The user's middle name
        /// </summary>
        public string middle_name { get; set; }
        /// <summary>
        /// The user's full name
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// The URL to the medium-sized profile picture for the user being queried. 
        /// The image can have a maximum width of 100px and a maximum height of 300px. 
        /// This URL may be blank
        /// </summary>
        public string pic { get; set; }
        /// <summary>
        /// The URL to the largest-sized profile picture for the user being queried. 
        /// The image can have a maximum width of 200px and a maximum height of 600px. 
        /// This URL may be blank
        /// </summary>
        public string pic_big { get; set; }
        /// <summary>
        /// The URL to the largest-sized profile picture for the user being queried. 
        /// The image can have a maximum width of 200px and a maximum height of 600px, and is overlaid with the Facebook favicon. 
        /// This URL may be blank
        /// </summary>
        public string pic_big_with_logo { get; set; }
        /// <summary>
        /// The URL to the small-sized profile picture for the user being queried. 
        /// The image can have a maximum width of 50px and a maximum height of 150px. 
        /// This URL may be blank
        /// </summary>
        public string pic_small { get; set; }
        /// <summary>
        /// The URL to the small-sized profile picture for the user being queried. 
        /// The image can have a maximum width of 50px and a maximum height of 150px, and is overlaid with the Facebook favicon. 
        /// This URL may be blank
        /// </summary>
        public string pic_small_with_logo { get; set; }
        /// <summary>
        /// The URL to the square profile picture for the user being queried. 
        /// The image can have a maximum width and height of 50px. 
        /// This URL may be blank
        /// </summary>
        public string pic_square { get; set; }
        /// <summary>
        /// The URL to the square profile picture for the user being queried. 
        /// The image can have a maximum width and height of 50px, and is overlaid with the Facebook favicon. 
        /// This URL may be blank
        /// </summary>
        public string pic_square_with_logo { get; set; }
        /// <summary>
        /// The URL to the medium-sized profile picture for the user being queried. 
        /// The image can have a maximum width of 100px and a maximum height of 300px, and is overlaid with the Facebook favicon. 
        /// This URL may be blank
        /// </summary>
        public string pic_with_logo { get; set; }
        /// <summary>
        /// The time the profile was most recently updated (UNIX timestamp). 
        /// If the user's profile has not been updated in the past three days, this value will be 0
        /// </summary>
        public string profile_update_time { get; set; }
        /// <summary>
        /// The URL to a user's profile
        /// </summary>
        public string profile_url { get; set; }
        /// <summary>
        /// The user's favorite quotes
        /// </summary>
        public string quotes { get; set; }
        /// <summary>
        /// The type of relationship for the user being queried
        /// </summary>
        public string relationship_status { get; set; }
        /// <summary>
        /// The user's religion
        /// </summary>
        public string religion { get; set; }
        /// <summary>
        /// The user's gender
        /// </summary>
        public string sex { get; set; }
        /// <summary>
        /// The user ID of the partner (for example, husband, wife, boyfriend, girlfriend)
        /// </summary>
        public string significant_other_id { get; set; }
        /// <summary>
        /// The user's current status
        /// </summary>
        public FqlStatus status { get; set; }
        /// <summary>
        /// The user's total number of subscribers
        /// </summary>
        public string subscriber_count { get; set; }
        /// <summary>
        /// The user ID
        /// </summary>
        public string uid { get; set; }
        /// <summary>
        /// The user's username
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// Indicates whether or not Facebook has verified the user
        /// </summary>
        public bool verified { get; set; }
        /// <summary>
        /// The number of Wall posts for the user being queried
        /// </summary>
        public string wall_count { get; set; }
        /// <summary>
        /// The website
        /// </summary>
        public string website { get; set; }
    }

    public class Address
    {
        /// <summary>
        /// Street of the location
        /// </summary>
        public string street { get; set; }
        /// <summary>
        /// City of the location
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// State of the location
        /// </summary>
        public string state { get; set; }
        /// <summary>
        /// Country of the location
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// Zip code of the location
        /// </summary>
        public string zip { get; set; }
        /// <summary>
        /// Latitude of the location
        /// </summary>
        public string latitude { get; set; }
        /// <summary>
        /// Longitude of the location
        /// </summary>
        public string longitude { get; set; }
        /// <summary>
        /// ID of the location
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// Name of the location
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// ID of the parent location of this location
        /// </summary>
        public string located_in { get; set; }
    }

    public class FqlStatus
    {
        /// <summary>
        /// Message of the status
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// Time of when the status was posted (UNIX timestamp)
        /// </summary>
        public string timestamp { get; set; }
        /// <summary>
        /// ID of the status
        /// </summary>
        public string status_id { get; set; }
        /// <summary>
        /// URL of the status
        /// </summary>
        public string source { get; set; }
        /// <summary>
        /// User's ID
        /// </summary>
        public string uid { get; set; }
        /// <summary>
        /// Number of comments on the status
        /// </summary>
        public string comment_count { get; set; }

    }
}