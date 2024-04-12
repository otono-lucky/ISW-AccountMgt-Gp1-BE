namespace AccountMgt.Model.DTO
{
    public class UpdateProfileBalanceDto
    {
        public decimal Balance { get; set; }
        public Guid UserId { get; set; }
    }

}
