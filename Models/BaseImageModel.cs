namespace Server.Models
{
    public class BaseImageModel : BaseModel
    {
        public string ImageType { get; set; }
        
        public string ImageName { get; set; }
        
        public string ImagePath { get; set; }   
    }
}