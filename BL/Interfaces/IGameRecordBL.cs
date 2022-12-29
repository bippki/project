using projectus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectus.BL.Interfaces
{
	public interface IGameRecordBL
	{
		GameRecord GetById(int id);
		GameRecord GetByUserId(int userId);

		void Add(ScoreModel mod);
	}
}
