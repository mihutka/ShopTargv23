namespace ShopTARgv23.Models.Kindergarten
{
    public class KindergartenDeleteViewmodel
    {
        public Guid? Id { get; set; }

        public string GroupName { get; set; }

        public int ChildrenCount { get; set; }

        public string KindergartenName { get; set; }

        public string Teacher { get; set; }

        //only in db

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
