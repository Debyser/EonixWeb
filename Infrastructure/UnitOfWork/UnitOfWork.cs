using ApplicationCore.Repositories;
using System.Data;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbTransaction _dbTransaction;
        public IAddressRepository AddressRepository { get; set; }


        public UnitOfWork(IAddressRepository addressRepository, IDbTransaction dbTransaction)
        {
            _dbTransaction = dbTransaction;
            AddressRepository = addressRepository;
        }

        public void Dispose()
        {
            if (_dbTransaction != null && _dbTransaction.Connection != null)
            {
                _dbTransaction.Connection.Close();
                _dbTransaction?.Connection?.Dispose();
            }

            if (_dbTransaction != null) _dbTransaction?.Dispose();
        }

        public Task SaveChangesAsync()
        {
            try
            {

                _dbTransaction.Commit();
            }
            catch (Exception)
            {
                _dbTransaction.Rollback();
            }
            return Task.CompletedTask;
        }
    }
}
