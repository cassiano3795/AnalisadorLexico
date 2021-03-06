using sly.lexer;

namespace Analisador.Lexer
{
    [Lexer(IgnoreEOL = true)]
    public enum Tokens
    {
        #region Literals

        [Lexeme(GenericToken.Identifier, IdentifierType.AlphaNumericDash)]
        ID,

        [Lexeme(GenericToken.String)]
        STRING,

        [Lexeme(GenericToken.Int)]
        INTEGER,

        [Lexeme(GenericToken.Double)]
        FLOATNUMBER,

        #endregion

        #region KeyWords

        [Keyword("true")]
        TRUE,

        [Keyword("false")]
        FALSE,

        [Keyword("void")]
        VOID,

        [Lexeme(GenericToken.KeyWord, "int")]
        INT,

        [Lexeme(GenericToken.KeyWord, "char")]
        CHAR,

        [Lexeme(GenericToken.KeyWord, "float")]
        FLOAT,

        [Lexeme(GenericToken.KeyWord, "if")]
        IF,

        [Lexeme(GenericToken.KeyWord, "else")]
        ELSE,

        [Lexeme(GenericToken.KeyWord, "switch")]
        SWITCH,

        [Lexeme(GenericToken.KeyWord, "case")]
        CASE,

        [Lexeme(GenericToken.KeyWord, "break")]
        BREAK,

        [Lexeme(GenericToken.KeyWord, "default")]
        DEFAULT,

        [Keyword("while")]
        WHILE,

        [Keyword("for")]
        FOR,

        [Lexeme(GenericToken.KeyWord, "return")]
        RETURN,

        #endregion

        #region SugarTokens

        [Lexeme(GenericToken.SugarToken, "=")]
        ASSIGN,

        [Lexeme(GenericToken.SugarToken, "+=")]
        PLUSASSIGN,

        [Lexeme(GenericToken.SugarToken, "-=")]
        MINUSASSIGN,

        [Lexeme(GenericToken.SugarToken, "*=")]
        MULASSIGN,

        [Lexeme(GenericToken.SugarToken, "/=")]
        DIVIDEASSIGN,

        [Lexeme(GenericToken.SugarToken, ",")]
        COMMA,

        [Lexeme(GenericToken.SugarToken, ";")]
        SEMI,

        [Lexeme(GenericToken.SugarToken, ":")]
        COLON,

        [Lexeme(GenericToken.SugarToken, "(")]
        LPAREN,

        [Lexeme(GenericToken.SugarToken, ")")]
        RPAREN,

        [Lexeme(GenericToken.SugarToken, "{")]
        LBRACKET,

        [Lexeme(GenericToken.SugarToken, "}")]
        RBRACKET,

        [Lexeme(GenericToken.SugarToken, "+")]
        PLUS,

        [Lexeme(GenericToken.SugarToken, "-")]
        MINUS,

        [Lexeme(GenericToken.SugarToken, "*")]
        MUL,

        [Lexeme(GenericToken.SugarToken, "/")]
        DIVIDE,

        [Lexeme(GenericToken.SugarToken, ">")]
        GREATER,

        [Lexeme(GenericToken.SugarToken, ">=")]
        GREATEROREQUAL,

        [Lexeme(GenericToken.SugarToken, "<")]
        LESSER,

        [Lexeme(GenericToken.SugarToken, "<=")]
        LESSEROREQUAL,

        [Lexeme(GenericToken.SugarToken, "==")]
        EQUALS,

        [Lexeme(GenericToken.SugarToken, "!=")]
        DIFFERENT,

        [Sugar("++")]
        INCREMENT,

        [Sugar("--")]
        DECREMENT

        #endregion
    }
}