namespace HR_Platform.Application.SearchFilters.Common;

public record SearchFilterResponse
(
    int TotalCount,
    IEnumerable<object> Results,
    string Message
);
