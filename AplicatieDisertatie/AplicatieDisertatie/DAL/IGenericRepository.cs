using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicatieDisertatie.DAL
{
	public interface IGenericRepository<T> : IDisposable
	{
		IEnumerable<T> Get();
		T GetByID(object id);
		void Insert(T student);
		void Delete(object id);
		void Update(T student);
		void Save();
	}
}
