using System.Collections.Generic;
using System.IO;
using System.Linq;
using Emm.SqlStringsGenerator.Tests.Utils;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Xunit;

namespace Emm.SqlStringsGenerator.Tests;

public class SqlStringsSourceGeneratorTests
{
    
    [Fact]
    public void GenerateClassesBasedOnSqlFile()
    {
        // Create an instance of the source generator.
        var generator = new SqlStringsSourceGenerator();

        // Source generators should be tested using 'GeneratorDriver'.
        var driver = CSharpGeneratorDriver.Create(new[] { generator },
            new[]
            {
                // Add the additional file separately from the compilation.
                new TestAdditionalFile("./Queries.sql", "")
            });

        // To run generators, we can use an empty compilation.
        var compilation = CSharpCompilation.Create(nameof(SqlStringsSourceGeneratorTests));

        // Run generators. Don't forget to use the new compilation rather than the previous one.
        driver.RunGeneratorsAndUpdateCompilation(compilation, out var newCompilation, out _);

        // Retrieve all files in the compilation.
        var generatedFiles = newCompilation.SyntaxTrees
            .Select(t => Path.GetFileName(t.FilePath))
            .ToArray();

        var root = newCompilation.SyntaxTrees.First().GetRoot();

        List<string> namespaces = new();
        List<string> classNames = new();
        List<string> identifiers = new();
        
        foreach (var node in root.DescendantNodes())
        {
            switch (node)
            {
                case NamespaceDeclarationSyntax namespaceDeclaration:
                    namespaces.Add(namespaceDeclaration.Name.ToString());
                    break;

                case ClassDeclarationSyntax classDeclaration:
                    classNames.Add(classDeclaration.Identifier.ValueText);
                    break;
                
                case FieldDeclarationSyntax fieldDeclaration:
                    identifiers.Add(fieldDeclaration.Declaration.Variables[0].Identifier.ValueText);
                    break;
            }
        }

        // Check Generated Files
        Assert.Equivalent(new[]
        {
            $"SqlStringsMyQueries.g.cs" 
        }, generatedFiles);
        
        // Check Generated ClassNames
        Assert.Equivalent(new[]
        {
            $"MyQueries" 
        }, classNames);
        
        // Check Generated ClassNames
        Assert.Equivalent(new[]
        {
            $"DeleteQuery",
            $"GetCount"
        }, identifiers);

    }
}