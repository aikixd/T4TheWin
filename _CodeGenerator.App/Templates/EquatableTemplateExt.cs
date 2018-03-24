using _CodeGenerator.Bridge;
using _CodeGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CodeGenerator.App.Templates
{
    partial class EquatableTemplate
    {
        public IClassInfoProvider Class { get; }

        public EquatableTemplate(IClassInfoProvider classInfo)
        {
            this.Class = classInfo;
        }

        private IEnumerable<ClassMember> GetParticipatingMembers()
        {
            if (this
                .Class
                .Attributes
                .Any(x =>
                    x.Type.IsSameType(typeof(GenerateEqualityAttribute)) &&
                    x.Arguments.Any(y =>
                        y.Key.Type.IsSameType(typeof(MemberDiscovery)) &&
                        (MemberDiscovery)y.Value == MemberDiscovery.OptIn)))
                return GetOptInMembers();

            return GetRegualrMembers();

        }

        private IEnumerable<ClassMember> GetOptInMembers()
        {
            var r =
                this
                .Class
                .Members
                .Where(x =>
                    x.Attributes.Any(y =>
                        y.Type.IsSameType(typeof(MemberAttribute)) &&
                        y.Arguments.Any(z =>
                            z.Key.Type.IsSameType(typeof(MemberDiscovery)) &&
                            (MemberDiscovery)z.Value == MemberDiscovery.Include)));

            if (r.Any() == false)
                throw new InvalidOperationException("Equatable has no members.");

            return r;
        }

        private IEnumerable<ClassMember> GetRegualrMembers()
        {
            var r =
                this
                .Class
                .Members
                .Where(x =>
                    x.Attributes.Any(y =>
                        y.Type.IsSameType(typeof(MemberAttribute)) &&
                        y.Arguments.Any(z =>
                            z.Key.Type.IsSameType(typeof(MemberDiscovery)) &&
                            (MemberDiscovery)z.Value != MemberDiscovery.Exclude)));

            if (r.Any() == false)
                throw new InvalidOperationException("Equatable has no members.");

            return r;
        }
    }
}
