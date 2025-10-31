using System.Threading.Tasks;

public class AudioService
{
    public Task InitializeTask()
    {
        return Task.Delay(1000);
    }
}