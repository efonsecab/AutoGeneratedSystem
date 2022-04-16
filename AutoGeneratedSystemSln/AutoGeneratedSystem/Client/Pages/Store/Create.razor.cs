using AutoGeneratedSystem.Models.Store;
using Microsoft.AspNetCore.Components;
using Blazored.Toast.Services;
using AutoGeneratedSystem.ClientServices;
using AutoGeneratedSystem.Common;
namespace AutoGeneratedSystem.Client.Pages.Store
{
[Route(Constants.PageRoutes.StoreRoutes.Create)]
public partial class Create
{
[Inject]
StoreClientService StoreClientService { get; set; }
[Inject]
private IToastService ToastService { get;set; }
[Inject] private NavigationManager NavigationManager { get; set; }
private CreateStoreModel Model {get;set;} = new();
private bool IsLoading {get;set;} = false;
protected override async Task OnInitializedAsync()
{
try
{
IsLoading = true;
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
var result = await this.StoreClientService.CreateStoreAsync(this.Model);
ToastService.ShowSuccess("New Store has been created");
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
