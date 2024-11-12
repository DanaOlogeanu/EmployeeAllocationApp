using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.Dtos;
using Domain.Models;

namespace Application.Logic
{
    public class TaskSkillLogic : ITaskSkillLogic
    {
        private readonly ITaskSkillDao taskSkillDao;
        private readonly ISkillDao skillDao;
        private readonly ITaskProjectDao taskProjectDao;

        public TaskSkillLogic(ITaskSkillDao taskSkillDao, ISkillDao skillDao, ITaskProjectDao taskProjectDao)
        {
            this.taskSkillDao = taskSkillDao;
            this.skillDao = skillDao;
            this.taskProjectDao = taskProjectDao;
        }

        public async Task<TaskSkill> CreateAsync(TaskSkillCreationDto dto)
        {
            Skill? skill = await skillDao.GetByNameAsync(dto.SkillName);
            if (skill == null)
            {
                throw new Exception($"Skill with name {dto.SkillName} was not found.");
            }
            
            TaskProject? taskProject = await taskProjectDao.GetByIdAsync(dto.TaskProjectId);
            if (taskProject == null)
            {
                throw new Exception($"Task project with ID {dto.TaskProjectId} was not found.");
            }
            
            ValidateData(dto);
            TaskSkill toCreate = new TaskSkill(dto.TaskProjectId, dto.SkillName, dto.Proficiency);
            TaskSkill created = await taskSkillDao.CreateAsync(toCreate);

            return created;
        }

        private void ValidateData(TaskSkillCreationDto dto)
        {
            if ((int)dto.Proficiency == 0) throw new Exception("Skill proficiency cannot be empty.");
           
        }

        public Task<IEnumerable<TaskSkill>> GetAsync(SearchTaskSkillParametersDto searchParameters)
        {
            return taskSkillDao.GetAsync(searchParameters);
        }

        public async Task<TaskSkillBasicDto> GetByIdAsync(int id)
        {
            TaskSkill? skill = await taskSkillDao.GetByIdAsync(id);
            if (skill == null)
            {
                throw new Exception($"Task skill with id {id} not found");
            }

            return new TaskSkillBasicDto(skill.Id, skill.TaskProjectId, skill.SkillName, skill.Proficiency);
        }

        public async Task UpdateAsync(TaskSkillBasicDto taskSkill)
        {
            await taskSkillDao.UpdateAsync(taskSkill);
        }

        public async Task DeleteAsync(int taskSkillId)
        {
            await taskSkillDao.DeleteAsync(taskSkillId);
        }
    }
}
