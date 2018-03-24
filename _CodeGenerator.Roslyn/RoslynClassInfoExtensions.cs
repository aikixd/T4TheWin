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
    internal static class RoslynClassInfoExtensions
    {
        public static Accessabilty ToInternalType(this Microsoft.CodeAnalysis.Accessibility accessibility)
        {
            switch (accessibility)
            {
                case Microsoft.CodeAnalysis.Accessibility.Public:
                    return Accessabilty.Public;

                case Microsoft.CodeAnalysis.Accessibility.Private:
                    return Accessabilty.Private;

                case Microsoft.CodeAnalysis.Accessibility.Internal:
                    return Accessabilty.Internal;

                case Microsoft.CodeAnalysis.Accessibility.Protected:
                    return Accessabilty.Protected;

                default:
                    throw new NotImplementedException();
            }
        }

        public static Domain.Type ToInternalType(this ITypeSymbol type)
        {
            return new Domain.Type(type.Name, type.ContainingNamespace.ToString());
        }

        public static IEnumerable<Domain.Attribute> ToInternalType(this IEnumerable<AttributeData> attrs)
        {
            return
                attrs
                .Select(x =>
                {
                    return x.ToInternalType();
                });
        }

        public static Domain.Attribute ToInternalType(this AttributeData attr)
        {
            if (attr.ConstructorArguments.Any() == false)
                return new Domain.Attribute(
                    attr.AttributeClass.ToInternalType(),
                    new Dictionary<Argument, object>());

            return
                new Domain.Attribute(
                    attr.AttributeClass.ToInternalType(),
                    attr.ConstructorArguments.Zip(attr.AttributeConstructor.Parameters, (args, param) => new { param, arg = args })
                    .ToDictionary(
                        y => new Argument(y.param.Name, y.arg.Type.ToInternalType()),
                        y => y.arg.Value));
        }

        public static Domain.Attribute ToInternalType(this AttributeSyntax attr, SemanticModel semanticModel)
        {
            var attrType = semanticModel.GetTypeInfo(attr).Type as INamedTypeSymbol;

            if (attr.ArgumentList == null)
                return new Domain.Attribute(attrType.ToInternalType(), new Dictionary<Argument, object>());

            var args =
                attr
                .ArgumentList
                .Arguments
                .Select(x => new { arg = x, type = x.GetArgumentType(semanticModel) })
                .ToArray();

            var ctor =
                attrType.Constructors
                .Single(x =>
                {
                    return 
                        x.Parameters
                        .Select(y => y.Type.ToInternalType())
                        .SequenceEqual(args.Select(y => y.type));
                });

            var namedArgs =
                args
                .Zip(
                    ctor.Parameters,
                    (arg, param) => new { arg = arg.arg, type = arg.type, name = param.Name, value = arg.arg.GetArgumentValue(semanticModel) })
                .ToArray();

            return
                new Domain.Attribute(
                    attrType.ToInternalType(),
                    namedArgs
                    .ToDictionary(
                        x => new Argument(
                            x.name,
                            x.type),
                        x => x.value)
                    );
        }

        public static Domain.Type GetArgumentType(this AttributeArgumentSyntax arg, SemanticModel semanticModel)
        {
            var argExpr = arg.ChildNodes().First();

            return 
                (argExpr as MemberAccessExpressionSyntax)?.GetMemberType(semanticModel) ??
                (argExpr as LiteralExpressionSyntax)?.GetLiteralType(semanticModel) ??
                throw new NotImplementedException();
        }

        public static Domain.Type GetMemberType(this MemberAccessExpressionSyntax member, SemanticModel semanticModel)
        {
            var node = member.ChildNodes().First();

            if (node is IdentifierNameSyntax name)
                return semanticModel.GetTypeInfo(name).Type.ToInternalType();

            throw new NotImplementedException();
        }

        public static Domain.Type GetLiteralType(this LiteralExpressionSyntax member, SemanticModel semanticModel)
        {
            throw new NotImplementedException();
        }

        public static object GetArgumentValue(this AttributeArgumentSyntax arg, SemanticModel semanticModel)
        {
            var argExpr = arg.ChildNodes().First();

            return
                (argExpr as MemberAccessExpressionSyntax)?.GetMemberValue(semanticModel) ??
                throw new NotImplementedException();
        }

        public static object GetMemberValue(this MemberAccessExpressionSyntax member, SemanticModel semanticModel)
        {
            var node = member.ChildNodes().Skip(1).First();

            if (node is IdentifierNameSyntax name)
            {
                var memberSymbol = semanticModel.GetSymbolInfo(name).Symbol;

                var constant = semanticModel.GetConstantValue(name);

                if (constant.HasValue)
                    return constant.Value;               
            }

            throw new NotImplementedException();
        }
    }
}