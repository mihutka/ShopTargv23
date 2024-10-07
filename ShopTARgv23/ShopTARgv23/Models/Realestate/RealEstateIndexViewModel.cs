namespace ShopTARgv23.Models.Realestate
{
    public class RealEstateIndexViewModel
    {
        public Guid? Id { get; set; }

        public string Location { get; set; }

        public double Size { get; set; }

        public int RoomNumber { get; set; }

        public string BuildingType { get; set; }

        //only in db

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
