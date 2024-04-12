using AccountMgt.Model.DTO;

namespace AccountMgt.Model.ResponseModels
{
    public class CreateProfileResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ProfileDto Profile { get; set; } 
    }

}
