namespace WWB.Common.Dtos
{
    public class ApiPageResult<T> where T : class
    {
        public IList<T> List { get; set; }

        public int Total { get; set; }
    }
}