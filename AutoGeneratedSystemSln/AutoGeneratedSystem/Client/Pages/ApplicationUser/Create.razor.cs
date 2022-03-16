using AutoGeneratedSystem.Models.ApplicationUser;
using Microsoft.AspNetCore.Components;
using AutoGeneratedSystem.ClientServices;
namespace AutoGeneratedSystem.Client.Pages.ApplicationUser
{
public partial class Create
{
[Inject]
ApplicationUserClientService ApplicationUserClientService { get; set; }
private CreateApplicationUserModel Model {get;set;} = new();
private async Task OnValidSubmitAsync()
{
var result = await this.ApplicationUserClientService.CreateApplicationUserAsync(this.Model);
}
}
}
