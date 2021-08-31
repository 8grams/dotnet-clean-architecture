using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using FirebaseAdmin.Messaging;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Models.Notifications;

namespace SFIDWebAPI.Infrastructure.Notifications.FCM
{
    public class FCMService : IFCMService
    {
        public async Task SendAsync(FCMMessage message)
        {
            var payload = new Message()
            {
                Notification = new Notification
                {
                    Title = message.Title,
                    Body = message.Body
                },
                Data = new Dictionary<string, string>()
                {
                    { "key", message.Key },
                    { "type", message.Type },
                    { "click_action", "FLUTTER_NOTIFICATION_CLICK" }
                }
            };

            if (message.NotificationType.Equals(FCMMessage.TYPE_TOPIC))
            {
                payload.Topic = message.NotificationKey;
            }
            else
            {
                payload.Token = message.NotificationKey;
            }
            
            await FirebaseMessaging.DefaultInstance.SendAsync(payload);  
        }
    }
}
