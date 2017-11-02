using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PascalABCCompiler.Errors;
using PascalABCCompiler;
using PascalABCCompiler.SyntaxTree;

namespace SyntaxVisitors
{
    public class CollectTypeclassesVisitor: CollectUpperNodesVisitor
    {
        public Dictionary<string, Dictionary<string, List<declaration>>> typeclassInstanceDeclarations =
            new Dictionary<string, Dictionary<string, List<declaration>>>();

        public override void visit(type_declaration typeclassDeclaration)
        {
            var typeclassDefinition = typeclassDeclaration.type_def as typeclass_definition;
            if (typeclassDefinition == null)
            {
                return;
            }

            var typeclassName = typeclassDeclaration.type_name as typeclass_restriction;

            if (typeclassInstanceDeclarations.ContainsKey(typeclassName.name))
            {
                // AddError
            }
            else
            {
                typeclassInstanceDeclarations.Add(typeclassName.name, new Dictionary<string, List<declaration>>());
            }
        }
    }
}
