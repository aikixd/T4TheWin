﻿using _CodeGenerator.App.Templates;
using _CodeGenerator.Bridge;
using _CodeGenerator.Definitions;
using _CodeGenerator.Definitions.Syntax;
using _CodeGenerator.Domain;
using _CodeGenerator.Roslyn;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CodeGenerator.App
{
    static class ProjectOperations
    {
        public static Project RemoveFiles(this Project project, IEnumerable<string> files)
        {
            var documents = project.Documents.Where(x => files.Contains(x.FilePath));

            foreach (var doc in documents)
            {
                project = project.RemoveDocument(doc.Id);
            }

            return project;
        }

        public static (Project project, IEnumerable<string> addedFiles) 
            ExtendDecoratedClasses(this Project project, string generatedFilesDir)
        {
            var addedFiles = new LinkedList<string>();

            foreach (var doc in project.Documents)
            {
                var semanticsModel = doc.GetSemanticModelAsync().Result;
                forAttr<GenerateEqualityAttribute>(semanticsModel, (x) => new EquatableTemplate(x).TransformText(), "Equatable");
                forAttr<GenerateDisposableAttribute>(semanticsModel, (x) => new DisposableTemplate(x).TransformText(), "Disposable");

            }

            return (project, addedFiles);

            void forAttr<TAttr>(SemanticModel semanticModel, Func<IClassInfoProvider, string> genFn, string genName)
            {
                var classInfos = Attributes.FindDecoratedWith<TAttr>(semanticModel);

                foreach (var clsNfo in classInfos)
                {
                    var output = genFn(clsNfo);
                    var filePath = Path.Combine(generatedFilesDir, clsNfo.Namespace, $"{clsNfo.Name}.{genName}.cs");

                    // Skip document addition in new sdk.
                    project =
                        project
                        .AddDocument(
                            filePath,
                            output)
                        .Project;

                    addedFiles.AddLast(filePath);
                }
            }
        }

        public static (Project project, IEnumerable<string> addedFiles)
            GenerateFromDefinition(
                this Project project, 
                string generatedFilesDir, 
                IEnumerable<DefinitionGenerationInfo> nfos)
        {
            var addedFiles = new LinkedList<string>();

            foreach (var nfo in nfos)
                generate(nfo.Output, nfo.Name, nfo.Namespace, nfo.Affix);

            return (project, addedFiles);

            void generate(string output, string name, string @namespace, string genName)
            {
                var fileName = $"{name}.{genName}.cs";
                var filePath = Path.Combine(generatedFilesDir, @namespace, fileName);

                var doc = project.Documents.SingleOrDefault(x => x.FilePath == filePath);

                if (doc != null)
                {
                    project = doc.WithText(SourceText.From(output)).Project;
                }
                else
                {
                    project =
                        project
                        
                        .AddDocument(
                            filePath,
                            output)
                        .Project;
                }
                
                addedFiles.AddLast(filePath);
            }
        }

        public static Project DetachAddedFiles(this (Project project, IEnumerable<string> addedFiles) t, LinkedList<string> targetList)
        {
            foreach (var f in t.addedFiles)
            {
                targetList.AddLast(f);
            }

            return t.project;
        }

        public static DefinitionGenerationInfo[] GetGenerationInfoForVsSyntax()
        {
            return new[]
            {
                new DefinitionGenerationInfo(
                    Definition.Whitespace.classInfo.Name,
                    Definition.Whitespace.classInfo.Namespace,
                    "ext",
                    new ScannerTemplate(
                        Definition.Whitespace.classInfo,
                        Definition.Whitespace.whitespace)
                        .TransformText()),

                new DefinitionGenerationInfo(
                    Definition.Syntax.classInfo.Name,
                    Definition.Syntax.classInfo.Namespace,
                    "ext",
                    new ParserTemplate(
                        Definition.Syntax.classInfo,
                        Definition.Syntax.syntaxParts
                            .Where(x =>
                                false ==  
                                (x is SyntaxList sl && (sl.Flags & SyntaxListFlags.SkipParserGeneration) == SyntaxListFlags.SkipParserGeneration))
                                .ToArray())
                        .TransformText())
            }
            .Union(
                Definition
                .SyntaxParts
                .Select(x =>
                    new DefinitionGenerationInfo(
                        x.classInfo.Name,
                        x.classInfo.Namespace,
                        "syntax",
                        new SyntaxNodeTemplate(x.classInfo, x.syntaxPart).TransformText())))
            .Union(
                Definition
                .SyntaxParts
                .Where(x => x.syntaxPart is Definitions.Syntax.Stream)
                .Select(x =>
                    new DefinitionGenerationInfo(
                        x.classInfo.Name,
                        x.classInfo.Namespace,
                        "tokenList",
                        new TokenListTemplate(x.classInfo, x.syntaxPart).TransformText())))
            .Union(
                Definition
                .SyntaxParts
                .Where(x =>
                    x.syntaxPart is DelimitedTextSyntax ||
                    x.syntaxPart is SyntaxList)
                .Select(x =>
                    new DefinitionGenerationInfo(
                        x.classInfo.Name,
                        x.classInfo.Namespace,
                        "contentInterface",
                        new ContentInterfaceTemplate(
                            x.classInfo, 
                            x.syntaxPart).TransformText())))
            .ToArray();
        }
    }
}
