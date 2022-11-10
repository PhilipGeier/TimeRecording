using TimeTracking.Domain;

namespace TimeTracking.Data;

public static class DbInitializer
{
    public static void Initialize(DataContext context)
    {
        if (context.Users.Any() && context.TimeRecordings.Any()) 
            return;

        var user1 = new User
        {
            Id = Guid.NewGuid(),
            Email = "philipgeieru@gmail.com",
            FirstName = "Philip",
            LastName = "Geier",
            Password = "test",
            PhoneNumber = "066475091125"
        };

        var user2 = new User
        {
            Id = Guid.NewGuid(),
            Email = "magdalena.huber@gmail.com",
            FirstName = "Magdalena",
            LastName = "Huber",
            Password = "test2",
            PhoneNumber = "00606006060"
        };

        var timeRecordings = new List<TimeRecording>
        {
            new()
            {
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now - TimeSpan.FromHours(8),
                EndTime = DateTime.Now,
                User = user1
            },
            new()
            {
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now - TimeSpan.FromHours(4),
                EndTime = DateTime.Now,
                User = user1
            },
            new()
            {
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now - TimeSpan.FromHours(3),
                EndTime = DateTime.Now,
                User = user2
            },
            new()
            {
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now - TimeSpan.FromHours(1),
                EndTime = DateTime.Now,
                User = user2
            },
            new()
            {
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now - TimeSpan.FromHours(9),
                EndTime = DateTime.Now,
                User = user2
            },
        };
        
        context.TimeRecordings.AddRange(timeRecordings);
        context.SaveChanges();
    }
}