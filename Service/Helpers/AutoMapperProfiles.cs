﻿using DTO.Account;
using System.Linq;
using AutoMapper;
using Core.Models.Entities.User;
using DTO.Account.Photo;
using DTO.Posts;
using Core.Models.Entities.Posts;
using DTO.Account.Base;
using Core.Models.Entities.Messages;
using DTO.Messages;
using Extensions.Common;

namespace Service.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            int currentUserId = 0;
            string currentUsername = string.Empty;

            CreateMap<AppUser, DtoMember>()
                .ForMember(dst => dst.IsFollowed, opt => opt.MapFrom(src => src.TheFollowingList.Any(x => x.TheFollowing.UserName == currentUsername && x.IsActive == true)))
                //.ForMember(dst => dst.ProfilePhotoUrl, opt => opt.MapFrom(src => src.TheUserPhotosList.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dst => dst.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));

            CreateMap<UserPhoto, MemberPhotoDTO>();

            CreateMap<AppUser, BaseMemberDTO>();

            CreateMap<DtoMemberUpdate, AppUser>()
                .ForMember(dst => dst.Id, act => act.Ignore())
                .ForMember(dst => dst.Email, act => act.Ignore())
                .ForMember(dst => dst.UserName, act => act.Ignore())
                .ForMember(dst => dst.ProfilePhotoUrl, act => act.Ignore());


            CreateMap<Post, DtoPostResponse>()
                .ForMember(dst => 
                    dst.IsLiked, opt => 
                    opt.MapFrom(src => src.ThePostLikedList.Any(x => x.UserId == currentUserId && x.IsActive == true)));

            CreateMap<DtoPostRequest, Post>().ForMember(dst => dst.CreateDateTime, act => act.Ignore());


            CreateMap<Message, DtoMessageResponse>()
                .ForMember(dst => dst.SenderPhotoUrl, opt => opt.MapFrom(src => src.TheSender.ProfilePhotoUrl))
                .ForMember(dst => dst.RecieverPhotoUrl, opt => opt.MapFrom(src => src.TheReciever.ProfilePhotoUrl));
        }
    }
}
