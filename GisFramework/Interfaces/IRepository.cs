using System.Collections.Generic;
using System.Linq;
using GisFramework.Domains;

namespace GisFramework.Interfaces
{
	public interface IRepository<TMessageDomain> where TMessageDomain : MessageDomain
	{
		void InsertRange(List<TMessageDomain> entities);
		void Update(TMessageDomain messageDomain);
		TMessageDomain GetById(object id);
		IQueryable<TMessageDomain> Table { get; }
	}
}