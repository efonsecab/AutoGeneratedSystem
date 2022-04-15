using AutoGeneratedSystem.ClientServices;
using AutoGeneratedSystem.Common;
using AutoGeneratedSystem.Models.ApplicationUser;
using Microsoft.AspNetCore.Components;
using Blazored.Toast.Services;
namespace AutoGeneratedSystem.Client.Pages.ApplicationUser
{
[Route(Constants.PageRoutes.ApplicationUserRoutes.List)]
public partial class List
{
[Inject]
ApplicationUserClientService ApplicationUserClientService { get; set; }
[Inject]
private IToastService ToastService { get;set; }
[Inject] private NavigationManager NavigationManager { get; set; }
private ApplicationUserModel[] AllApplicationUser { get; set; }
private bool IsLoading {get; set;}
protected override async Task OnInitializedAsync()
{
try
{
this.IsLoading = true;
this.AllApplicationUser = await ApplicationUserClientService.GetAllApplicationUserAsync();
}
catch (Exception ex)
{
ToastService.ShowError(ex.Message);
}
finally
{
this.IsLoading=false;
}
}
private async Task DeleteApplicationUserAsync(ApplicationUserModel model)
{
try
{
this.IsLoading = true;
await this.ApplicationUserClientService.DeleteApplicationUserAsync(model);
this.AllApplicationUser = await ApplicationUserClientService.GetAllApplicationUserAsync();
ToastService.ShowSuccess($"ApplicationUser with id {model.ApplicationUserId} has been deleted");
}
catch (Exception ex)
{
ToastService.ShowError(ex.Message);
}
finally
{
this.IsLoading = false;
}
}
}
}
