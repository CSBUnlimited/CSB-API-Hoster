using System.Collections.Generic;

namespace CSB_API_Hoster.Controllers.UserResourses
{
    public class UserResponse
    {
        public IEnumerable<UserVM> UserVM { get; set; }
        public bool IsSuccess { get; set; }
        public int Status { get; set; }
    }
}
