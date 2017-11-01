using PascalABCCompiler.SemanticTree;
using PascalABCCompiler.SyntaxTree;
using PascalABCCompiler.SystemLibrary;
using PascalABCCompiler.TreeRealization;
using SymTable = SymbolTable;

namespace PascalABCCompiler.TreeConverter
{
    public partial class syntax_tree_visitor
    {



        bool VisitInstanceDeclaration(type_declaration instanceDeclaration)
        {
            var instanceDefinition = instanceDeclaration.type_def as instance_definition;
            if (instanceDefinition == null)
            {
                return false;
            }

            var instanceName = instanceDeclaration.type_name as typeclass_restriction;

            if (instanceName.restriction_args.Count == 0)
            {
                // TODO: AddError
            }

            if (instanceName.restriction_args.Count > 1)
            {
                throw new NotSupportedError(get_location(instanceName.restriction_args));
            }
            /*            var typeclassScope = SymbolTable.CreateScope(context.CurrentScope);
                        context.enter_scope(typeclassScope);

                        context.leave_scope();
                        */
            return true;
        }
    }
}