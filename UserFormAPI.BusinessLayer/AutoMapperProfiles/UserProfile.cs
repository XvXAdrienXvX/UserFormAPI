using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserFormAPI.BusinessLayer.DTO;
using UserFormAPI.DataAccessLayer.Models;

namespace UserFormAPI.BusinessLayer.AutoMapperProfiles
{
    public class UserProfile : Profile 
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>()
                    .ForAllOtherMembers(opt => opt.UseDestinationValue());
            CreateMap<UserDTO, User>();

        }         
    }
}
