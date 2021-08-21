using System.Collections.Generic;

public static class PaginationValidator {

    private static int PAGESIZE_MIN = 0;

    private static int PAGESIZE_MAX = 100;

    private static int PAGE_MIN = 0;

    private static int PAGE_MAX = 100;

    public static Dictionary<string, string[]> Validate(int pageSize, int page)
    {
        Dictionary<string, string[]> errors = new Dictionary<string, string[]>();

        if(pageSize < PAGESIZE_MIN || pageSize > PAGESIZE_MAX)
        {
            errors.Add("pageSize", new []{"pageSize is not between " + PAGESIZE_MIN.ToString() + " and " + PAGESIZE_MAX.ToString()});
        }
        if(page < PAGE_MIN || page > PAGE_MAX)
        {
            errors.Add("page", new []{"page is not between " + PAGE_MIN.ToString() + " and " + PAGE_MAX.ToString()});
        }

        return errors;
    }
}