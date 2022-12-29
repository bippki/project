using projectus.DAL.Interfaces;
using projectus.BL.Interfaces;
using projectus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectus.BL
{
	public class GameRecordBL : IGameRecordBL
	{
		private IGameRecordDAL _dal;
		public GameRecordBL(IGameRecordDAL dal)
		{
			_dal = dal;
		}

		public GameRecord GetById(int id)
		{
			return _dal.GetById(id);
		}

		public GameRecord GetByUserId(int userId)
		{
			return _dal.GetByUserId(userId);
		}

		public void Add(ScoreModel mod)
		{
			_dal.Add(mod);
		}
	}
}
