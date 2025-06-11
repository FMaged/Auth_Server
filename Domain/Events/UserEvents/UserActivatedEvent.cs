

using Domain.ValueObjects.User;

namespace Domain.Events.UserEvents
{
    public class UserActivatedEvent
    {
        public DateTime OccurredOn { get; }
        public Guid UserId { get; }
        public UserName UserName { get; }
        public Email Email { get; }

        public UserActivatedEvent(Guid userId, UserName userName, Email email)
        {
            OccurredOn = DateTime.UtcNow;
            UserId = userId;
            UserName = userName;
            Email = email;
        }
    }
}
