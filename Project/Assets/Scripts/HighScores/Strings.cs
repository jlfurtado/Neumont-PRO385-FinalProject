using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

static class Strings
{
    // MAGIC STRING SEARCH REGEX: [^((?!const).)*".+".+$]


    #region Defaults

    public const string DEFAULT_NAME = "Your Name";

    #endregion

    #region Delimiters

    public const string COMMA = ",";
    public const string COLON = ":";
    public const string SPACE_COLON_SPACE = " : ";
    public const string NEWLINE = "\n";
    public const string PLUS_SYMBOL = "+";

    #endregion

    #region Keys

    public const string SCORE_PREFIX = "Score: ";
    public const string BASE_HIGH_SCORE_TEXT = "High Scores:\n";
    public const string DEFAULT_SCORE_TEXT = DEFAULT_NAME + ":0";
    public const string YOUR_SCORE_TEXT_PREFIX = "Your Score: ";
    public static readonly string[] HIGH_SCORE_KEYS = { "1st", "2nd", "3rd", "4th", "5th" };

    #endregion

    #region Text Effects

    public const string OPEN_COLOR_PREFIX = "<color=\"#";
    public const string OPEN_COLOR_POSTFIX = "\">";
    public const string CLOSE_COLOR = "</color>\n";
    public const string BOLD_PREFIX = "<b>";
    public const string BOLD_POSTFIX = "</b>\n";

    #endregion

}

