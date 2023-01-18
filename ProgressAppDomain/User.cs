using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressApp
{
    public class User
    {
        public string Username { get; set; }
        public ProjectItem RootProject { get; set; }
        public int ID { get; set; }

        public User(string username, int id=0, int rooProjectId=0)
        {
            Username = username;
            ID = id;
            RootProject = new ProjectItem(username, null, rooProjectId);
        }
    }
}
