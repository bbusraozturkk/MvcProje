﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
   public class ImageFile
    {
        [Key]
        public int ImageID { get; set; }

        [StringLength(1000)]
        public string ImageName { get; set; }

        [StringLength(1000)]
        public string ImagePath { get; set; }
    }
}
