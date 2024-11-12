using Application.DaoInterfaces;
using Domain.Dtos;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs
{
    public class TaskSkillEfcDao : ITaskSkillDao
    {
        private readonly AppContext context;

        public TaskSkillEfcDao(AppContext context)
        {
            this.context = context;
        }

        public async Task<TaskSkill> CreateAsync(TaskSkill taskSkill)
        {
            EntityEntry<TaskSkill> added = await context.TaskSkills.AddAsync(taskSkill);
            await context.SaveChangesAsync();
            return added.Entity;
        }

        public async Task<IEnumerable<TaskSkill>> GetAsync(SearchTaskSkillParametersDto searchParameters)
        {
            IQueryable<TaskSkill> query = context.TaskSkills.Include(ts => ts.TaskProject).Include(ts => ts.Skill).AsQueryable();
            if (searchParameters.TaskProjectId != null)
            {
                query = query.Where(ts => ts.TaskProjectId == searchParameters.TaskProjectId);
            }
            if (!string.IsNullOrEmpty(searchParameters.SkillName))
            {
                query = query.Where(ts => ts.SkillName.ToLower().Equals(searchParameters.SkillName.ToLower()));
            }
            if (searchParameters.Proficiency != null)
            {
                query = query.Where(ts => ts.Proficiency == searchParameters.Proficiency);
            }
            List<TaskSkill> result = await query.ToListAsync();
            return result;
        }

        public async Task<TaskSkill?> GetByIdAsync(int id)
        {
            TaskSkill? found = await context.TaskSkills
                .Include(ts => ts.TaskProject)
                .Include(ts => ts.Skill)
                .SingleOrDefaultAsync(ts => ts.Id == id);
            return found;
        }

        public async Task UpdateAsync(TaskSkillBasicDto taskSkill)
        {
            var existingSkill = await context.TaskSkills.FindAsync(taskSkill.TaskSkillId);
            if (existingSkill == null)
            {
                throw new Exception("Task skill not found");
            }

            existingSkill.Proficiency = taskSkill.Proficiency;

            context.TaskSkills.Update(existingSkill);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int taskSkillId)
        {
            var existingSkill = await context.TaskSkills.FindAsync(taskSkillId);
            if (existingSkill == null)
            {
                throw new Exception("Task skill not found");
            }

            context.TaskSkills.Remove(existingSkill);
            await context.SaveChangesAsync();
        }
    }
}
