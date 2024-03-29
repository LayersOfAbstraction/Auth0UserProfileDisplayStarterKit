﻿using System;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using Example.Auth0.AuthenticationApi.AccessTokenManagement;
using Example.Auth0.AuthenticationApi.AccessTokenManagement.Interfaces;
using Microsoft.Extensions.Configuration;

//The namespace is different to the rest of the project
//and stops the ManagementApi from been recognized.
//It has been deleted.
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IClientAccessTokenManagementService _clientAccessTokenManagementService;

        public UserService(IConfiguration configuration,
            IClientAccessTokenManagementService clientAccessTokenManagementService)
        {
            _configuration = configuration;
            _clientAccessTokenManagementService = clientAccessTokenManagementService;
        }

        //Prepending Auth0.ManagementApi.Models to User prevents confusion
        //for .NET 6 to know if we want to list Auth0 users or our own view model users.
        public async Task<IPagedList<Auth0.ManagementApi.Models.User>> GetUsersAsync(GetUsersRequest request,
            PaginationInfo paginationInfo, CancellationToken cancellationToken)
        {
            return await MakeCallAsync(async apiClient => await apiClient.Users.GetAllAsync(request, paginationInfo),
                cancellationToken);
        }

        private async Task<TResponse> MakeCallAsync<TResponse>(Func<ManagementApiClient, Task<TResponse>> callFunc,
            CancellationToken cancellationToken)
        {
            var apiClient = await GetApiClientAsync(forceRenewal: false, cancellationToken);
            try
            {
                return await callFunc(apiClient);
            }
            catch (AuthenticationException) // retry if 401
            {
                apiClient = await GetApiClientAsync(forceRenewal: true, cancellationToken);
                return await callFunc(apiClient);
            }
        }

        private async Task<ManagementApiClient> GetApiClientAsync(bool forceRenewal, CancellationToken cancellationToken)
        {
            var parameters = new ClientAccessTokenParameters
            {
                ForceRenewal = forceRenewal
            };

            var token = await _clientAccessTokenManagementService.GetClientAccessTokenAsync(nameof(UserService), parameters, cancellationToken);

            return new ManagementApiClient(token, new Uri(_configuration["Auth0:ManagementApi:BaseUri"]));
        }
    }
