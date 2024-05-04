using BurgerRoyale.Payment.Application.Models;

namespace BurgerRoyale.Payment.Application.Extensions;

public static class NotificationModelConverter
{
    public static T ConvertTo<T> (this NotificationModel notificationModel) where T : NotificationModel, new()
    {
        var model = new T();
        model.AddNotifications(notificationModel.Notifications);
        return model;
    }
}