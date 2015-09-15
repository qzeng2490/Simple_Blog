using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace H622.Models
{
    public class Tag
    {

     
        [Key]
        public string name { get; set; }
        
        public virtual IList<Post> posts { get; set; }
    }
}