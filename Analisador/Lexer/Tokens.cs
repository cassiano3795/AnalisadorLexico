﻿using sly.lexer;

namespace Analisador.Lexer
{
    public enum Tokens
    {
        [Lexeme(GenericToken.Identifier, IdentifierType.AlphaNumericDash)]
        ID = 1,

        [Lexeme(GenericToken.SugarToken, "=")]
        ASSIGN = 2,

        [Lexeme(GenericToken.SugarToken, ",")]
        COMMA = 3,

        [Lexeme(GenericToken.SugarToken, ";")]
        SEMI = 4,

        [Lexeme(GenericToken.String)]
        STRING = 5,

        [Lexeme(GenericToken.Int)]
        INTEGER = 6,

        [Lexeme(GenericToken.Double)]
        FLOATNUMBER = 7,

        [Lexeme(GenericToken.SugarToken, "+")]
        PLUS = 8,

        [Lexeme(GenericToken.SugarToken, "-")]
        MINUS = 9,

        [Lexeme(GenericToken.SugarToken, "*")]
        MUL = 10,

        [Lexeme(GenericToken.SugarToken, "/")]
        DIVIDE = 11,

        [Lexeme(GenericToken.KeyWord, "int")]
        INT = 12,

        [Lexeme(GenericToken.KeyWord, "char")]
        CHAR = 13,

        [Lexeme(GenericToken.KeyWord, "float")]
        FLOAT = 14,

        EOS = 15,
    }
}