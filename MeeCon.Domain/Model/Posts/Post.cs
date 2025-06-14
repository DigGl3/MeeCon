﻿using System.Collections.Generic;
using System;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;
using MeeCon.Domain.Model.User;

namespace MeeCon.Domain.Model.Post
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public int NrOfReports { get; set; }
        public bool IsPrivate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public bool IsDeleted { get; set; }


        // Foreign key
        public int UserId { get; set; }

        //Navigation properties
        public UDbModel User { get; set; }
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
        public ICollection<Report> Reports { get; set; } = new List<Report>();

    }
}