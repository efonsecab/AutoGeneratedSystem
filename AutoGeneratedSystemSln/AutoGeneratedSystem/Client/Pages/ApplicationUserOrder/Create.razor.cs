using AutoGeneratedSystem.Models.ApplicationUserOrder;
using Microsoft.AspNetCore.Components;
using Blazored.Toast.Services;
using AutoGeneratedSystem.ClientServices;
using AutoGeneratedSystem.Common;
namespace AutoGeneratedSystem.Client.Pages.ApplicationUserOrder
{
[Route(Constants.PageRoutes.ApplicationUserOrderRoutes.Create)]
public partial class Create
{
[Inject]
ApplicationUserOrderClientService ApplicationUserOrderClientService { get; set; }
[Inject]
private IToastService ToastService { get;set; }
[Inject] private NavigationManager NavigationManager { get; set; }
private CreateApplicationUserOrderModel Model {get;set;} = new();
private bool IsLoading {get;set;} = false;
private long ApplicationUserId { get; set; }
private AutoGeneratedSystem.Models.ApplicationUser.ApplicationUserModel[] ApplicationUserModelItems{ get; set; }
[Inject] private ApplicationUserClientService ApplicationUserClientService { get; set; }
protected override async Task OnInitializedAsync()
{
try
{
IsLoading = true;
this.ApplicationUserModelItems = await ApplicationUserClientService.GetAllApplicationUserAsync();
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
private async Task OnValidSubmitAsync()
{
try
{
IsLoading = true;
var result = await this.ApplicationUserOrderClientService.CreateApplicationUserOrderAsync(this.Model);
this.NavigationManager.NavigateTo("/ApplicationUserOrder/List");
ToastService.ShowSuccess("New ApplicationUserOrder has been created");
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
