using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DaoInterfaces;
using Application.Logic;
using Domain.Dtos;
using Domain.Models;
using Moq;
using Xunit;

namespace MyApp.Tests.Application
{
    public class ProjectLogicTests
    {
        private readonly Mock<IProjectDao> _projectDaoMock;
        private readonly Mock<IUserDao> _userDaoMock;
        private readonly Mock<ITaskSkillDao> _taskSkillDaoMock;
        private readonly ProjectLogic _projectLogic;

        public ProjectLogicTests()
        {
            _projectDaoMock = new Mock<IProjectDao>();
            _userDaoMock = new Mock<IUserDao>();
            _taskSkillDaoMock = new Mock<ITaskSkillDao>();
            _projectLogic = new ProjectLogic(_projectDaoMock.Object, _userDaoMock.Object, _taskSkillDaoMock.Object);
        }

        // CREATE
        [Fact]
        public async Task CreateAsync_ValidData_ReturnsCreatedProject()
        {
            // Arrange
            var dto = new ProjectCreationDto("testuser",
                "Test Project",
                "Test Description",
                true,
                DateOnly.FromDateTime(DateTime.Now),
                DateOnly.FromDateTime(DateTime.Now).AddMonths(1),
                ProjectStatus.Created,
                "TestTag",
                Priority.High);
       

            var user = new User("testuser", "password", null, null, null, null, null, null, null, null);
            var createdProject = new Project(dto.ProjectName, dto.ProjectName, dto.Description, dto.IsInvoicable, dto.StartDate, dto.Deadline, dto.ProjectStatus, dto.TagName, dto.ProjectPriority);

            _userDaoMock
                .Setup(x => x.GetByUsernameAsync(dto.OwnerUsername))
                .ReturnsAsync(user);

            _projectDaoMock
                .Setup(x => x.CreateAsync(It.IsAny<Project>()))
                .ReturnsAsync(createdProject);

            // Act
            var result = await _projectLogic.CreateAsync(dto);

            // Assert
            Assert.Equal(dto.ProjectName, result.ProjectName);
            Assert.Equal(dto.Description, result.Description);
            Assert.Equal(dto.ProjectStatus, result.ProjectStatus);
            Assert.Equal(dto.TagName, result.TagName);
            Assert.Equal(dto.ProjectPriority, result.ProjectPriority);
            _projectDaoMock.Verify(x => x.CreateAsync(It.IsAny<Project>()), Times.Once);
        }

        // UPDATE
        [Fact]
        public async Task UpdateAsync_ProjectNotFound_ThrowsException()
        {
            // Arrange
            var dto = new ProjectBasicDto(1, "Updated Project", "Updated Description", "testuser", true,
                DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now).AddDays(2),
                ProjectStatus.Ongoing, "UpdatedTag", Priority.Medium, new List<TaskProject>());
           

            _projectDaoMock
                .Setup(x => x.GetByIdAsync(dto.ProjectId))
                .ReturnsAsync((Project)null); // Simulate not found.

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _projectLogic.UpdateAsync(dto));
            Assert.Equal("Project not found", exception.Message);
            _projectDaoMock.Verify(x => x.GetByIdAsync(dto.ProjectId), Times.Once);
            _projectDaoMock.Verify(x => x.UpdateAsync(It.IsAny<Project>()), Times.Never);
        }

        // GETBYIDASYNC
        [Fact]
        public async Task GetByIdAsync_ProjectExists_ReturnsBasicDto()
        {
            // Arrange
            var project = new Project
            {
                ProjectId = 1,
                ProjectName = "Test Project",
                Description = "Test Description",
                OwnerUsername = "testuser",
                IsInvoicable = true,
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                Deadline = DateOnly.FromDateTime(DateTime.Now).AddMonths(1),
                ProjectStatus = ProjectStatus.Created,
                TagName = "TestTag",
                ProjectPriority = Priority.High
            };

            _projectDaoMock
                .Setup(x => x.GetByIdAsync(project.ProjectId))
                .ReturnsAsync(project);

            // Act
            var result = await _projectLogic.GetByIdAsync(project.ProjectId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(project.ProjectId, result.ProjectId);
            Assert.Equal(project.ProjectName, result.ProjectName);
            Assert.Equal(project.Description, result.Description);
            Assert.Equal(project.ProjectStatus, result.ProjectStatus);
            Assert.Equal(project.TagName, result.TagName);
            Assert.Equal(project.ProjectPriority, result.ProjectPriority);

            _projectDaoMock.Verify(x => x.GetByIdAsync(project.ProjectId), Times.Once);
        }

        // VALIDATE DATA
        [Fact]
        public async Task ValidateData_InvalidData_ThrowsException()
        {
            // Mock the user data
            var user = new User ( "testuser2","password", null, null, null, null, null, null, null, null);

            // Setup the mock for user retrieval
            _userDaoMock
                .Setup(x => x.GetByUsernameAsync("testuser2"))
                .ReturnsAsync(user);
            // Arrange
            var invalidDto = new ProjectCreationDto("testuser2",
                "", // Invalid ProjectName
                "Test Description",
                true,
                DateOnly.FromDateTime(DateTime.Now),
                DateOnly.FromDateTime(DateTime.Now).AddMonths(1),
                ProjectStatus.Created,
                "TestTag",
                Priority.Medium);
        

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _projectLogic.CreateAsync(invalidDto));
            Assert.Equal("ProjectName cannot be empty.", exception.Message);
        }

        // DUPLICATE PROJECT
        [Fact]
        public async Task DuplicateProject_ProjectNotFound_ThrowsException()
        {
            
            // Arrange
            var originalProject = new ProjectBasicDto(1,
                "Original Project",
                "Original Description",
                "testuser",
                true,
                null, null,
                ProjectStatus.Created,
                "OriginalTag",
                Priority.Low, null);
            // {
            //     ProjectId = 1,
            //     ProjectName = "Original Project",
            //     Description = "Original Description",
            //     ProjectStatus = ProjectStatus.Created,
            //     TagName = "OriginalTag",
            //     ProjectPriority = Priority.Low
            // };

            _projectDaoMock
                .Setup(x => x.GetByIdAsync(originalProject.ProjectId))
                .ReturnsAsync((Project)null); // Simulate not found.

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _projectLogic.DuplicateProject(originalProject, "testuser"));
            Assert.Equal("Project not found", exception.Message);
        }
    }
}
