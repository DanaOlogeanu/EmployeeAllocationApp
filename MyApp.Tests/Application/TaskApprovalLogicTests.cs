using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DaoInterfaces;
using Application.Logic;
using Domain.Dtos;
using Domain.Models;
using Moq;
using Xunit;

namespace MyApp.Tests.Application;

public class TaskApprovalLogicTests
{
    private readonly Mock<ITaskApprovalDao> _approvalDaoMock;
    private readonly Mock<ITaskProjectDao> _taskProjectDaoMock;
    private readonly TaskApprovalLogic _taskApprovalLogic;

    public TaskApprovalLogicTests()
    {
        _approvalDaoMock = new Mock<ITaskApprovalDao>();
        _taskProjectDaoMock = new Mock<ITaskProjectDao>();
        _taskApprovalLogic = new TaskApprovalLogic(_approvalDaoMock.Object, _taskProjectDaoMock.Object);
    }
    
    //CREATE 
    [Fact]
    public async Task CreateAsync_ValidData_ReturnsCreatedTaskApproval()
    {
        // Arrange
        var dto = new TaskApprovalCreationDto(1, "testuser", ApprovalStatus.Requested);
        // {
        //     TaskProjectId = 1,
        //     OwnerUsername = "testuser",
        //     Status = ApprovalStatus.Requested
        // };
        
        var createdTaskApproval = new TaskApproval(1, dto.OwnerUsername, dto.Status);
        
        _approvalDaoMock
            .Setup(x => x.CreateAsync(It.IsAny<TaskApproval>()))
            .ReturnsAsync(createdTaskApproval);

        // Act
        var result = await _taskApprovalLogic.CreateAsync(dto);

        // Assert
        Assert.Equal(dto.TaskProjectId, result.TaskProjectId);
        Assert.Equal(dto.OwnerUsername, result.OwnerUsername);
        Assert.Equal(dto.Status, result.Status);
        
        _approvalDaoMock.Verify(x => x.CreateAsync(It.IsAny<TaskApproval>()), Times.Once);
    }

    //UPDATE
    [Fact]
    public async Task UpdateAsync_TaskApprovalNotFound_ThrowsException()
    {
        // Arrange
        var dto = new TaskApprovalBasicDto(1, null, null, ApprovalStatus.Approved, "Updated comments", null);
        // {
        //     TaskApprovalId = 1,
        //     Status = ApprovalStatus.Approved,
        //     Comments = "Updated comments"
        // };
        
        _approvalDaoMock
            .Setup(x => x.GetByIdAsync(dto.TaskApprovalId))
            .ReturnsAsync((TaskApproval)null); // Simulate not found.

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _taskApprovalLogic.UpdateAsync(dto));
        Assert.Equal("Task approval not found", exception.Message);

        _approvalDaoMock.Verify(x => x.GetByIdAsync(dto.TaskApprovalId), Times.Once);
        _approvalDaoMock.Verify(x => x.UpdateAsync(It.IsAny<TaskApproval>()), Times.Never);
    }

    //GETBYIDASYNC
    [Fact]
    public async Task GetByIdAsync_TaskApprovalExists_ReturnsBasicDto()
    {
        // Arrange
        var taskApproval = new TaskApproval
        {
            Id = 1,
            TaskProjectId = 10,
            OwnerUsername = "testuser",
            Status = ApprovalStatus.Requested,
            Comments = "Test comments",
            Date = DateOnly.FromDateTime(DateTime.Now)
        };

        _approvalDaoMock
            .Setup(x => x.GetByIdAsync(taskApproval.Id))
            .ReturnsAsync(taskApproval);

        // Act
        var result = await _taskApprovalLogic.GetByIdAsync(taskApproval.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(taskApproval.Id, result.TaskApprovalId);
        Assert.Equal(taskApproval.TaskProjectId, result.TaskProjectId);
        Assert.Equal(taskApproval.OwnerUsername, result.OwnerUsername);
        Assert.Equal(taskApproval.Status, result.Status);
        Assert.Equal(taskApproval.Comments, result.Comments);

        _approvalDaoMock.Verify(x => x.GetByIdAsync(taskApproval.Id), Times.Once);
    }

    
    [Fact]
    public async Task ValidateData_InvalidData_ThrowsException()
    {
        // Arrange
        var invalidDto = new TaskApprovalCreationDto(0, " ", ApprovalStatus.Requested);
        // {
        //     TaskProjectId = 0, // Invalid ID
        //     OwnerUsername = "",
        //     Status = ApprovalStatus.Requested
        // };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _taskApprovalLogic.CreateAsync(invalidDto));
        Assert.Equal("Invalid TaskProjectId.", exception.Message);
    }

}