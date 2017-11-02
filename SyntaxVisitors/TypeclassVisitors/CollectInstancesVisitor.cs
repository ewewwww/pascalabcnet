using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PascalABCCompiler.Errors;
using PascalABCCompiler;
using PascalABCCompiler.SyntaxTree;

namespace SyntaxVisitors
{
    public class CollectInstancesVisitor: CollectUpperNodesVisitor
    {
        public Dictionary<string, Dictionary<string, List<declaration>>> typeclassInstanceDeclarations =
            new Dictionary<string, Dictionary<string, List<declaration>>>();

        public CollectInstancesVisitor(Dictionary<string, Dictionary<string, List<declaration>>> typeclassInstanceDeclarations)
        {
            this.typeclassInstanceDeclarations = typeclassInstanceDeclarations;
        }

        public override void visit(type_declaration instanceDeclaration)
        {
            var instanceDefinition = instanceDeclaration.type_def as instance_definition;
            if (instanceDefinition == null)
            {
                return;
            }

            var instanceName = instanceDeclaration.type_name as typeclass_restriction;
            var instances = typeclassInstanceDeclarations[instanceName.name];
            var restrictedType = instanceName.restriction_args.params_list[0].ToString();
            if (!instances.ContainsKey(restrictedType))
            {
                instances.Add(restrictedType, new List<declaration>());
            }
            foreach (var defBlock in instanceDefinition.body.class_def_blocks)
            {
                instances[restrictedType].AddRange(defBlock.members);
            }
        }
    }
}
