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


        
        Dictionary<string, Dictionary<string, type_declaration>> instances = new Dictionary<string, Dictionary<string, type_declaration>>();



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

            if (instances.ContainsKey(instanceName.name))
                instances[instanceName.name].Add(instanceName.restriction_args[0].ToString(), instanceDeclaration);
            else
            {
                instances.Add(instanceName.name, new Dictionary<string, type_declaration>());
                instances[instanceName.name].Add(instanceName.restriction_args[0].ToString(), instanceDeclaration);
            }
            
            return true;
        }
    }
}