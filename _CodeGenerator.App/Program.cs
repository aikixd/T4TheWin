﻿using _CodeGenerator.App.Templates;
using _CodeGenerator.Definitions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _CodeGenerator.App
{
    class Program
    {
        static void Main(string[] args)
        {
            // Workaround for not finding the correct tooling.
            var registryKey = $@"SOFTWARE{(Environment.Is64BitProcess ? @"\Wow6432Node" : string.Empty)}\Microsoft\VisualStudio\SxS\VS7";
            using (RegistryKey subKey = Registry.LocalMachine.OpenSubKey(registryKey))
            {
                string visualStudioPath = subKey?.GetValue("15.0") as string;
                if (!string.IsNullOrEmpty(visualStudioPath))
                {
                    Environment.SetEnvironmentVariable("VSINSTALLDIR", visualStudioPath);
                    Environment.SetEnvironmentVariable("VisualStudioVersion", @"15.0");
                }
            }

            var workspace = MSBuildWorkspace.Create();

            workspace.LoadMetadataForReferencedProjects = true;

            workspace.WorkspaceFailed += Workspace_WorkspaceFailed;

            var projectsDefs = new Dictionary<string, DefinitionGenerationInfo[]>
            {
                {
                    Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\T4TW.Vsix\T4TW.Vsix.csproj")),
                    new []
                    {
                        new DefinitionGenerationInfo(
                            $"{DefinitionGeneral.ClassifierName}",
                            $"{DefinitionGeneral.ClassifierNamespace}",
                            "formats",
                            new ClassificationFormatsTemplate(
                                DefinitionGeneral.ClassifierNamespace,
                                Definition.TemplateFormats).TransformText()),

                        new DefinitionGenerationInfo(
                            $"{DefinitionGeneral.ClassifierName}",
                            $"{DefinitionGeneral.ClassifierNamespace}",
                            "classifications",
                            new ClassificationDefinitionsTemplate(Definition.ClassificationDefinition).TransformText())
                    } },
                {
                    Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\T4TW.VS.Syntax\T4TW.VS.Syntax.csproj")),
                    new[]
                    {
                        new DefinitionGenerationInfo(
                            Definition.Whitespace.classInfo.Name,
                            Definition.Whitespace.classInfo.Namespace,
                            "ext",
                            new ScannerTemplate(
                                Definition.Whitespace.classInfo,
                                Definition.Whitespace.whitespace)
                                .TransformText())
                    } }

            };

            //var p1 = workspace.OpenProjectAsync(projectPaths[0]).Result;

            foreach (var p in projectsDefs)
                workspace.OpenProjectAsync(p.Key).Wait();

            var result = 
                workspace.TryApplyChanges(
                Generate(
                    workspace.CurrentSolution, 
                    workspace
                    .CurrentSolution
                    .ProjectIds
                    .ToArray(),
                    projectsDefs));

            if (result == false)
                throw new Exception("Can't apply changes.");
        }

        private static void Workspace_WorkspaceFailed(object sender, WorkspaceDiagnosticEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static string[] GetOldGeneratedFiles(string generatedFilesDir)
        {
            if (Directory.Exists(generatedFilesDir) == false)
                return new string[0];

            return 
                Directory
                .GetFiles(generatedFilesDir, "*", SearchOption.AllDirectories)
                .ToArray();
        }

        private static Solution Generate(Solution solution, ProjectId[] projectIds, Dictionary<string, DefinitionGenerationInfo[]> defs)
        {
            foreach (var id in projectIds)
            {
                var project = solution.GetProject(id);

                var generatedFilesDir = Path.Combine(Path.GetDirectoryName(project.FilePath) + @"\AutoGenerated\");
                var oldGeneratedFiles = GetOldGeneratedFiles(generatedFilesDir);

                var generatedList = new LinkedList<string>();


                solution =
                    project
                    .GenerateFromDefinition(generatedFilesDir, defs[project.FilePath]).DetachAddedFiles(generatedList)
                    .ExtendDecoratedClasses(generatedFilesDir).DetachAddedFiles(generatedList)
                    .RemoveFiles(oldGeneratedFiles.Except(generatedList))     
                    .Solution;
            }

            return solution;
        }

        //private static void GenerateForProject(MSBuildWorkspace workspace, string projectFilePath)
        //{
        //    var projDir = Path.GetDirectoryName(projectFilePath);
        //    var generatedFilesDir = Path.Combine(projDir + @"\AutoGenerated\");

        //    var project = workspace.OpenProjectAsync(projectFilePath).Result;

        //    workspace.TryApplyChanges(
        //        project
        //        .RemoveOldGeneratedFiles(generatedFilesDir)
        //        .Solution
        //    );


        //    workspace.TryApplyChanges(
        //        project
        //        .ExtendDecoratedClasses(generatedFilesDir)
        //        .Solution
        //    );
        //}
    }
}