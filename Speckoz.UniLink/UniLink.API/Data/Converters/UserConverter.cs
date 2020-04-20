using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniLink.API.Data.Converter;
using UniLink.API.Data.VO;
using UniLink.Dependencies.Models;

namespace UniLink.API.Data.Converters
{
    public class UserConverter : IParser<UserVO, UserModel>, IParser<UserModel, UserVO>
    {
        public UserModel Parse(UserVO origin)
        {
            if (origin == null) return new UserModel();
            return new UserModel
            {
                Email = origin.Email,
                Name = origin.Name,
                UserType = origin.UserType,
                UserId = origin.UserId
            };
        }

        public UserVO Parse(UserModel origin)
        {
            if (origin == null) return new UserVO();
            return new UserVO 
            {
                Email = origin.Email,
                Name = origin.Name,
                UserType = origin.UserType,
                UserId = origin.UserId
            };
        }

        public List<UserModel> ParseList(List<UserVO> origin)
        {
            if (origin == null) return new List<UserModel>();
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<UserVO> ParseList(List<UserModel> origin)
        {
            if (origin == null) return new List<UserVO>();
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
