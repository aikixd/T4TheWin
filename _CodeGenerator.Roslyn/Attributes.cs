using _CodeGenerator.Domain;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CodeGenerator.Roslyn
{
    public static class Attributes
    {
        public static IEnumerable<IClassInfoProvider> FindDecoratedWith<TAttribute>(SemanticModel semanticModel)
        {
            return
                semanticModel.SyntaxTree.GetRoot()
                .DescendantNodes().OfType<ClassDeclarationSyntax>()
                .Where(cls =>
                    cls.AttributeLists.Any(list =>
                        list.Attributes.Any(attr =>
                        {
                            var symbol = semanticModel.GetTypeInfo(attr).Type;

                            return 
                                typeof(TAttribute).FullName == $"{symbol.ContainingNamespace}.{symbol.Name}" &&
                                typeof(TAttribute).Assembly.GetName().Name == symbol.ContainingAssembly.Name;
                        })))
                .Select(x => new RoslynClassInfoProvider(x, semanticModel));
        }
    }
}
