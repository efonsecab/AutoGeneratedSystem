﻿using Microsoft.AspNetCore.Components;
namespace AutoGeneratedSystem.Client.Helpers
{
    public static class NavigationHelper
    {
        public static void NavigateToCreatePageForEntity(NavigationManager navigationManager, string entityName)
        {
            navigationManager.NavigateTo($"{entityName}/Create");
        }
    }
}