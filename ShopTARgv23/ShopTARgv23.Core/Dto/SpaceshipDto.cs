﻿

namespace ShopTARgv23.Core.Dto
{
    public class SpaceshipDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Type { get; set; }

        public DateTime BuildDate { get; set; }

        public int CargoWeight { get; set; }

        public int Crew { get; set; }

        public int EnginePower { get; set; }

        //only in db

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }
    }
}
