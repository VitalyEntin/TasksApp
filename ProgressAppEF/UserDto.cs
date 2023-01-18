using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgressApp
{
    [Table("Users")]
    public class UserDto
    {
        [Required]       
        public string Username { get; set; }
        [Key]
        public int Id { get; set; }
        public int RootProjectId { get; set; }
        public UserDto(string username)
        {
            Username = username;
        }
    }
}
