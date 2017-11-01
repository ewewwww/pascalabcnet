using PascalABCCompiler.SemanticTree;
using PascalABCCompiler.SyntaxTree;
using PascalABCCompiler.SystemLibrary;
using PascalABCCompiler.TreeRealization;
using SymTable = SymbolTable;

namespace PascalABCCompiler.TreeConverter
{
    public partial class syntax_tree_visitor
    {



        bool VisitWhereTypeclassConstraint(where_definition whereDefinition)
        {
            var whereTypeclassConstraint = whereDefinition as where_typeclass_constraint;
            if (whereTypeclassConstraint == null)
            {
                return false;
            }

            return true;
        }
    }
}