using System.Collections.Generic;
using System.Linq;
using Analisador.Lexer;
using Analisador.Model;
using sly.lexer;
using sly.parser;
using sly.parser.generator;
using sly.parser.parser;

namespace Analisador.CParser
{
    public class CParser
    {
        [Production("program: statements")]
        public AST Program(AST block)
        {
            return block;
        }

        [Production("statements: statement")]
        public AST Statements(AST statement)
        {
            return statement;
        }

        [Production("statements: statement statements+")]
        public AST Statements(AST statement, List<AST> statements)
        {
            var seq = new SequenceStatement(statement);

            foreach (var ast in statements)
            {
                if (ast is SequenceStatement sequenceStatement)
                    seq.AddRange(sequenceStatement.Statements);
                else
                    seq.Add(ast);
            }

            return seq;
        }

        [Production("statement: intdeclaration")]
        [Production("statement: chardeclaration")]
        [Production("statement: floatdeclaration")]
        [Production("statement: ifstatement")]
        [Production("statement: assignstatement")]
        [Production("statement: switchstatement")]
        public AST Statement(AST declaration)
        {
            return declaration;
        }

        [Production("intdeclaration: INT ids SEMI")]
        public AST IntDeclaration(Token<Tokens> intToken, AST statement, Token<Tokens> semiToken)
        {
            IntDeclarationStatement intDeclaration;

            if (statement is SequenceStatement sequenceStatement)
                intDeclaration = new IntDeclarationStatement(sequenceStatement.Statements)
                {
                    Position = intToken.Position
                };
            else
                intDeclaration = new IntDeclarationStatement(statement)
                {
                    Position = intToken.Position
                };

            return intDeclaration;
        }

        [Production("chardeclaration: CHAR ids SEMI")]
        public AST CharDeclaration(Token<Tokens> charToken, AST statement, Token<Tokens> semiToken)
        {
            CharDeclarationStatement charDeclaration;

            if (statement is SequenceStatement sequenceStatement)
                charDeclaration = new CharDeclarationStatement(sequenceStatement.Statements)
                {
                    Position = charToken.Position
                };
            else
                charDeclaration = new CharDeclarationStatement(statement)
                {
                    Position = charToken.Position
                };

            return charDeclaration;
        }

        [Production("floatdeclaration: FLOAT ids SEMI")]
        public AST FloatDeclaration(Token<Tokens> floatToken, AST statement, Token<Tokens> semiToken)
        {
            FloatDeclarationStatement floatDeclaration;

            if (statement is SequenceStatement sequenceStatement)
                floatDeclaration = new FloatDeclarationStatement(sequenceStatement.Statements)
                {
                    Position = floatToken.Position
                };
            else
                floatDeclaration = new FloatDeclarationStatement(statement)
                {
                    Position = floatToken.Position
                };

            return floatDeclaration;
        }

        [Production("ifstatement: IF LPAREN CParser_expressions RPAREN LBRACKET statements? RBRACKET elsestatement?")]
        [Production("ifstatement: IF LPAREN boolean RPAREN LBRACKET statements? RBRACKET elsestatement?")]
        public AST IfStatement(Token<Tokens> ifToken, Token<Tokens> lparenToken, Expression condition, Token<Tokens> rparenToken, Token<Tokens> lbracketToken, ValueOption<AST> thenStatement, Token<Tokens> rbracketToken, ValueOption<AST> elseStatement)
        {
            var then = thenStatement.Match(ast => ast, () => null);

            var elseStm = elseStatement.Match(ast => ast, () => null);

            var ifStatement = new IfStatement(condition, then, elseStm);

            return ifStatement;
        }

        [Production("elsestatement: ELSE ifstatement")]
        public AST ElseStatement(Token<Tokens> elseToken, AST ifStatement)
        {
            return ifStatement;
        }

        [Production("elsestatement: ELSE LBRACKET statements? RBRACKET")]
        public AST ElseStatement(Token<Tokens> elseToken, Token<Tokens> lbracketToken, ValueOption<AST> statements, Token<Tokens> rbracketToken)
        {
            return statements.Match(ast => ast, () => null);
        }

