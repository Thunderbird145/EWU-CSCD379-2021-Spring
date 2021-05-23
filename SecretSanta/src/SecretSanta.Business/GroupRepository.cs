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
        
        public AssignmentResult generateAssignments(int id) {
            Group group = MockData.Groups[id];
            if (group.Users.Count <= 2) {
                return AssignmentResult.Error("Only 2 users in group, need more users.");
            }
            for (int ix = 0; ix < group.Users.Count; ix++) {
                if (ix == group.Users.Count - 1) {
                    group.Assignments.Add(new Assignment(group.Users[group.Users.Count - 1], group.Users[0]));
                }
                group.Assignments.Add(new Assignment(group.Users[ix], group.Users[ix + 1]));
            }
            return AssignmentResult.Success();
        }
    }
}
