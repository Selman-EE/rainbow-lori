namespace Application.Model.Response
{
    using Application.Common;
    using System.Collections.Generic;

    public class ErrorResponse
    {
        public ErrorResponse() { }

        public ErrorResponse(ErrorModel error)
        {
            Errors.Add(error);
        }

        public ICollection<ErrorModel> Errors { get; set; } = new HashSet<ErrorModel>();


    }
}
