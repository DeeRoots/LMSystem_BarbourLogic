using LMS.Repositories.General;

namespace LMS.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
       
            IGeneralRepository<T> GeneralRepository<T>() where T : class;

            void Save();
        }
    }
