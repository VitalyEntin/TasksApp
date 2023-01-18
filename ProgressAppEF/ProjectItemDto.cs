using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressApp
{
    public class ProjectItemDto
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int? ParentProjectId { get; set; }
        public ProjectItemDto(ProjectItem item)
        {
            ID = item.ID;
            Title = item.Title;
            Description = item.Description;
            Status = item.ItemStatus.ToString();
            ParentProjectId = item.ParentProject?.ID;
        }
        public ProjectItemDto(string title, string description="", string status="Planned", int? parentProjectId=null)
        {
            Title = title;
            Description = description;
            Status = status;
            ParentProjectId = parentProjectId;
        }
    }
}
