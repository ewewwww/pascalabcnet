using PascalABCCompiler.SemanticTree;
using PascalABCCompiler.SyntaxTree;
using System.Collections.Generic;
using PascalABCCompiler.SystemLibrary;
using PascalABCCompiler.TreeRealization;
using SymTable = SymbolTable;

namespace PascalABCCompiler.TreeConverter
{
    public partial class syntax_tree_visitor
    {


        // TODO: Replace string key something more sufficient
        Dictionary<string, List<declaration>> typeclassDictionary = new Dictionary<string, List<declaration>>();



        bool VisitTypeclassDeclaration(type_declaration typeclassDeclaration)
        {
            var typeclassDefinition = typeclassDeclaration.type_def as typeclass_definition;
            if (typeclassDefinition == null)
            {
                return false;
            }

            var typeclassName = typeclassDeclaration.type_name as typeclass_restriction;
            
            // Checks
            if (typeclassName.restriction_args.Count == 0)
            {
                // TODO: AddError
            }

            if (typeclassName.restriction_args.Count > 1)
            {
                throw new NotSupportedError(get_location(typeclassName.restriction_args));
            }

            typeclassDictionary[typeclassName.name] = new List<declaration>();
            // Gather information
            foreach (var classMembers in typeclassDefinition.body.class_def_blocks)
            {
                typeclassDictionary[typeclassName.name].AddRange(classMembers.members);
            }

            return true;
        }
    }
}