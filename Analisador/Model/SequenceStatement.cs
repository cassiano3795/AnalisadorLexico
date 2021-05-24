using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Text;
using sly.lexer;

namespace Analisador.Model
{
    public class SequenceStatement : Statement
    {
        public SequenceStatement()
        {
            Statements = new List<AST>();
        }

        public SequenceStatement(AST statement)
        {
            Statements = new List<AST> { statement };
        }

        public SequenceStatement(List<AST> seq)
        {
            Statements = seq;
        }

        private List<AST> Statements { get; }
        public int Count => Statements.Count;

        public AsnWriter.Scope CompilerScope { get; set; }

        public LexerPosition Position { get; set; }

        public string Dump(string tab)
        {
            var dump = new StringBuilder();
            dump.AppendLine($"{tab}(SEQUENCE [");
            Statements.ForEach(c => dump.AppendLine($"{c.Dump(tab + "\t")},"));
            dump.AppendLine($"{tab}] )");
            return dump.ToString();
        }

        //public string Transpile(CompilerContext context)
        //{
        //    var block = new StringBuilder("{\n");
        //    foreach (var stmt in Statements)
        //    {
        //        block.Append(stmt.Transpile(context));
        //        block.AppendLine(";");
        //    }

        //    block.AppendLine("}");
        //    return block.ToString();
        //}

        //public Emit<Func<int>> EmitByteCode(CompilerContext context, Emit<Func<int>> emiter)
        //{
        //    foreach (var stmt in Statements) emiter = stmt.EmitByteCode(context, emiter);
        //    return emiter;
        //}

        public AST Get(int i)
        {
            return Statements[i];
        }

        public void Add(AST statement)
        {
            Statements.Add(statement);
        }

        public void AddRange(List<AST> stmts)
        {
            Statements.AddRange(stmts);
        }
    }
}