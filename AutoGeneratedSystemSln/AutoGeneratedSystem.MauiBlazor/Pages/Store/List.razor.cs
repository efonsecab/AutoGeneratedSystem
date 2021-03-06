using AutoGeneratedSystem.ClientServices;
using AutoGeneratedSystem.Common;
using AutoGeneratedSystem.Models.Store;
using Microsoft.AspNetCore.Components;
using Blazored.Toast.Services;
namespace AutoGeneratedSystem.MauiBlazor.Pages.Store
{
[Route(Constants.PageRoutes.StoreRoutes.List)]
public partial class List
{
[Inject]
StoreClientService StoreClientService { get; set; }
[Inject]
private IToastService ToastService { get;set; }
[Inject] private NavigationManager NavigationManager { get; set; }
private StoreModel[] AllStore { get; set; }
private bool IsLoading {get; set;}
protected override async Task OnInitializedAsync()
{
try
{
this.IsLoading = true;
this.AllStore = await StoreClientService.GetAllStoreAsync();
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
private async Task DeleteStoreAsync(StoreModel model)
{
try
{
this.IsLoading = true;
await this.StoreClientService.DeleteStoreAsync(model);
this.AllStore = await StoreClientService.GetAllStoreAsync();
ToastService.ShowSuccess($"Store with id {model.StoreId} has been deleted");
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
