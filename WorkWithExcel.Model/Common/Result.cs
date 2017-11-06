using WorkWithExcel.Abstract.Common;

namespace WorkWithExcel.Model.Common
{
    public class Result :IResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
