using PascalABCCompiler.SemanticTree;
using PascalABCCompiler.SyntaxTree;
using PascalABCCompiler.SystemLibrary;
using PascalABCCompiler.TreeRealization;
using SymTable = SymbolTable;

namespace PascalABCCompiler.TreeConverter
{
    public partial class syntax_tree_visitor
    {



        bool VisitTypeclassDeclaration(type_declaration typeclassDeclaration)
        {
            var typeclassDefinition = typeclassDeclaration.type_def as typeclass_definition;
            if (typeclassDefinition == null)
            {
                return false;
            }

            var typeclassName = typeclassDeclaration.type_name as typeclass_restriction;
            
            if (typeclassName.restriction_args.Count == 0)
            {
                // TODO: AddError
            }

            if (typeclassName.restriction_args.Count > 1)
            {
                throw new NotSupportedError(get_location(typeclassName.restriction_args));
            }
/*            var typeclassScope = SymbolTable.CreateScope(context.CurrentScope);
            context.enter_scope(typeclassScope);

            context.leave_scope();
            */
            return true;
        }
    }
}