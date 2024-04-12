namespace AccountMgt.Model.ResponseModels
{
    public class UpdateProfileBalanceResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public decimal Balance { get; set; }
    }

}
