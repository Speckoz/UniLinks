using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniLink.API.Data.Converter;
using UniLink.API.Data.VO;
using UniLink.Dependencies.Models;

namespace UniLink.API.Data.Converters
{
    public class UserConverter : IParser<UserVO, UserBaseModel>, IParser<UserBaseModel, UserVO>
    {
        public UserBaseModel Parse(UserVO origin)
        {
            if (origin == null) return new UserBaseModel();
            return new UserBaseModel
            {
                Email = origin.Email,
                Name = origin.Name,
                UserType = origin.UserType,
                UserId = origin.UserId
            };
        }

        public UserVO Parse(UserBaseModel origin)
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

        public List<UserBaseModel> ParseList(List<UserVO> origin)
        {
            if (origin == null) return new List<UserBaseModel>();
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<UserVO> ParseList(List<UserBaseModel> origin)
        {
            if (origin == null) return new List<UserVO>();
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
