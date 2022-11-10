using Microsoft.EntityFrameworkCore;
using TimeTracking.Domain;

namespace TimeTracking.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<TimeRecording> TimeRecordings => Set<TimeRecording>();
}