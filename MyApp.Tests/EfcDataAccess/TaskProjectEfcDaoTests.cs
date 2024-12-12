using Domain.Dtos;
using Domain.Models;
using EfcDataAccess.DAOs;
using Microsoft.EntityFrameworkCore;
using AppContext = EfcDataAccess.AppContext;
// Your DAO class namespace
// Your models like TaskProject, Project, etc.
// If using any DTOs like SearchTaskProjectParametersDto

namespace MyApp.Tests.EfcDataAccess
{
    public class TaskProjectEfcDaoTests
    {
        private readonly AppContext _context;
        private readonly TaskProjectEfcDao _dao;

        public TaskProjectEfcDaoTests()
        {
            // Set up in-memory database for testing
            var options = new DbContextOptionsBuilder<AppContext>()
                .UseInMemoryDatabase("TestDatabase")  // Unique database name for each test run
                .Options;

            // Create the context using the options above
            _context = new AppContext(options);
            _dao = new TaskProjectEfcDao(_context);
        }

        // Test for CreateAsync
        [Fact]
        public async Task CreateAsync_TaskProjectIsValid_ReturnsCreatedTaskProject()
        {
            // Arrange
            var taskProject = new TaskProject
            {
                ProjectId = 1,
                OwnerUsername = "TestUser",
                TaskStatusEnum = TaskStatusEnum.Created,
                Name = "Test Task",
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                Deadline = DateOnly.FromDateTime(DateTime.Now).AddDays(1)
            };

            // Add a mock project to the in-memory database
            var project = new Project
            {
                ProjectId = 1,
                ProjectName = "Test Project",
                TagName = "test_tag",
                OwnerUsername = "TestUser"
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            // Act
            var result = await _dao.CreateAsync(taskProject);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(taskProject.ProjectId, result.ProjectId);
            Assert.Equal("Test Task", result.Name);
            Assert.Equal(TaskStatusEnum.Created, result.TaskStatusEnum);
        }

        // Test for GetByIdAsync
        [Fact]
        public async Task GetByIdAsync_TaskProjectExists_ReturnsTaskProject()
        {
            // Arrange
            var taskProject = new TaskProject
            {
                
                ProjectId = 2,
                OwnerUsername = "TestUser",
                TaskStatusEnum = TaskStatusEnum.Created,
                Name = "Test Task",
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                Deadline = DateOnly.FromDateTime(DateTime.Now).AddDays(1),
                
                Estimate = null,
                DependentOn = null,
                OrderNo = null,
                Notes = null,
                TaskSkills = new List<TaskSkill>(),
                Project = new Project { ProjectId = 2, ProjectName = "Test Project",
                    TagName = "test_tag",
                    OwnerUsername = "TestUser"}, // Initialize Project
           
            };

            _context.TasksProject.Add(taskProject);
            await _context.SaveChangesAsync();

            // Act
            var result = await _dao.GetByIdAsync(taskProject.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(taskProject.Id, result.Id);
            Assert.Equal(taskProject.Name, result.Name);
        }

        // Test for UpdateAsync
        [Fact]
        public async Task UpdateAsync_TaskProjectExists_UpdatesTaskProject()
        {
            // Arrange
            var taskProject = new TaskProject
            {
                ProjectId = 3,
                OwnerUsername = "TestUser",
                TaskStatusEnum = TaskStatusEnum.Created,
                Name = "Test Task",
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                Deadline = DateOnly.FromDateTime(DateTime.Now).AddDays(1),
                
                  
                Estimate = null,
                DependentOn = null,
                OrderNo = null,
                Notes = null,
                TaskSkills = new List<TaskSkill>(),
                Project = new Project { ProjectId = 3, ProjectName = "Test Project",
                    TagName = "test_tag",
                    OwnerUsername = "TestUser"}, // Initialize Project
            };

            _context.TasksProject.Add(taskProject);
            await _context.SaveChangesAsync();

            taskProject.Name = "Updated Task Name";

            // Act
            await _dao.UpdateAsync(taskProject);

            // Assert
            var updatedTaskProject = await _dao.GetByIdAsync(taskProject.Id);
            Assert.Equal("Updated Task Name", updatedTaskProject.Name);
        }

        // Test for GetTasksUser
        [Fact]
        public async Task GetTasksUser_UsernameExists_ReturnsTaskProjects()
        {
            // Arrange
            var taskProject1 = new TaskProject
            {
                ProjectId = 1,
                OwnerUsername = "TestUser",
                TaskStatusEnum = TaskStatusEnum.Created,
                Name = "Test Task 1",
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                Deadline = DateOnly.FromDateTime(DateTime.Now).AddDays(1),
                
                
            };

            var taskProject2 = new TaskProject
            {
                ProjectId = 1,
                OwnerUsername = "TestUser",
                TaskStatusEnum = TaskStatusEnum.Ongoing,
                Name = "Test Task 2",
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                Deadline = DateOnly.FromDateTime(DateTime.Now).AddDays(2),
                
                
            };

            _context.TasksProject.Add(taskProject1);
            _context.TasksProject.Add(taskProject2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _dao.GetTasksUser("TestUser");

            // Assert
            Assert.Equal(6, result.Count());
        }

        // Test for SearchTasksAsync
        [Fact]
        public async Task SearchTasksAsync_ValidSearchParams_ReturnsMatchingTasks()
        {
            // Arrange
            var taskProject1 = new TaskProject
            {
                ProjectId = 3,
                OwnerUsername = "TestUser",
                TaskStatusEnum = TaskStatusEnum.Created,
                Name = "Test Task Test 1",
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                Deadline = DateOnly.FromDateTime(DateTime.Now).AddDays(1)
            };

            var taskProject2 = new TaskProject
            {
                ProjectId = 3,
                OwnerUsername = "TestUser",
                TaskStatusEnum = TaskStatusEnum.Completed,
                Name = "Test Task Test 2",
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                Deadline = DateOnly.FromDateTime(DateTime.Now).AddDays(2)
            };

            _context.TasksProject.Add(taskProject1);
            _context.TasksProject.Add(taskProject2);
            await _context.SaveChangesAsync();

            var searchParams = new SearchTaskProjectParametersDto
            {
                TaskName = "Test Task Test 1"
            };

            // Act
            var result = await _dao.SearchTasksAsync(searchParams);

            // Assert
            Assert.Single(result);
            Assert.Equal("Test Task Test 1", result.First().Name);
        }

        // Test for GetBySeq
        [Fact]
        public async Task GetBySeq_ValidParams_ReturnsTaskProject()
        {
            // Arrange
            var taskProject = new TaskProject
            {
                ProjectId = 3,
                OwnerUsername = "TestUser",
                TaskStatusEnum = TaskStatusEnum.Created,
                Name = "Test Task",
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                Deadline = DateOnly.FromDateTime(DateTime.Now).AddDays(1),
                SequenceNo = 10
            };

            _context.TasksProject.Add(taskProject);
            await _context.SaveChangesAsync();

            // Act
            var result = await _dao.GetBySeq(3, 10);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(taskProject.Id, result.Id);
        }
    }
}
