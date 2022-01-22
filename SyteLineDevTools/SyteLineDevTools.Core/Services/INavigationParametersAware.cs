namespace SyteLineDevTools.MVVM.Services
{
    public interface INavigationParametersAware
    {
        void NavigatedTo(Dictionary<string, object> parameters);
        
    }
}
