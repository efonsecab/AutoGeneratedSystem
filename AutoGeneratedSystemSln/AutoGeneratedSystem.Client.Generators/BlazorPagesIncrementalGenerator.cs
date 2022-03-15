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
                                StringBuilder razorCreatePage = new StringBuilder();
                                razorCreatePage.AppendLine($"@page \"/{entityName}/Create\"");
                                razorCreatePage.AppendLine($"<h3>{entityName}</h3>");
                                string modelTypeFullName = $"AutoGeneratedSystem.Models.{entityName}.Create{entityName}Model";
                                var modelType = compilation.GetTypeByMetadataName(modelTypeFullName);
                                var modelProperties = modelType!.GetMembers().Where(p => p.Kind == SymbolKind.Property);
                                razorCreatePage.AppendLine($"<EditForm Model=\"@this.Model\" OnValidSubmit=\"OnValidSubmitAsync\">");
                                razorCreatePage.AppendLine("<div class=\"mb-3\">");
                                razorCreatePage.AppendLine("<DataAnnotationsValidator></DataAnnotationsValidator>");
                                razorCreatePage.AppendLine("<ValidationSummary></ValidationSummary>");
                                razorCreatePage.AppendLine("</div>");
                                foreach (IPropertySymbol singleProperty in modelProperties)
                                {
                                    var propertyType = singleProperty.Type;
                                    switch (propertyType.Name)
                                    {
                                        case nameof(String):
                                            razorCreatePage.AppendLine("<div class=\"mb-3\">");
                                            razorCreatePage.AppendLine($"<label class=\"form-label\">@nameof(Model.{singleProperty.Name})</label>");
                                            razorCreatePage.AppendLine($"<InputText class=\"form-control\" @bind-Value=\"Model.{singleProperty.Name}\"/>");
                                            razorCreatePage.AppendLine("</div>");
                                            break;
                                    }
                                }
                                razorCreatePage.AppendLine("<div class=\"mb-3\">");
                                razorCreatePage.AppendLine("<button class=\"btn btn-primary\" type=\"submit\">Submit</button>");
                                razorCreatePage.AppendLine("</div>");
                                razorCreatePage.AppendLine("</EditForm>");

                                var parentFolderPath = Path.GetDirectoryName(singleAdditionalText.Path);
                                var newFilePath = Path.Combine(parentFolderPath, "Create.razor");
                                File.WriteAllText(newFilePath, razorCreatePage.ToString());

                                StringBuilder pageCodeBehind = new StringBuilder();
                                pageCodeBehind.AppendLine($"using AutoGeneratedSystem.Models.{entityName};");
                                pageCodeBehind.AppendLine("using Microsoft.AspNetCore.Components;");
                                pageCodeBehind.AppendLine($"using AutoGeneratedSystem.ClientServices;");
                                pageCodeBehind.AppendLine($"namespace AutoGeneratedSystem.Client.Pages.{entityName}");
                                pageCodeBehind.AppendLine("{");
                                pageCodeBehind.AppendLine($"public partial class {singleFileLine}");
                                pageCodeBehind.AppendLine("{");
                                pageCodeBehind.AppendLine("[Inject]");
                                pageCodeBehind.AppendLine($"{entityName}ClientService {entityName}ClientService {{ get; set; }}");
                                pageCodeBehind.AppendLine($"private Create{entityName}Model Model {{get;set;}} = new();");
                                pageCodeBehind.AppendLine($"private async Task OnValidSubmitAsync()");
                                pageCodeBehind.AppendLine("{");
                                pageCodeBehind.AppendLine($"var result = await this.{entityName}ClientService.Create{entityName}Async(this.Model);");
                                pageCodeBehind.AppendLine("}");
                                pageCodeBehind.AppendLine("}");
                                pageCodeBehind.AppendLine("}");
                                var codeBehindFilePath = Path.Combine(parentFolderPath, "Create.razor.cs");
                                File.WriteAllText(codeBehindFilePath, pageCodeBehind.ToString());
                                //context.AddSource($"Pages_{entityName}_Create_razor.g.cs",
                                //    SourceText.From(pageCodeBehind.ToString(),Encoding.UTF8));
                                break;
                        }
                    }
                }
            }
        }
    }
}
