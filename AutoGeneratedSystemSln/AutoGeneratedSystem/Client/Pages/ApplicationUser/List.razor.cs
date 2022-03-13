﻿using AutoGeneratedSystem.ClientServices;
using AutoGeneratedSystem.Models.ApplicationUser;
using Microsoft.AspNetCore.Components;

namespace AutoGeneratedSystem.Client.Pages.ApplicationUser
{
    public partial class List
    {
        [Inject]
        ApplicationUserClientService ApplicationUserClientService { get; set; }
        private ApplicationUserModel[] AllApplicationUsers { get; set; }
        protected override async Task OnInitializedAsync()
        {
            this.AllApplicationUsers = await ApplicationUserClientService.GetAllApplicationUserAsync();
        }
    }
}
