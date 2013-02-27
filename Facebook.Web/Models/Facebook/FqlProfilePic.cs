using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facebook.Web.Models.Facebook
{
    public class FqlProfilePic
    {
        /// <summary>
        /// Height of the requested profile picture, in pixels
        /// </summary>
        public string height { get; set; }
        /// <summary>
        /// ID of the user
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// Whether the profile picture has been set (i.e. the profile picture is the default picture)
        /// </summary>
        public bool is_silhouette { get; set; }
        /// <summary>
        /// actual height of the returned profile picture, in pixels (this may or may not be the same as `height`)
        /// </summary>
        public string real_height { get; set; }
        /// <summary>
        /// Actual width of the returned profile picture, in pixels (this may or may not be the same as `width`)
        /// </summary>
        public string real_width { get; set; }
        /// <summary>
        /// URL pointing to the returned picture
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// Width of the requested profile picture, in pixels
        /// </summary>
        public string width { get; set; }
    }
}