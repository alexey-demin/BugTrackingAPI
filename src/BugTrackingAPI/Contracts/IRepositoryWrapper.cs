namespace BugTrackingAPI.Contracts
{
    public interface IRepositoryWrapper
    {
        IProjectRepository Project { get; }
        ITaskRepository Task { get; }
        ITaskStatusRepository TaskStatus { get; }
    }
}
