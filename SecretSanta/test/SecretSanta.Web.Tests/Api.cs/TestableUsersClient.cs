using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SecretSanta.Web.Api;

namespace SecretSanta.Web.Tests.Api {


    public class TestableUsersClient : IUsersClient
    {
        public Task DeleteAsync(int id)
        { 
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public List<FullUser>? GetAllUsersReturnValue {get; set;}
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

        public Task PutAsync(int id, NameUser updateUser)
        {
            throw new System.NotImplementedException();
        }

        public Task PutAsync(int id, NameUser updateUser, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}