using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniLink.Dependencies.Enums;

namespace UniLink.API.Data.VO
{
    public class UserVO
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public UserTypeEnum UserType { get; set; }
        public Guid UserId { get; set; }
    }
}
