﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Immutable;
using System.Text;

namespace AutoGeneratedSystem.Client.Generators
{
    [Generator]
    public class BlazorPagesIncrementalGenerator : IIncrementalGenerator
    {
        private const string BlazorWasmPageOfEntityAttribute = "BlazorWasmPageOfEntity";

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
#if DEBUG
            //System.Diagnostics.Debugger.Launch();
#endif
            // Combine the selected interfaces with the `Compilation`
            IncrementalValueProvider<(Compilation, ImmutableArray<AdditionalText>)>
                compilationAndAdditionalFiles
                = context.CompilationProvider.Combine(context.AdditionalTextsProvider.Collect());

            // Generate the source using the compilation and classes
            context.RegisterSourceOutput(compilationAndAdditionalFiles,
                static (spc, source) => Execute(source.Item1, source.Item2, spc));
        }

        static void Execute(Compilation compilation,
            ImmutableArray<AdditionalText> additionalTexts, SourceProductionContext context)
        {
            foreach (var singleAdditionalText in additionalTexts)
            {
                if (singleAdditionalText.Path.EndsWith("AutoGeneratePages.txt"))
                {
                    var allFileLines = File.ReadAllLines(singleAdditionalText.Path);
                    foreach (var singleFileLine in allFileLines)
                    {
                        var entityName = Directory.GetParent(singleAdditionalText.Path).Name;
                        switch (singleFileLine)
                        {
                            case "Create":
                                GenerateCreatePage(compilation, singleAdditionalText, singleFileLine, entityName);
                                break;
                            case "List":
                                GenerateListPage(compilation, singleAdditionalText, singleFileLine, entityName);
                                break;
                        }
                    }
                }
            }
        }

