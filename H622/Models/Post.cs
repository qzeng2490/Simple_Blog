using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace H622.Models
{
    public class Post
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        public string Urlslug { get; set; }
        public string Content { get; set; }
        public virtual System.Nullable<DateTime> CreateTime { get; set; }
        public virtual System.Nullable<DateTime> LastModified { get; set; }
        public virtual IList<Tag> Tags { get; set; }
    }
}