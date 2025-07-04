﻿using MeeCon.Domain.Model.Post;
using MeeCon.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeeCon.Domain.Model.Post
{
    public class Story
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDeleted { get; set; }

        // Foreign key
        public int UserId { get; set; }

        //Navigation properties
        public UDbModel User { get; set; }
    }
}