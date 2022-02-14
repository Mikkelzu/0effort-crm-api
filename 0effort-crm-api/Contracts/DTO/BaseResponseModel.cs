namespace _0effort_crm_api.Contracts.DTO
{
    public class BaseResponseModel
    {
        public bool IsSuccess { get; set; }
        public string[] Errors { get; set; }
        public string Error { get; set; }
    }
}
