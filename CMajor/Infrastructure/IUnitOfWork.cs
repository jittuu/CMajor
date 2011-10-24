using System;

namespace CMajor.Infrastructure {
    public interface IUnitOfWork : IDisposable {
        int Commit();
    }
}