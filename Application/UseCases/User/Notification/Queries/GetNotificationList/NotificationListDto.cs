using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.Interfaces.Mappings;
using SFIDWebAPI.Application.Infrastructures.AutoMapper;
using SFIDWebAPI.Application.Misc;

namespace SFIDWebAPI.Application.UseCases.User.Notification.Queries.GetNotificationList
{
    public class NotificationListDto : PaginationDto
    {
        public IList<NotificationData> Data { set; get; }
    }

    public class NotificationData : BaseDtoData, IHaveCustomMapping
    {
        public string Title { set; get; }
        public string Content { set; get; }
        public Attachment Attachment { set; get; }
        public bool IsDeletable { set; get; }
        public bool HasRead { set; get; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.Notification, NotificationData>()
                .ForMember(nDto => nDto.Id, opt => opt.MapFrom(n => n.Id))
                .ForMember(nDto => nDto.Title, opt => opt.MapFrom(n => n.Title))
                .ForMember(nDto => nDto.Content, opt => opt.MapFrom(n => n.Content))
                .ForMember(nDto => nDto.Attachment, opt => opt.MapFrom(n => Resolve( (AutoMapperProfile)configuration, n) ))
                .ForMember(nDto => nDto.IsDeletable, opt => opt.MapFrom(n => n.IsDeletable))
                .ForMember(nDto => nDto.CreatedAt, opt => opt.MapFrom(n => n.CreateDate))
                .ForMember(nDto => nDto.UpdatedAt, opt => opt.MapFrom(n => n.LastUpdateDate));
        }

        public static Attachment Resolve(AutoMapperProfile profile, Domain.Entities.Notification source)
        {
            if (string.IsNullOrEmpty(source.Attachment)) return null;
            
            var link = profile.GetFullUrl(source.Attachment); // get link
            var fileName = link.Split("_").Last(); // get type
            var type = Utils.GetFileExtension(fileName); // get type

            return new Attachment()
            {
                Link = link,
                FileName= fileName,
                Type = type
            };
        }
    }

    public class Attachment
    {
        public string Type { set; get; }
        public string FileName { set; get; }
        public string Link { set; get; }
    }
}
