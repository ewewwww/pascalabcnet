using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PascalABCCompiler.Errors;
using PascalABCCompiler;
using PascalABCCompiler.SyntaxTree;

namespace SyntaxVisitors
{
    public class CollectRestrictedFunctionsVisitor: BaseChangeVisitor
    {
        public Dictionary<string, Dictionary<string, List<declaration>>> typeclassInstanceDeclarations =
            new Dictionary<string, Dictionary<string, List<declaration>>>();

        public CollectRestrictedFunctionsVisitor(Dictionary<string, Dictionary<string, List<declaration>>> typeclassInstanceDeclarations)
        {
            this.typeclassInstanceDeclarations = typeclassInstanceDeclarations;
        }

        public override void visit(procedure_definition _procedure_definition)
        {
            bool isConstrainted = _procedure_definition.proc_header.where_defs != null &&
                _procedure_definition.proc_header.where_defs.defs.Any(x => x is where_typeclass_constraint);
            if (!isConstrainted)
                return;

            

            foreach (var where in _procedure_definition.proc_header.where_defs.defs)
            {
                var whereC = (where as where_typeclass_constraint).restriction;
                var instances = typeclassInstanceDeclarations[whereC.name];
                foreach (var instance in instances.Values)
                {
                    (_procedure_definition.proc_body as block).defs.defs.AddRange(instance);
                }

                // substitution template type
                var restricted = whereC.restriction_args.params_list[0].ToString();
                var newType = instances.Keys.First();
                foreach (var param in _procedure_definition.proc_header.parameters.params_list)
                {
                    var type = param.vars_type.ToString();
                    if (type == restricted)
                        param.vars_type = new named_type_reference(newType);
                }

                var func = _procedure_definition.proc_header as function_header;
                if (func != null)
                {
                    var type = func.return_type.ToString();
                    if (type == restricted)
                        func.return_type = new named_type_reference(newType);
                }
            }

            _procedure_definition.proc_header.where_defs = null;
            _procedure_definition.proc_header.template_args = null;
        }
    }
}
