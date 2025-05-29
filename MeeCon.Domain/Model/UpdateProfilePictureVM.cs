using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeeCon.Domain.Model
{
    public class UpdateProfilePictureVM
    {
        public IFormFile ProfilePictureImage { get; set; }
    }
}
