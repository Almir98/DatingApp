using AutoMapper;
using DatingApp.API.Models;
using DatingApp.API.ViewModels;
using DatingApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Mapper
{
    public class Mappers:Profile
    {
        public Mappers()
        {
            CreateMap<User, UserForDetailsVM>().ForMember(e => e.PhotoUrl, a =>
            {
                a.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
            });

            //.ForMember(a => a.Age, e =>
            //{
            //    e.ResolveUsing(d=>d.DateOfBirth.CalculateAge()); 
            //});

            CreateMap<User, UserForListVM>().ForMember(e => e.PhotoUrl, a =>
            {
                a.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
            });

            CreateMap<Photo, PhotosForDetailsVM>().ReverseMap();
            CreateMap<User, UserForUpdateVM>().ReverseMap();

            CreateMap<Photo, PhotoForReturnVM>().ReverseMap();
            CreateMap<Photo, PhotoForCreationVM>().ReverseMap();

            CreateMap<UserVM, User>().ReverseMap();





        }
    }
}
