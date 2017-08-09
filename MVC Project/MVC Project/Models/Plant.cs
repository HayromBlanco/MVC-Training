﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Project.Models
{
    public class Plant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PlantedDate { get; set; }
        public int PlantType { get; set; }
        public int IdParcel { get; set; }
    }
}