using System.Collections.Generic;

public static class IdValidator {

    public static Dictionary<string, string[]> Validate(long id)
    {
        Dictionary<string, string[]> errors = new Dictionary<string, string[]>();
        
        if(id < 0)
        {
            errors.Add("id", new []{"id can't be negative"});
        }

        return errors;
    }
}