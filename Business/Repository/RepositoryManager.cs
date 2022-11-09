using CompanyEmployees.Contracts;
using CompanyEmployees.Entities;
using Data;

namespace CompanyEmployees.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly DutchContext _dutchContext;
        private ICompanyRepository? _companyRepository;

        public RepositoryManager(DutchContext dutchContext)
        {
            _dutchContext = dutchContext;
        }

        public ICompanyRepository Company {
            get
            {
                if (_companyRepository == null)

                    _companyRepository = new CompanyRepository(_dutchContext);

                return _companyRepository;
            }
        }

        public void Save()
        {
            _dutchContext.SaveChanges();
        }
    }
}