using AutoGeneratedSystem.Models.ApplicationUser;
using Microsoft.AspNetCore.Components;
using Blazored.Toast.Services;
using AutoGeneratedSystem.ClientServices;
using AutoGeneratedSystem.Common;
namespace AutoGeneratedSystem.MauiBlazor.Pages.ApplicationUser
{
[Route(Constants.PageRoutes.ApplicationUserRoutes.Create)]
public partial class Create
{
[Inject]
ApplicationUserClientService ApplicationUserClientService { get; set; }
[Inject]
private IToastService ToastService { get;set; }
private CreateApplicationUserModel Model {get;set;} = new();
private bool IsLoading {get;set;} = false;
private async Task OnValidSubmitAsync()
{
try
{
IsLoading = true;
var result = await this.ApplicationUserClientService.CreateApplicationUserAsync(this.Model);
ToastService.ShowSuccess("New ApplicationUser has been created");
}
catch (Exception ex)
{
ToastService.ShowError(ex.Message);
}
finally
{
IsLoading = false;
}
}
}
}
