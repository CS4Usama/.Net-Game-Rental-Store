using GameRentalStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRentalStore.DataAccess.Repository.IRepository
{
	public interface IGameMediaRepository : IRepository<GameMedia>
	{
		void Update(GameMedia obj);
	}
}
