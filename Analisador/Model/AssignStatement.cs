using System;
using System.ComponentModel;
using System.Formats.Asn1;
using System.Text;
using sly.lexer;

namespace Analisador.Model
{
    public class AssignStatement : Statement
    {
        public AssignStatement(string variableName, AssignType assignType, Expression value)
        {
            VariableName = variableName;
            AssignType = assignType;
            Value = value;
        }

        public string VariableName { get; set; }
        public AssignType AssignType { get; set; }
        public Expression Value { get; set; }

        public AsnWriter.Scope CompilerScope { get; set; }

        public LexerPosition Position { get; set; }

        public string Dump(string tab)
        {
            var dmp = new StringBuilder();
            dmp.AppendLine($"{tab}(ASSIGN");
            dmp.AppendLine($"{tab}\t{VariableName}");
            dmp.AppendLine(Value.Dump(tab + "\t"));
            dmp.AppendLine($"{tab})");
            return dmp.ToString();
        }

        //public string Transpile(CompilerContext context)
        //{
        //    var code = new StringBuilder();
        //    if (IsVariableCreation)
        //        code.AppendLine(
        //            $"{TypeConverter.WhileToCSharp(CompilerScope.GetVariableType(VariableName))} {VariableName};");
        //    code.AppendLine($"{VariableName} = {Value.Transpile(context)};");
        //    return code.ToString();
        //}

        //public Emit<Func<int>> EmitByteCode(CompilerContext context, Emit<Func<int>> emiter)
        //{
        //    Local local = null;
        //    if (IsVariableCreation)
        //        local = emiter.DeclareLocal(TypeConverter.WhileToType(CompilerScope.GetVariableType(VariableName)),
        //            VariableName);
        //    else
        //        local = emiter.Locals[VariableName];
        //    Value.EmitByteCode(context, emiter);
        //    emiter.StoreLocal(local);
        //    return emiter;
        //}
    }
}