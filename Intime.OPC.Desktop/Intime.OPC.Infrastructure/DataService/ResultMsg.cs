namespace Intime.OPC.Infrastructure.DataService
{
    public class ResultMsg
    {
        public bool IsSuccess { get; set; }
        public string Msg { get; set; }

        public object Data { get; set; }

        public static ResultMsg Success(string msg = "")
        {
            return new ResultMsg
            {
                IsSuccess = true,
                Msg = msg
            };
        }

        public static ResultMsg Failure(string msg)
        {
            return new ResultMsg
            {
                IsSuccess = false,
                Msg = msg
            };
        }
    }
}