﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeeCon.Domain.Model.Home
{
    public class PostVM
    {
        public string Content { get; set; }
        public IFormFile Image { get; set; }

    }
}