        private static void GenerateCreatePage(Compilation compilation, AdditionalText singleAdditionalText, string singleFileLine, string entityName)
        {
            StringBuilder razorCreatePage = new StringBuilder();
            string pageAction = "Create";
            razorCreatePage.AppendLine($"<h3>{entityName}</h3>");
            string modelTypeFullName = $"AutoGeneratedSystem.Models.{entityName}.{pageAction}{entityName}Model";
            var modelType = compilation.GetTypeByMetadataName(modelTypeFullName);
            var modelProperties = modelType!.GetMembers().Where(p => p.Kind == SymbolKind.Property);
            razorCreatePage.AppendLine("<LoadingIndicator IsLoading=\"IsLoading\"></LoadingIndicator>");
            razorCreatePage.AppendLine($"<EditForm Model=\"@this.Model\" OnValidSubmit=\"OnValidSubmitAsync\">");
            razorCreatePage.AppendLine("<div class=\"mb-3\">");
            razorCreatePage.AppendLine("<DataAnnotationsValidator></DataAnnotationsValidator>");
            razorCreatePage.AppendLine("<ValidationSummary></ValidationSummary>");
            razorCreatePage.AppendLine("</div>");
            foreach (IPropertySymbol singleProperty in modelProperties)
            {
                var propertyType = singleProperty.Type;
                switch (propertyType.ToString())
                {
                    case "bool?":
                    case "bool":
                        razorCreatePage.AppendLine("<div class=\"mb-3\">");
                        razorCreatePage.AppendLine($"<label class=\"form-check-label\">@nameof(Model.{singleProperty.Name})</label>");
                        razorCreatePage.AppendLine($"<InputCheckbox class=\"form-check-input\" @bind-Value=\"Model.{singleProperty.Name}\"/>");
                        razorCreatePage.AppendLine("</div>");
                        break;
                    case "int?":
                    case "int":
                    case "long?":
                    case "long":
                    case "decimal?":
                    case "decimal":
                    case "double?":
                    case "double":
                        razorCreatePage.AppendLine("<div class=\"mb-3\">");
                        razorCreatePage.AppendLine($"<label class=\"form-label\">@nameof(Model.{singleProperty.Name})</label>");
                        razorCreatePage.AppendLine($"<InputNumber class=\"form-control\" @bind-Value=\"Model.{singleProperty.Name}\"/>");
                        razorCreatePage.AppendLine("</div>");
                        break;
                    case "System.String?":
                    case "System.String":
                    case "string?":
                    case "string":
                        razorCreatePage.AppendLine("<div class=\"mb-3\">");
                        razorCreatePage.AppendLine($"<label class=\"form-label\">@nameof(Model.{singleProperty.Name})</label>");
                        razorCreatePage.AppendLine($"<InputText class=\"form-control\" @bind-Value=\"Model.{singleProperty.Name}\"/>");
                        razorCreatePage.AppendLine("</div>");
                        break;
                    case "System.DateTimeOffset?":
                    case "System.DateTimeOffset":
                    case "System.DateTime?":
                    case "System.DateTime":
                        razorCreatePage.AppendLine("<div class=\"mb-3\">");
                        razorCreatePage.AppendLine($"<label class=\"form-label\">@nameof(Model.{singleProperty.Name})</label>");
                        razorCreatePage.AppendLine($"<InputDateTime class=\"form-control\" @bind-Value=\"@Model.{singleProperty.Name}\" />");
                        razorCreatePage.AppendLine("</div>");
                        break;
                        //Seems Blzor does not yet support component serialization of DateOnly data types
                        //case "System.DateOnly?":
                        //case "System.DateOnly":
                        //    razorCreatePage.AppendLine("<div class=\"mb-3\">");
                        //    razorCreatePage.AppendLine($"<label class=\"form-label\">@nameof(Model.{singleProperty.Name})</label>");
                        //    razorCreatePage.AppendLine($"<InputDate class=\"form-control\" @bind-Value=\"@Model.{singleProperty.Name}\"/>");
                        //    razorCreatePage.AppendLine("</div>");
                        //    break;
                }
            }
            razorCreatePage.AppendLine("<div class=\"mb-3\">");
            razorCreatePage.AppendLine("<button class=\"btn btn-primary\" type=\"submit\">Submit</button>");
            razorCreatePage.AppendLine("</div>");
            razorCreatePage.AppendLine("</EditForm>");

            var parentFolderPath = Path.GetDirectoryName(singleAdditionalText.Path);
            var newFilePath = Path.Combine(parentFolderPath, $"{pageAction}.razor");
            File.WriteAllText(newFilePath, razorCreatePage.ToString());

            StringBuilder pageCodeBehind = new StringBuilder();
            pageCodeBehind.AppendLine($"using AutoGeneratedSystem.Models.{entityName};");
            pageCodeBehind.AppendLine("using Microsoft.AspNetCore.Components;");
            pageCodeBehind.AppendLine("using Blazored.Toast.Services;");
            pageCodeBehind.AppendLine($"using AutoGeneratedSystem.ClientServices;");
            pageCodeBehind.AppendLine("using AutoGeneratedSystem.Common;");
            pageCodeBehind.AppendLine($"namespace AutoGeneratedSystem.Client.Pages.{entityName}");
            pageCodeBehind.AppendLine("{");
            pageCodeBehind.AppendLine($"[Route(Constants.PageRoutes.{entityName}Routes.{pageAction})]");
            pageCodeBehind.AppendLine($"public partial class {singleFileLine}");
            pageCodeBehind.AppendLine("{");
            pageCodeBehind.AppendLine("[Inject]");
            pageCodeBehind.AppendLine($"{entityName}ClientService {entityName}ClientService {{ get; set; }}");
            pageCodeBehind.AppendLine("[Inject]");
            pageCodeBehind.AppendLine("private IToastService ToastService { get;set; }");
            pageCodeBehind.AppendLine($"private {pageAction}{entityName}Model Model {{get;set;}} = new();");
            pageCodeBehind.AppendLine("private bool IsLoading {get;set;} = false;");
            pageCodeBehind.AppendLine($"private async Task OnValidSubmitAsync()");
            pageCodeBehind.AppendLine("{");
            pageCodeBehind.AppendLine("try");
            pageCodeBehind.AppendLine("{");
            pageCodeBehind.AppendLine("IsLoading = true;");
            pageCodeBehind.AppendLine($"var result = await this.{entityName}ClientService.{pageAction}{entityName}Async(this.Model);");
            pageCodeBehind.AppendLine($"ToastService.ShowSuccess(\"New {entityName} has been created\");");
            pageCodeBehind.AppendLine("}");
            pageCodeBehind.AppendLine("catch (Exception ex)");
            pageCodeBehind.AppendLine("{");
            pageCodeBehind.AppendLine("ToastService.ShowError(ex.Message);");
            pageCodeBehind.AppendLine("}");
            pageCodeBehind.AppendLine("finally");
            pageCodeBehind.AppendLine("{");
            pageCodeBehind.AppendLine("IsLoading = false;");
            pageCodeBehind.AppendLine("}");
            pageCodeBehind.AppendLine("}");
            pageCodeBehind.AppendLine("}");
            pageCodeBehind.AppendLine("}");
            var codeBehindFilePath = Path.Combine(parentFolderPath, $"{pageAction}.razor.cs");
            File.WriteAllText(codeBehindFilePath, pageCodeBehind.ToString());
            //context.AddSource($"Pages_{entityName}_Create_razor.g.cs",
            //    SourceText.From(pageCodeBehind.ToString(),Encoding.UTF8));
        }

