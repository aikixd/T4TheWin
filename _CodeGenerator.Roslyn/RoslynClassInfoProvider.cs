using _CodeGenerator.Domain;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _CodeGenerator.Roslyn
{
    class RoslynClassInfoProvider : IClassInfoProvider
    {
        private ClassDeclarationSyntax classDeclaration;
        private SemanticModel semanticModel;

        private INamedTypeSymbol classInfo;

        public RoslynClassInfoProvider(ClassDeclarationSyntax declaration, SemanticModel semanticModel)
        {
            this.classDeclaration = declaration;
            this.semanticModel = semanticModel;

            this.classInfo = (INamedTypeSymbol)semanticModel.GetDeclaredSymbol(declaration);
        }


        public string Name => 
            classInfo.Name;

        public string Namespace => 
            classInfo.ContainingNamespace.ToString();

        public Accessabilty Accessability => 
            classInfo.DeclaredAccessibility.ToInternalType();

        public bool IsStatic => 
            classInfo.IsStatic;

        public bool IsSealed => throw new NotImplementedException();

        public IEnumerable<Domain.Attribute> Attributes =>
            // Can't use semantic model here, seems Roslyn needs references
            // to system assemblies to validate that the attribute actually
            // derives from System.Attribute
            
            classDeclaration
            .AttributeLists
            .SelectMany(x => x.Attributes)
            .Select(x => x.ToInternalType(semanticModel));

        public IEnumerable<ClassMember> Members =>
            classInfo.GetMembers()
            .Where(x => x.Kind == SymbolKind.Field || x.Kind == SymbolKind.Property)
            .Where(x => x.IsImplicitlyDeclared == false)
            .Select(x =>
            {
                switch (x)
                {
                    case IFieldSymbol fld:
                        return new ClassMember(
                            fld.Name,
                            fld.Type.ToInternalType(),
                            (fld
                            .DeclaringSyntaxReferences[0]
                            .GetSyntax()
                            .Parent.Parent as FieldDeclarationSyntax)
                            .AttributeLists
                                .SelectMany(y => y.Attributes)
                                .Select(y => y.ToInternalType(semanticModel)));

                        //classDeclaration
                        //        .Members
                        //        .OfType<FieldDeclarationSyntax>()
                        //        .Single(y => semanticModel.GetDeclaredSymbol(y.DescendantNodes().OfType<VariableDeclaratorSyntax>().Single()) == fld)
                        //        .AttributeLists
                        //        .SelectMany(y => y.Attributes)
                        //        .Select(y => y.ToInternalType(semanticModel)));

                    case IPropertySymbol prop:
                        return new ClassMember(
                            prop.Name,
                            prop.Type.ToInternalType(),
                            classDeclaration
                                .Members
                                .OfType<PropertyDeclarationSyntax>()
                                .Single(y => semanticModel.GetDeclaredSymbol(y) == prop)
                                .AttributeLists
                                .SelectMany(y => y.Attributes)
                                .Select(y => y.ToInternalType(semanticModel)));
    }

                throw new ArgumentOutOfRangeException("symbol", "Symbol must be either field or property.");
            });
    }
}
