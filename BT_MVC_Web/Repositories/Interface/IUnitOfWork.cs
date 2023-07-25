namespace BT_MVC_Web.Repositories.Interface
{
    public interface IUnitOfWork
    {
        ICityRepository City { get; }
        IWardRepository Ward { get; }
        IDistrictRepository District { get; }
        IEmployeeRepository Employee { get; }
        IEthnicityRepository Ethnicity { get; }
        IOccupationRepository Occupation { get; }
        Task SaveAsync();
        void Save();
    }
}