        [Production("switchstatement: SWITCH LPAREN ID RPAREN LBRACKET cases RBRACKET")]
        public AST SwitchStatement(Token<Tokens> switchToken, Token<Tokens> lparenToken, Token<Tokens> idToken,
            Token<Tokens> rparenToken, Token<Tokens> lbracketToken, AST cases, Token<Tokens> rbracketToken)
        {
            SwitchStatement switchStatement;

            var identifierStatement = new IdentifierStatement(idToken.StringWithoutQuotes);

            if (cases is SequenceStatement sequenceStatement)
                switchStatement = new SwitchStatement(identifierStatement, sequenceStatement.Statements);
            else
                switchStatement = new SwitchStatement(identifierStatement, new List<AST> { cases });

            return switchStatement;
        }

        [Production("cases: CASE literal COLON LBRACKET [d] statements finish SEMI RBRACKET [d]")]
        public AST CaseStatement(Token<Tokens> caseToken, AST literal, Token<Tokens> colonToken, AST statements,
            AST finish, Token<Tokens> semiToken)
        {
            var @case = new CaseStatement(literal, statements);

            return @case;
        }

        [Production("cases: CASE literal COLON LBRACKET [d] statements finish SEMI RBRACKET [d] cases+")]
        public AST CaseStatement(Token<Tokens> caseToken, AST literal, Token<Tokens> colonToken, AST statements,
            AST finish, Token<Tokens> semiToken, List<AST> cases)
        {
            var c = new CaseStatement(literal, statements);

            var seq = new SequenceStatement(c);

            foreach (var @case in cases)
            {
                if (@case is SequenceStatement sequenceStatement)
                    seq.AddRange(sequenceStatement.Statements);
                else
                    seq.Add(@case);
            }

            return seq;
        }

        [Production("cases: DEFAULT COLON LBRACKET [d] statements finish SEMI RBRACKET [d]")]
        public AST CaseStatement(Token<Tokens> defaulToken, Token<Tokens> colonToken, AST statements, AST finish, Token<Tokens> semiToken)
        {
            return new DefaultCaseStatement(null, statements)
            {
                Position = defaulToken.Position
            };
        }

        [Production("finish: BREAK")]
        public AST FinishStatement(Token<Tokens> breakToken)
        {
            return null;
        }

        [Production("finish: return")]
        public AST FinishStatement(AST returnStatement)
        {
            return returnStatement;
        }

        [Production("return: RETURN operand?")]
        public AST ReturnStatement(Token<Tokens> returnToken, ValueOption<AST> operand)
        {
            var operandValue = operand.Match(ast => ast, () => null);

            return new ReturnStatement(operandValue);
        }

        [Production("assignstatement: location assign expression SEMI")]
        [Production("assignstatement: location assign CParser_expressions SEMI")]
        public AST AssignStatement(AST location, AssignType assignType, Expression expression, Token<Tokens> semiToken)
        {
            var identifier = location as IdentifierStatement;

            return new AssignStatement(identifier.VariableName, assignType, expression)
            {
                Position = identifier.Position
            };
        }

        [Production("assign: ASSIGN")]
        [Production("assign: PLUSASSIGN")]
        [Production("assign: MINUSASSIGN")]
        [Production("assign: MULASSIGN")]
        [Production("assign: DIVIDEASSIGN")]
        public AST Assign(Token<Tokens> assignToken)
        {
            return new AssignType(assignToken);
        }

        [Production("ids: ID")]
        public AST IdDeclaration(Token<Tokens> idToken)
        {
            return new IdentifierStatement(idToken.StringWithoutQuotes)
            {
                Position = idToken.Position
            };
        }

        [Production("ids: ID COMMA ids+")]
        public AST IdDeclaration(Token<Tokens> idToken, Token<Tokens> commaToken, List<AST> ids)
        {
            var identifier = new IdentifierStatement(idToken.StringWithoutQuotes)
            {
                Position = idToken.Position
            };

            var sequence = new SequenceStatement(identifier);

            sequence.AddRange(ids);

            return sequence;
        }

        [Production("expression: operand")]
        public AST Expression(Expression operand)
        {
            return operand;
        }

        [Operand]
        [Production("operand: literal")]
        public AST Operand(AST literal)
        {
            return literal;
        }

        [Operand]
        [Production("operand: location")]
        public AST OperandLocation(AST location)
        {
            return location;
        }


        [Production("literal: INTEGER")]
        public AST LiteralInteger(Token<Tokens> integerToken)
        {
            return new IntegerConstant(integerToken.IntValue)
            {
                Position = integerToken.Position
            };
        }

