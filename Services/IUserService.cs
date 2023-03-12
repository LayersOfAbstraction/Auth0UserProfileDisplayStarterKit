using System.Threading;
using System.Threading.Tasks;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi;

//The namespace is different to the rest of the project
//and stops the ManagementApi from been recognized.
//It has been deleted.
    public interface IUserService
    {
        //Prepending Auth0.ManagementApi.Models to User prevents confusion
        //for .NET 6 to know if we want to list Auth0 users or our own view model users.
        Task<IPagedList<Auth0.ManagementApi.Models.User>> GetUsersAsync(GetUsersRequest request, PaginationInfo paginationInfo,
                CancellationToken cancellationToken);
    }
