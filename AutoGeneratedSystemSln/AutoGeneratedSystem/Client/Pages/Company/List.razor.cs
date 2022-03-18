using AutoGeneratedSystem.ClientServices;
using AutoGeneratedSystem.Common;
using AutoGeneratedSystem.Models.Company;
using Microsoft.AspNetCore.Components;
using Blazored.Toast.Services;
namespace AutoGeneratedSystem.Client.Pages.Company
{
[Route(Constants.PageRoutes.CompanyRoutes.List)]
public partial class List
{
[Inject]
CompanyClientService CompanyClientService { get; set; }
[Inject]
private IToastService ToastService { get;set; }
private CompanyModel[] AllCompany { get; set; }
private bool IsLoading {get; set;}
protected override async Task OnInitializedAsync()
{
try
{
this.IsLoading = true;
this.AllCompany = await CompanyClientService.GetAllCompanyAsync();
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
}
}
