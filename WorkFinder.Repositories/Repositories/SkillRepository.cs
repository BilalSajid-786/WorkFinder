using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;
using WorkFinder.Repositories.DbContext;
using WorkFinder.RepositoryContracts;

namespace WorkFinder.Repositories.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly DapperDbContext _dapperDbContext;
        public SkillRepository(DapperDbContext dapperDbContext)
        {
            _dapperDbContext = dapperDbContext;
        }

        public async Task<IEnumerable<Skill>> GetSkillByName(string searchName)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[GetSkillByName]";
            var parameters = new DynamicParameters();
            parameters.Add("@SearchName", searchName);
            return await connection.QueryAsync<Skill>(sql, parameters,commandType:System.Data.CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Skill>> GetSkills()
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[GetSkills]";
            return await connection.QueryAsync<Skill>(sql,commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<int> InsertSkill(Skill skill)
        {
            using var connection = _dapperDbContext.CreateConnection();
            var sql = "[InsertSkill]";
            var parameters = new DynamicParameters();
            parameters.Add("@SkillName", skill.SkillName);
            return await connection.ExecuteScalarAsync<int>(sql, parameters, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
