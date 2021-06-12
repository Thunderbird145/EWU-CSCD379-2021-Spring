using System;
using System.Collections.Generic;
using SecretSanta.Data;

namespace SecretSanta.Business
{
    public class GroupRepository : IGroupRepository
    {
        public Group Create(Group item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            item.Users.Add(new User() {
                FirstName = "ffd",
                LastName = "Frido"
            }); 

            using var dbContext = new DbContext();
            dbContext.Groups.Add(item);
            dbContext.SaveChangesAsync();

            return item;
        }

        public Group? GetItem(int id)
        {
            using var dbContext = new DbContext();

            Group group = dbContext.Groups.Find(id);


            return group;
        }

        public ICollection<Group> List()
        {
            using DbContext dbContext = new DbContext();
            List<Group> groupList = new List<Group>();
            foreach (var group in dbContext.Groups)
            {
                groupList.Add(group);
            }
            return groupList;
        }

        public bool Remove(int id)
        {
            try {
                using var dbContext = new DbContext();
                Group group = dbContext.Groups.Find(id);
                dbContext.Groups.Remove(group);
                dbContext.SaveChangesAsync();
                return true;
            } catch {
                return false;
            }
        }

        public void Save(Group item)
        {
            if (item is null)
            {
                throw new System.ArgumentNullException(nameof(item));
            }

            using (var context = new DbContext())
            {
                var group = context.Groups.Find(item.Id);
                group.Name = item.Name;
                group.Users = item.Users;
                context.SaveChangesAsync();
            }
            
        }

        public User addUser(int userId, int groupId) 
        {
            using var dbContext = new DbContext();

            Group group = dbContext.Groups.Find(groupId);

            User user = dbContext.Users.Find(userId);

            group.Name = "bill";

            group.Users.Add(user);

            dbContext.SaveChangesAsync();

            return new User();
        }

        public AssignmentResult GenerateAssignments(int groupId)
        {
            if (!MockData.Groups.TryGetValue(groupId, out Group? group))
            {
                return AssignmentResult.Error("Group not found");
            }

            Random random = new();
            var groupUsers = new List<User>(group.Users);

            if (groupUsers.Count < 3)
            {
                return AssignmentResult.Error($"Group {group.Name} must have at least three users");
            }

            var users = new List<User>();
            //Put the users in a random order
            while(groupUsers.Count > 0)
            {
                int index = random.Next(groupUsers.Count);
                users.Add(groupUsers[index]);
                groupUsers.RemoveAt(index);
            }

            //The assignments are created by linking the current user to the next user.
            group.Assignments.Clear();
            for(int i = 0; i < users.Count; i++)
            {
                int endIndex = (i + 1) % users.Count;
                group.Assignments.Add(new Assignment(users[i], users[endIndex]));
            }
            return AssignmentResult.Success();
        }
    }
}
