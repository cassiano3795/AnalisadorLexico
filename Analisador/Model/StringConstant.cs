using System;
using System.Formats.Asn1;
using sly.lexer;

namespace Analisador.Model
{
    public class StringConstant : Expression
    {
        public StringConstant(string value)
        {
            Value = value;
        }

        public string Value { get; set; }

        public AsnWriter.Scope CompilerScope { get; set; }

        public LexerPosition Position { get; set; }

        //public WhileType Whiletype
        //{
        //    get => WhileType.STRING;
        //    set { }
        //}

        public string Dump(string tab)
        {
            return $"{tab}(STRING {Value})";
        }

        //public string Transpile(CompilerContext context)
        //{
        //    return $"\"{Value}\"";
        //}

        //public Emit<Func<int>> EmitByteCode(CompilerContext context, Emit<Func<int>> emiter)
        //{
        //    emiter.LoadConstant(Value);
        //    return emiter;
        //}
    }
}