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



        Dictionary<string, procedure_definition> constraintedFuncDecl = new Dictionary<string, procedure_definition>();



        // TODO: Need to rename substracted parts.
        Dictionary<string, List<typeclass_restriction>> functionsCosntraints = new Dictionary<string, List<typeclass_restriction>>();



        bool VisitWhereTypeclassConstraint(where_definition whereDefinition)
        {
            var whereTypeclassConstraint = whereDefinition as where_typeclass_constraint;
            if (whereTypeclassConstraint == null)
            {
                return false;
            }

            string name = whereTypeclassConstraint.restriction.name;
            string funcName = context.func_stack.top().name;
            if (functionsCosntraints.ContainsKey(funcName))
                functionsCosntraints[funcName].Add(whereTypeclassConstraint.restriction);
            else
            {
                functionsCosntraints[funcName] = new List<typeclass_restriction>();
                functionsCosntraints[funcName].Add(whereTypeclassConstraint.restriction);
            }

            constraintedFuncDecl[funcName] = whereDefinition.Parent.Parent as procedure_definition;
            //(whereDefinition.Parent as function_header).
            /*
            foreach (var decl in typeclassDictionary[name])
            {
                decl.visit(this);
            }
            */
            return true;
        }



        void GenerateFuncInstance()
        {

        }



        void AddConstraintDefinitions(procedure_definition _procedure_definition)
        {
            var funcName = _procedure_definition.proc_header.name.meth_name.name;
            // TODO: I need something more sufficient
            if (functionsCosntraints.ContainsKey(funcName))
            {
                // TODO: Substitution
                var defs = (_procedure_definition.proc_body as block).defs;

                foreach (var constraint in functionsCosntraints[funcName])
                {
                    defs.list.AddRange(typeclassDictionary[constraint.name]);
                }
            }
        }
    }
}