        [Production("literal: STRING")]
        public AST LiteralString(Token<Tokens> stringToken)
        {
            return new StringConstant(stringToken.StringWithoutQuotes)
            {
                Position = stringToken.Position
            };
        }

        [Production("literal: FLOATNUMBER")]
        public AST LiteralFloat(Token<Tokens> floatToken)
        {
            return new FloatConstant(double.Parse(floatToken.StringWithoutQuotes.Replace(",", "").Replace(".", ",")))
            {
                Position = floatToken.Position
            };
        }

        [Production("location: ID")]
        public AST Location(Token<Tokens> idToken)
        {
            return new IdentifierStatement(idToken.StringWithoutQuotes)
            {
                Position = idToken.Position
            };
        }

        [Production("boolean: TRUE")]
        [Production("boolean: FALSE")]
        public AST Boolean(Token<Tokens> booleanToken)
        {
            return new BooleanConstant(bool.Parse(booleanToken.StringWithoutQuotes))
            {
                Position = booleanToken.Position
            };
        }

        [Operation((int)Tokens.LESSER, Affix.InFix, Associativity.Right, 50)]
        [Operation((int)Tokens.LESSEROREQUAL, Affix.InFix, Associativity.Right, 50)]
        [Operation((int)Tokens.GREATER, Affix.InFix, Associativity.Right, 50)]
        [Operation((int)Tokens.GREATEROREQUAL, Affix.InFix, Associativity.Right, 50)]
        [Operation((int)Tokens.EQUALS, Affix.InFix, Associativity.Right, 50)]
        [Operation((int)Tokens.DIFFERENT, Affix.InFix, Associativity.Right, 50)]
        public AST BinaryComparisonExpression(AST left, Token<Tokens> operatorToken,
            AST right)
        {
            var oper = operatorToken.TokenID switch
            {
                Tokens.LESSER => BinaryOperator.LESSER,
                Tokens.LESSEROREQUAL => BinaryOperator.LESSEROREQUAL,
                Tokens.GREATER => BinaryOperator.GREATER,
                Tokens.GREATEROREQUAL => BinaryOperator.GREATEROREQUAL,
                Tokens.EQUALS => BinaryOperator.EQUALS,
                Tokens.DIFFERENT => BinaryOperator.DIFFERENT,
                _ => BinaryOperator.ADD
            };

            var operation = new BinaryOperation(left as Expression, oper, right as Expression);
            return operation;
        }

        [Operation((int)Tokens.PLUS, Affix.InFix, Associativity.Right, 10)]
        [Operation((int)Tokens.MINUS, Affix.InFix, Associativity.Right, 10)]
        public AST binaryTermNumericExpression(AST left, Token<Tokens> operatorToken, AST right)
        {
            var oper = BinaryOperator.ADD;

            switch (operatorToken.TokenID)
            {
                case Tokens.PLUS:
                    {
                        oper = BinaryOperator.ADD;
                        break;
                    }
                case Tokens.MINUS:
                    {
                        oper = BinaryOperator.SUB;
                        break;
                    }
            }

            var operation = new BinaryOperation(left as Expression, oper, right as Expression);
            return operation;
        }

        [Operation((int)Tokens.MUL, Affix.InFix, Associativity.Right, 50)]
        [Operation((int)Tokens.DIVIDE, Affix.InFix, Associativity.Right, 50)]
        public AST binaryFactorNumericExpression(AST left, Token<Tokens> operatorToken, AST right)
        {
            var oper = BinaryOperator.MULTIPLY;

            switch (operatorToken.TokenID)
            {
                case Tokens.MUL:
                    {
                        oper = BinaryOperator.MULTIPLY;
                        break;
                    }
                case Tokens.DIVIDE:
                    {
                        oper = BinaryOperator.DIVIDE;
                        break;
                    }
            }

            var operation = new BinaryOperation(left as Expression, oper, right as Expression);
            return operation;
        }

        public static Parser<Tokens, AST> GetParser()
        {
            var parserInstance = new CParser();
            var builder = new ParserBuilder<Tokens, AST>();
            var buildResult = builder.BuildParser(parserInstance, ParserType.EBNF_LL_RECURSIVE_DESCENT, "program");

            if (buildResult.IsOk)
                return buildResult.Result;

            return null;
        }
    }
}