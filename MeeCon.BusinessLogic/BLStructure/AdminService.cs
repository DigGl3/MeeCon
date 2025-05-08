using MeeCon.BusinessLogic.Interfaces;
using MeeCon.Domain.Model.Post;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeeCon.BusinessLogic.BLStructure
{
    public class AdminService : IAdminService
    {
        public Task<List<Post>> GetReportedPostsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
