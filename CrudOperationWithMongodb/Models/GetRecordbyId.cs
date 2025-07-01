namespace CrudOperationWithMongodb.Models
{
    public class GetRecordbyId
    {
        public bool IsSuccess {  get; set; }

        public string Message { get; set; }

        public InsertRecordRequest data { get; set; }
    }
}
