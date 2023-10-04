namespace forum.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }  // Stores the unique identifier for the current request.

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    // A boolean property that indicates whether the RequestId should be displayed. 
    // It returns true if RequestId is not null or empty, otherwise false.
}
