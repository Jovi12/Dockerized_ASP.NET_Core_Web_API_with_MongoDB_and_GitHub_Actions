namespace CrudOperationWithMongodb.Models
{
    public class GetRecordbyName
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public List<InsertRecordRequest> data { get; set; }
    }
}
