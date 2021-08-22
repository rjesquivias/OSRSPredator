using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class GuidValidator {

    private static string regex = "^[{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?$";

    public static Dictionary<string, string[]> Validate(string id)
    {
        Dictionary<string, string[]> errors = new Dictionary<string, string[]>();

        Match match = Regex.Match(id, regex, RegexOptions.IgnoreCase);
        
        if(!match.Success)
        {
            errors.Add("id", new []{"id must be a valid guid"});
        }

        return errors;
    }
}