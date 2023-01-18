using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProgressApp
{
    public static class DataAccess
    {
        public static void EnsureCreated()
        {
            using (var context = new ProjectItemDbContext())
            {
                context.EnsureCreated();
            }
        }
        public static IEnumerable<User> LoadAllUsers()
        {
            using (var context = new ProjectItemDbContext())
            {
                var users = new List<User>();
                var userDtos = context.Users.ToList();

                foreach (UserDto dto in userDtos)
                    users.Add(new User(dto.Username, dto.Id, dto.RootProjectId));

                return users;
            }
        }


        public static void AddUserToDb(UserDto user)
        {
            using (var context = new ProjectItemDbContext())
            {
                var rootProjectItemDto = new ProjectItemDto(new ProjectItem(user.Username, null));
                context.ProjectItems.Add(rootProjectItemDto);
                context.SaveChanges();
                user.RootProjectId = rootProjectItemDto.ID;
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
        public static void AddItemToDB(ProjectItemDto projectItemDto)
        {
            using (var context = new ProjectItemDbContext())
            {
                context.ProjectItems.Add(projectItemDto);
                context.SaveChanges();
            }
        }
        public static void DeleteItemFromDB(int itemId)
        {
            using (var context = new ProjectItemDbContext())
            {
                var item = context.ProjectItems.Find(itemId);
                if (item != null)
                {
                    context.ProjectItems.Remove(item);
                    context.SaveChanges();
                }
            }
        }
        public static void RemoveItemAndSubitems(ProjectItem itemToRemove)
        {
            using (ProjectItemDbContext context = new ProjectItemDbContext())
            {
                var item = context.ProjectItems.FirstOrDefault(i => i.ID == itemToRemove.ID);

                if (item != null)
                {
                    RemoveSubItems(context, itemToRemove.ID);
                    context.ProjectItems.Remove(item);
                    context.SaveChanges();
                }
            }
        }
        private static void RemoveSubItems(ProjectItemDbContext context, int parentId)
        {
            var subitems = context.ProjectItems.Where(i => i.ParentProjectId == parentId);
            foreach (var subitem in subitems)
            {
                RemoveSubItems(context, subitem.ID);
                context.ProjectItems.Remove(subitem);
            }
        }
        public static void SaveItemEditToDB(ProjectItem selectedProjectItem)
        {
            using (ProjectItemDbContext context = new ProjectItemDbContext())
            {
                var item = context.ProjectItems.FirstOrDefault(i => i.ID == selectedProjectItem.ID);

                item.Description = selectedProjectItem.Description;
                item.Title = selectedProjectItem.Title;
                item.Status = selectedProjectItem.ItemStatus.ToString();
                context.SaveChanges();
            }
        }
        public static IEnumerable<ProjectItem> GetAllUserProjects(int userId)
        {
            using (ProjectItemDbContext context = new ProjectItemDbContext())
            {
                int rootProjectId = context.Users.FirstOrDefault(u => u.Id == userId).RootProjectId;
                var userProjectItemDtos = GetDescendantItems(context.ProjectItems.ToList(), rootProjectId);

                var projectItems = new List<ProjectItem>();
                var dtoToProjectItemMap = new Dictionary<int, ProjectItem>();

                foreach (var dto in userProjectItemDtos)
                {
                    ProjectItem.Status projectStatus = ConvertStringToStatus(dto.Status);
                    ProjectItem projectItem = new ProjectItem(dto.Title, null, dto.ID, ConvertStringToStatus(dto.Status), dto.Description);

                    dtoToProjectItemMap[dto.ID] = projectItem;
                    projectItems.Add(projectItem);
                }
                foreach (var dto in userProjectItemDtos)
                {
                    var item = dtoToProjectItemMap[dto.ID];
                    if (dto.ParentProjectId.HasValue)
                    {
                        item.SetParentProject(dtoToProjectItemMap[dto.ParentProjectId.Value]);
                        item.ParentProject.SubItems.Add(item);
                    }
                }

                return projectItems;
            }
        }
        private static List<ProjectItemDto> GetDescendantItems(List<ProjectItemDto> projectItemDtos, int rootProjectId)
        {
            var descendants = new List<ProjectItemDto>();
            var item = projectItemDtos.FirstOrDefault(x => x.ID == rootProjectId);

            if (item != null)
            {
                descendants.Add(item);
                var children = projectItemDtos.Where(x => x.ParentProjectId == item.ID);

                foreach (var child in children)
                    descendants.AddRange(GetDescendantItems(projectItemDtos, child.ID));
            }
            return descendants;
        }
        public static ProjectItem.Status ConvertStringToStatus(string? status)
        {
            if (status == "Planned") return ProjectItem.Status.Planned;
            if (status == "InProgress") return ProjectItem.Status.InProgress;
            if (status == "Completed") return ProjectItem.Status.Completed;
            else return ProjectItem.Status.Failed;
        }
    }
}
