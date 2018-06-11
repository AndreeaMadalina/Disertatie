using AplicatieDisertatie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicatieDisertatie.DAL
{
	public class UnitOfWork : IDisposable
	{
		private Entities _context = new Entities();
		private GenericRepository<AspNetUser> _userRepository;
		private GenericRepository<AspNetRole> _roleRepository;
		private GenericRepository<AspNetUserClaim> _userClaimRepository;
		private GenericRepository<AspNetUserLogin> _userLoginRepository;
		private GenericRepository<File> _fileRepository;
		private GenericRepository<ResponseFile> _responseFileRepository;


		public GenericRepository<AspNetUser> UserRepository => _userRepository ?? new GenericRepository<AspNetUser>(_context);

		public GenericRepository<AspNetRole> RoleRepository => _roleRepository ?? new GenericRepository<AspNetRole>(_context);

		public GenericRepository<AspNetUserClaim> UserClaimRepository => _userClaimRepository ?? new GenericRepository<AspNetUserClaim>(_context);

		public GenericRepository<AspNetUserLogin> UserLoginRepository => _userLoginRepository ?? new GenericRepository<AspNetUserLogin>(_context);

		public GenericRepository<File> FileRepository => _fileRepository ?? new GenericRepository<File>(_context);

		public GenericRepository<ResponseFile> ResponseFileRepository => _responseFileRepository ?? new GenericRepository<ResponseFile>(_context);


		public void Save()
		{
			_context.SaveChanges();
		}

		private bool disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}