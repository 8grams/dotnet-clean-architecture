using AutoMapper;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.Interfaces.Mappings;

namespace SFIDWebAPI.Application.UseCases.Admin.Notification.Models
{
    public class NotificationData : BaseDtoData, IHaveCustomMapping
    {
        public string OwnerId { set; get; }
        public string OwnerType { set; get; }
        public string Title { set; get; }
        public string Content { set; get; }
        public string Attachment { set; get; }
        public bool IsDeletable { set; get; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.Notification, NotificationData>()
                .ForMember(bDto => bDto.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(bDto => bDto.OwnerId, opt => opt.MapFrom(b => b.OwnerId))
                .ForMember(bDto => bDto.OwnerType, opt => opt.MapFrom(b => b.OwnerType))
                .ForMember(bDto => bDto.Title, opt => opt.MapFrom(b => b.Title))
                .ForMember(bDto => bDto.Content, opt => opt.MapFrom(b => b.Content))
                .ForMember(bDto => bDto.Attachment, opt => opt.MapFrom(b => b.Attachment))
                .ForMember(bDto => bDto.IsDeletable, opt => opt.MapFrom(b => b.IsDeletable))
                .ForMember(bDto => bDto.CreatedAt, opt => opt.MapFrom(b => b.CreateDate))
                .ForMember(bDto => bDto.UpdatedAt, opt => opt.MapFrom(b => b.LastUpdateDate));
        }
    }


}