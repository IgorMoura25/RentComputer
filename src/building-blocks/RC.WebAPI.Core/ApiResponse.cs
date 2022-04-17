namespace RC.WebAPI.Core
{
    public class ApiResponse
    {
        public string Title { get; }
        public int Status { get; }
        public object Data { get; set; }
        public IEnumerable<string> Errors { get; }

        public ApiResponse(string title, int status)
        {
            Title = title;
            Status = status;
        }

        public ApiResponse(string title, int status, object data)
        {
            Title = title;
            Status = status;
            Data = data;
        }

        public ApiResponse(string title, int status, IEnumerable<string> errors)
        {
            Title = title;
            Status = status;
            Errors = errors;
        }
    }
}
