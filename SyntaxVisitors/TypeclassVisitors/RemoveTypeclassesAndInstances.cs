using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PascalABCCompiler.Errors;
using PascalABCCompiler;
using PascalABCCompiler.SyntaxTree;

namespace SyntaxVisitors
{
    public class RemoveTypeclassesAndInstances: BaseChangeVisitor
    {
        public override void visit(type_declarations _type_declarations)
        {
            for (int i = _type_declarations.Count - 1; i >= 0; i--)
            {
                if (_type_declarations.types_decl[i].type_def is instance_definition ||
                    _type_declarations.types_decl[i].type_def is typeclass_definition)
                    _type_declarations.types_decl.RemoveAt(i);
            }
        }
    }
}
