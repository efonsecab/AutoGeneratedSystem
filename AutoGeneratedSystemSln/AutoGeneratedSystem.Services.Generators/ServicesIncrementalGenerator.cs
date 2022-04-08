﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Immutable;
using System.Text;

namespace AutoGeneratedSystem.Services.Generators
{
    [Generator]
    public class ServicesIncrementalGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
#if DEBUG
            //System.Diagnostics.Debugger.Launch();
#endif
            // Do a simple filter for enums
            IncrementalValuesProvider<ClassDeclarationSyntax> classDeclarations =
                context.SyntaxProvider
                .CreateSyntaxProvider(
                    predicate: static (s, _) => IsSyntaxTargetForGeneration(s), // select enums with attributes
                    transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx)) // sect the enum with the [EnumExtensions] attribute
                .Where(static m => m is not null)!; // filter out attributed enums that we don't care about

            // Combine the selected interfaces with the `Compilation`
            IncrementalValueProvider<(Compilation, ImmutableArray<ClassDeclarationSyntax>)>
                compilationAndClasses
                = context.CompilationProvider.Combine(classDeclarations.Collect());

            // Generate the source using the compilation and classes
            context.RegisterSourceOutput(compilationAndClasses,
                static (spc, source) => Execute(source.Item1, source.Item2, spc));
        }

        private static ClassDeclarationSyntax GetSemanticTargetForGeneration(GeneratorSyntaxContext generatorSyntaxContext)
        {
            var classDeclarationSyntax = generatorSyntaxContext.Node as ClassDeclarationSyntax;
            return classDeclarationSyntax!;
        }

        private static bool IsSyntaxTargetForGeneration(SyntaxNode syntaxNode)
        {
            if (syntaxNode is ClassDeclarationSyntax)
            {
                ClassDeclarationSyntax classDeclarationSyntax = (ClassDeclarationSyntax)syntaxNode;
                foreach (var singleAttributeList in classDeclarationSyntax.AttributeLists)
                {
                    foreach (var singleAttribute in singleAttributeList.Attributes)
                    {
                        var identifierNameSyntax = (singleAttribute.Name) as IdentifierNameSyntax;
                        string identifierText = identifierNameSyntax!.Identifier.Text;
                        if (identifierText == "ServiceOfEntity")
                            return true;
                    }
                }
            }
            return false;
        }

        static void Execute(Compilation compilation,
            ImmutableArray<ClassDeclarationSyntax> classesDeclarationSyntax, SourceProductionContext context)
        {
            string assemblyName = compilation.AssemblyName;
            string[] splittedAssemblyName = assemblyName.Split('.');
            string assemblyNameFirstPart = splittedAssemblyName[0];
            foreach (var singleClassDeclarationSyntax in classesDeclarationSyntax)
            {
                var serviceName = singleClassDeclarationSyntax.Identifier.Text;
                foreach (var singleAttributeList in singleClassDeclarationSyntax.AttributeLists)
                {
                    foreach (var singleAttribute in singleAttributeList.Attributes)
                    {
                        var identifierNameSyntax = (singleAttribute.Name) as IdentifierNameSyntax;
                        string identifierText = identifierNameSyntax!.Identifier.Text;
                        if (identifierText == "ServiceOfEntity")
                        {
                            var argument = singleAttribute.ArgumentList!.Arguments.Single();
                            var memberAccessExpressionSyntax = argument.Expression as MemberAccessExpressionSyntax;
                            var simpleNameSyntax = memberAccessExpressionSyntax!.Name;
                            var entityName = simpleNameSyntax.Identifier.Text;
                            StringBuilder stringBuilder = new StringBuilder();
                            stringBuilder.AppendLine("using System.Threading.Tasks;");
                            stringBuilder.AppendLine($"using {assemblyNameFirstPart}.DataAccess.Data;");
                            stringBuilder.AppendLine($"using {assemblyNameFirstPart}.DataAccess.Models;");
                            stringBuilder.AppendLine("using System.Linq;");
                            stringBuilder.AppendLine($"namespace {assemblyNameFirstPart}.Services");
                            stringBuilder.AppendLine("{");
                            stringBuilder.AppendLine($"public partial class {serviceName}");
                            stringBuilder.AppendLine("{");
                            stringBuilder.AppendLine("private readonly AutogeneratedsystemDatabaseContext _autogeneratedsystemDatabaseContext;");
                            stringBuilder.AppendLine($"public {serviceName}(AutogeneratedsystemDatabaseContext autogeneratedsystemDatabaseContext)");
                            stringBuilder.AppendLine("{");
                            stringBuilder.AppendLine("_autogeneratedsystemDatabaseContext = autogeneratedsystemDatabaseContext;");
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine($"public async Task<{entityName}> Create{entityName}Async({entityName} entity, CancellationToken cancellationToken)");
                            stringBuilder.AppendLine("{");
                            stringBuilder.AppendLine($"await _autogeneratedsystemDatabaseContext.{entityName}.AddAsync(entity,cancellationToken);");
                            stringBuilder.AppendLine($"await _autogeneratedsystemDatabaseContext.SaveChangesAsync(cancellationToken);");
                            stringBuilder.AppendLine($"return entity;");
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine($"public IQueryable<{entityName}> GetAll{entityName}(CancellationToken cancellationToken)");
                            stringBuilder.AppendLine("{");
                            stringBuilder.AppendLine($"return _autogeneratedsystemDatabaseContext.{entityName};");
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine($"public async Task Delete{entityName}Async({entityName} entity, CancellationToken cancellationToken)");
                            stringBuilder.AppendLine("{");
                            stringBuilder.AppendLine($"this._autogeneratedsystemDatabaseContext.{entityName}.Remove(entity);");
                            stringBuilder.AppendLine($"await this._autogeneratedsystemDatabaseContext.SaveChangesAsync(cancellationToken);");
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine($"public async Task<{entityName}> Update{entityName}Async({entityName} entity, CancellationToken cancellationToken)");
                            stringBuilder.AppendLine("{");
                            stringBuilder.AppendLine($"this._autogeneratedsystemDatabaseContext.{entityName}.Update(entity);");
                            stringBuilder.AppendLine("await this._autogeneratedsystemDatabaseContext.SaveChangesAsync(cancellationToken);");
                            stringBuilder.AppendLine("return entity;");
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine($"public async Task<{entityName}> Get{entityName}ByIdAsync(long primarykeyValue, CancellationToken cancellationToken)");
                            stringBuilder.AppendLine("{");
                            stringBuilder.AppendLine($"var entity = await this._autogeneratedsystemDatabaseContext.{entityName}.FindAsync(new object?[] {{ primarykeyValue }}, cancellationToken:cancellationToken);");
                            stringBuilder.AppendLine("return entity;");
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("}");
                            context.AddSource($"{serviceName}.g.cs",
                        SourceText.From(stringBuilder.ToString(), Encoding.UTF8));
                        }
                    }
                }
            }
        }
    }
}
