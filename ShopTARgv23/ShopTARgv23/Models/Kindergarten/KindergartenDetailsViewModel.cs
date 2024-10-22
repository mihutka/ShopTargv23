namespace ShopTARgv23.Models.Kindergarten
{
    public class KindergartenDetailsViewModel
    {
        public Guid? Id { get; set; }
        public string GroupName { get; set; }
        public int ChildrenCount { get; set; }
        public string KindergartenName { get; set; }
        public string Teacher { get; set; }

        public List<KindergartenImageViewModel> Image { get; set; }
           = new List<KindergartenImageViewModel>();

        //only in db

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


    }
}
