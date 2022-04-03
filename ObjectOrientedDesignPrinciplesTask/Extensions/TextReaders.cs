using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ObjectOrientedDesignPrinciplesTask.Extensions;

public static class TextReaders
{
    public static string ReadNotWhiteSpaceLine(this TextReader reader)
    {
        string? type = null;
       
        while (type is null || type == String.Empty)
        {
            type = Console.ReadLine();
            if (type is not null)
            {
                type = Regex.Replace(type, @"\s+", " ").Trim();
            }
        }

        return type;
    }
}