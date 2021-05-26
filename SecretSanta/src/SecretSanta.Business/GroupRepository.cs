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

            MockData.Groups[item.Id] = item;
            return item;
        }

        public Group? GetItem(int id)
        {
            if (MockData.Groups.TryGetValue(id, out Group? user))
            {
                return user;
            }
            return null;
        }

        public ICollection<Group> List()
        {
            return MockData.Groups.Values;
        }

        public bool Remove(int id)
        {
            return MockData.Groups.Remove(id);
        }

        public void Save(Group item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            MockData.Groups[item.Id] = item;
        }
        
        public AssignmentResult GenerateAssignments(int id) {
            Group group = GetItem(id);
            if (group is null) {
                return AssignmentResult.Error("Group not found.");
            }
            if (group.Users.Count <= 2) {
                return AssignmentResult.Error("Only 2 users in group, need more users.");
            }
            List<User> tempUsers = group.Users;
            Random rng = new Random();  
            int n = tempUsers.Count;  
            while (n > 1) {  //Shuffle logic from a post on StackOverflow
                n--;  
                int k = rng.Next(n + 1);  
                User value = tempUsers[k];  
                tempUsers[k] = tempUsers[n];  
                tempUsers[n] = value;  
            }  
            group.Assignments.Clear();
            for (int ix = 0; ix < tempUsers.Count; ix++) {
                if (ix == group.Users.Count - 1) {
                    group.Assignments.Add(new Assignment(tempUsers[tempUsers.Count - 1], tempUsers[0]));
                } else {
                    group.Assignments.Add(new Assignment(tempUsers[ix], tempUsers[ix + 1]));
                }
            }
            return AssignmentResult.Success();
        }
    }
}
