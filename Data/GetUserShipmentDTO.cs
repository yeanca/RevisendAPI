namespace RevisendAPI.Data
{
    public class GetUserShipmentDTO
    {
        public int UserId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
