using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SecretSanta.Web.Api;

namespace SecretSanta.Web.Tests.Api {


    public class TestableUsersClient : IUsersClient
    {
        public List<int> DeleteUserReturnValue {get; set;} = new();
        public int DeleteInvocationCount {get; set;}
        public Task DeleteAsync(int id)
        { 
            DeleteInvocationCount++;
            DeleteUserReturnValue.Remove(id);
            return Task.FromResult<ICollection<int>?>(DeleteUserReturnValue);
        }


        public Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public List<FullUser>? GetAllUsersReturnValue {get; set;} = new();
        public int GetAllAsyncInvocationCount {get; set;}
        public Task<ICollection<FullUser>?> GetAllAsync()
        {
            GetAllAsyncInvocationCount++;
            return Task.FromResult<ICollection<FullUser>?>(GetAllUsersReturnValue);
        }

        public Task<ICollection<FullUser>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<FullUser> GetAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<FullUser> GetAsync(int id, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public int PostAsyncInvocationCount {get; set;}
        public List<FullUser> PostAsyncInvokedParameters {get; } = new();

        public Task<FullUser> PostAsync(FullUser fUser)
        {
            PostAsyncInvocationCount++;
            PostAsyncInvokedParameters.Add(fUser);
            return Task.FromResult(fUser);
        }

        public Task<FullUser> PostAsync(FullUser fUser, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }


        public int PutAsyncInvocationCount { get; set; }
        public List<NameUser> PutAsyncInvokedParameters { get; set; } = new();
        public Task PutAsync(int id, NameUser updateUser)
        {
            PutAsyncInvocationCount++;

            PutAsyncInvokedParameters[id].FirstName = updateUser.FirstName;
            PutAsyncInvokedParameters[id].LastName = updateUser.LastName;

            return Task.FromResult<ICollection<NameUser>?>(PutAsyncInvokedParameters);
        }

        public Task PutAsync(int id, NameUser updateUser, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}