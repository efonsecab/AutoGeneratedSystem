using AutoGeneratedSystem.Models.Company;
using Microsoft.AspNetCore.Components;
using Blazored.Toast.Services;
using AutoGeneratedSystem.ClientServices;
using AutoGeneratedSystem.Common;
namespace AutoGeneratedSystem.MauiBlazor.Pages.Company
{
[Route(Constants.PageRoutes.CompanyRoutes.Create)]
public partial class Create
{
[Inject]
CompanyClientService CompanyClientService { get; set; }
[Inject]
private IToastService ToastService { get;set; }
[Inject] private NavigationManager NavigationManager { get; set; }
private CreateCompanyModel Model {get;set;} = new();
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
var result = await this.CompanyClientService.CreateCompanyAsync(this.Model);
this.NavigationManager.NavigateTo("/Company/List");
ToastService.ShowSuccess("New Company has been created");
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
