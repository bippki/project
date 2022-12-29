using projectus.DAL.Interfaces;
using projectus.Entities;
using projectus.ORMDAL;

namespace projectus.DAL
{
	public class GameRecordDBRepo : IGameRecordDAL
	{
		public GameRecord GetById(int id)
		{
			using (var ctx = new DefaultDBContext())
			{
				return ctx.GameRecords.Find(id);
			}
		}
		public GameRecord GetByUserId(int userId)
		{
			using (var ctx = new DefaultDBContext())
			{
				return ctx.GameRecords.FirstOrDefault(item => item.UserId == userId);
			}
			
		}

		public void Add(ScoreModel mod)
		{
			using (var ctx = new DefaultDBContext())
			{
				ctx.GameRecords.Add(new GameRecord { Score = mod.Score, Date = DateTime.Now, UserId = mod.UserId});
				ctx.SaveChanges();
			}
		}
	}
}
