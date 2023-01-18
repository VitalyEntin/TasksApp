using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressApp
{
    public class ProjectItem
    {
        public enum Status { Planned, InProgress, Completed, Failed }

        private Status _itemStatus;
        private List<ProjectItem> _subItems;

        private int _id;
        private ProjectItem? _parentProject;

        public bool HasSubItems
        {
            get { return _subItems != null && _subItems.Count > 0; }
        }
        public string Title { set; get; }
        public string Description { set; get; }
        public Status ItemStatus
        {
            get
            {
                Status status = Status.Failed;

                bool isCompleted = true, inProgress = false;

                if (SubItems.Count > 0)
                {
                    for (int i = 0; i < SubItems.Count; i++)
                    {
                        if (SubItems[i].ItemStatus == Status.InProgress || SubItems[i].ItemStatus == Status.Completed)
                            inProgress = true;
                        if (SubItems[i].ItemStatus == Status.Planned || SubItems[i].ItemStatus == Status.InProgress)
                            isCompleted = false;
                    }
                    if (!inProgress && !isCompleted) status = Status.Planned;
                    if (inProgress && !isCompleted) status = Status.InProgress;
                    if (inProgress && isCompleted) status = Status.Completed;
                }
                else status = _itemStatus;

                return status;
            }
            private set { _itemStatus = value; }
        }
        public List<ProjectItem> SubItems => _subItems;
        public int ID => _id;
        public ProjectItem? ParentProject => _parentProject;

        public ProjectItem(string itemTitle, ProjectItem? parentProject, int id = 0, Status itemStatus = Status.Planned, string description = "")
        {
            Title = itemTitle;
            _id = id;
            _itemStatus = itemStatus;
            _subItems = new List<ProjectItem>();
            _parentProject = parentProject;
            Description = description;
        }
        public void AddSubItem(string itemTitle, int id)
        {
            _subItems.Add(new ProjectItem(itemTitle, this, id));
        }
        public void SetItemStatus(Status status)
        {
            if (!HasSubItems) _itemStatus = status;
        }
        public void SetParentProject(ProjectItem? parentProject)
        {
            _parentProject = parentProject;
        }

    }

    public class ProjectItemManager
    {
        /*
        public static User Vitaly = new User("Vitaly");
        public static List<ProjectItem> GenerateItems()
        {
            Vitaly.RootProject.AddSubItem("Clean Room");
            Vitaly.RootProject.AddSubItem("Build door");
            Vitaly.RootProject.AddSubItem("Go to bank");
            Vitaly.RootProject.AddSubItem("Do sports");


            Vitaly.RootProject.SubItems[0].AddSubItem("Now!");
            Vitaly.RootProject.SubItems[0].SubItems[0].Description = "Now is now!";
            Vitaly.RootProject.SubItems[0].AddSubItem("Or later!");
            Vitaly.RootProject.SubItems[0].SubItems[1].Description = "But better not...";

            Vitaly.RootProject.SubItems[0].SubItems[0].SetItemStatus(ProjectItem.Status.InProgress);

            return RootProjectToList(Vitaly.RootProject);
        }*/

        public static List<ProjectItem> RootProjectToList(ProjectItem root)
        {
            List<ProjectItem> list = new List<ProjectItem>();
            list.Add(root);
            GetAllSubItems(root, list);
            return list;
        }
        private static void GetAllSubItems(ProjectItem root, List<ProjectItem> list)
        {
            list.AddRange(root.SubItems);
            foreach (ProjectItem item in root.SubItems)
                GetAllSubItems(item, list);
        }
    }
}