        private static void GenerateListPage(Compilation compilation, AdditionalText singleAdditionalText, string singleFileLine, string entityName)
        {
            string pageAction = "List";
            StringBuilder razorPage = new StringBuilder();
            razorPage.AppendLine($"<h3>{entityName}</h3>");
            string modelTypeFullName = $"AutoGeneratedSystem.Models.{entityName}.{entityName}Model";
            var modelType = compilation.GetTypeByMetadataName(modelTypeFullName);
            var modelProperties = modelType!.GetMembers().Where(p => p.Kind == SymbolKind.Property);
            razorPage.AppendLine("<LoadingIndicator IsLoading=\"IsLoading\"></LoadingIndicator>");
            razorPage.AppendLine($"@if (this.All{entityName} != null)");
            razorPage.AppendLine("{");
            razorPage.AppendLine($"foreach (var single{entityName} in this.All{entityName})");
            razorPage.AppendLine("{");
            razorPage.AppendLine("<ul class=\"list-group\">");
            razorPage.AppendLine("<li class=\"list-group-item\">");
            foreach (IPropertySymbol singleProperty in modelProperties)
            {
                razorPage.AppendLine($"@nameof(single{entityName}.{singleProperty.Name}):@single{entityName}.{singleProperty.Name} <br>");
            }
            razorPage.AppendLine("</li>");
            razorPage.AppendLine("</ul>");
            razorPage.AppendLine("}");
            razorPage.AppendLine("}");

            var parentFolderPath = Path.GetDirectoryName(singleAdditionalText.Path);
            var newFilePath = Path.Combine(parentFolderPath, $"{pageAction}.razor");
            File.WriteAllText(newFilePath, razorPage.ToString());

            StringBuilder pageCodeBehind = new StringBuilder();
            pageCodeBehind.AppendLine("using AutoGeneratedSystem.ClientServices;");
            pageCodeBehind.AppendLine("using AutoGeneratedSystem.Common;");
            pageCodeBehind.AppendLine($"using AutoGeneratedSystem.Models.{entityName};");
            pageCodeBehind.AppendLine($"using Microsoft.AspNetCore.Components;");
            pageCodeBehind.AppendLine("using Blazored.Toast.Services;");
            pageCodeBehind.AppendLine($"namespace AutoGeneratedSystem.Client.Pages.{entityName}");
            pageCodeBehind.AppendLine("{");
            pageCodeBehind.AppendLine($"[Route(Constants.PageRoutes.{entityName}Routes.{pageAction})]");
            pageCodeBehind.AppendLine($"public partial class {singleFileLine}");
            pageCodeBehind.AppendLine("{");
            pageCodeBehind.AppendLine("[Inject]");
            pageCodeBehind.AppendLine($"{entityName}ClientService {entityName}ClientService {{ get; set; }}");
            pageCodeBehind.AppendLine("[Inject]");
            pageCodeBehind.AppendLine("private IToastService ToastService { get;set; }");
            pageCodeBehind.AppendLine($"private {entityName}Model[] All{entityName} {{ get; set; }}");
            pageCodeBehind.AppendLine("private bool IsLoading {get; set;}");
            pageCodeBehind.AppendLine("protected override async Task OnInitializedAsync()");
            pageCodeBehind.AppendLine("{");
            pageCodeBehind.AppendLine("try");
            pageCodeBehind.AppendLine("{");
            pageCodeBehind.AppendLine("this.IsLoading = true;");
            pageCodeBehind.AppendLine($"this.All{entityName} = await {entityName}ClientService.GetAll{entityName}Async();");
            pageCodeBehind.AppendLine("}");
            pageCodeBehind.AppendLine("catch (Exception ex)");
            pageCodeBehind.AppendLine("{");
            pageCodeBehind.AppendLine("ToastService.ShowError(ex.Message);");
            pageCodeBehind.AppendLine("}");
            pageCodeBehind.AppendLine("finally");
            pageCodeBehind.AppendLine("{");
            pageCodeBehind.AppendLine("this.IsLoading=false;");
            pageCodeBehind.AppendLine("}");
            pageCodeBehind.AppendLine("}");
            pageCodeBehind.AppendLine("}");
            pageCodeBehind.AppendLine("}");
            var codeBehindFilePath = Path.Combine(parentFolderPath, $"{pageAction}.razor.cs");
            File.WriteAllText(codeBehindFilePath, pageCodeBehind.ToString());
            //context.AddSource($"Pages_{entityName}_{pageAction}_razor.g.cs",
            //    SourceText.From(pageCodeBehind.ToString(),Encoding.UTF8));
        }
    }
}
