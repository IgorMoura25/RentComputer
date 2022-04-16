namespace RC.Catalog.API.Data.Dapper
{
    public class AddProcedureDTO
    {
        public Guid UniversalId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
