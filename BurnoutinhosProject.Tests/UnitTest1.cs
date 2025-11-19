using BurnoutinhosProject.Connection;
using BurnoutinhosProject.Repository;
using BurnoutinhosProject.Service; 
using BurnoutinhosProject.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using FluentAssertions;
using BurnoutinhosProject.Controllers;
using Microsoft.AspNetCore.Mvc;


namespace BurnoutinhosProject.Tests;

public class UnitTest1
{

    private readonly AppDbContext _context;
    private readonly SuggestionRepository _repo;
    private readonly SuggestionService _service;
    private readonly UserRepository _repoUser;
    private readonly UserService _serviceUser;

    public UnitTest1()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);
        _repo = new SuggestionRepository(_context);
        _service = new SuggestionService(_repo);
        _repoUser = new UserRepository(_context);
        _serviceUser = new UserService(_repoUser);
    }

    [Fact]
    public async Task CreateNotification_ShouldAddNotificationCorrectly()
    {
        // Arrange
        var notification = new Notification
        {
            Id = 1,
            Message = "Teste de notificação",
            UserId = 1,
            IsRead = false,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var result = await _context.Notification.AddAsync(notification);
        await _context.SaveChangesAsync();

        // Assert
        var fromDb = await _context.Notification.FindAsync(1);
        fromDb.Should().NotBeNull();
        fromDb!.Message.Should().Be("Teste de notificação");
        fromDb.IsRead.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteSuggestion_ShouldRemoveSuggestionCorrectly()
    {
        // Arrange
        var suggestion = new Suggestion
        {
            Id = 1,
            SuggestionDescription = "Sugestão para deletar",
            UserId = 1,
            CreatedAt = DateTime.UtcNow
        };

        await _context.Suggestion.AddAsync(suggestion);
        await _context.SaveChangesAsync();

        // Act
        var deleted = await _service.DeleteAsync(1);

        // Assert
        deleted.Should().BeTrue();
        var fromDb = await _context.Suggestion.FindAsync(1);
        fromDb.Should().BeNull();
    }

    [Fact]
    public async Task CreateTimeBlock_ShouldAddTimeBlockCorrectly()
    {
        // Arrange
        var timeBlock = new TimeBlock
        {
            Id = 1,
            Start = DateTime.UtcNow,
            End = DateTime.UtcNow.AddHours(2),
            Name = "Estudar",
            UserId = 1
        };

        // Act
        await _context.TimeBlock.AddAsync(timeBlock);
        await _context.SaveChangesAsync();

        // Assert
        var fromDb = await _context.TimeBlock.FindAsync(1);
        fromDb.Should().NotBeNull();
        fromDb!.Name.Should().Be("Estudar");
        fromDb.UserId.Should().Be(1);
    }

    [Fact]
    public async Task GetAllSuggestions_ShouldReturnAllSuggestions()
    {
        // Arrange
        var suggestions = new List<Suggestion>
        {
            new Suggestion { Id = 1, SuggestionDescription = "Sugestão 1", UserId = 1, CreatedAt = DateTime.UtcNow },
            new Suggestion { Id = 2, SuggestionDescription = "Sugestão 2", UserId = 2, CreatedAt = DateTime.UtcNow },
            new Suggestion { Id = 3, SuggestionDescription = "Sugestão 3", UserId = 1, CreatedAt = DateTime.UtcNow }
        };

        await _context.Suggestion.AddRangeAsync(suggestions);
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result.Should().HaveCount(3);
        result.Should().Contain(s => s.SuggestionDescription == "Sugestão 1");
    }

    [Fact]
    public async Task CreateTodo_ShouldAddTodoCorrectly()
    {
        // Arrange
        var todo = new Todo
        {
            Id = 1,
            Name = "Tarefa de teste",
            Description = "Descrição da tarefa",
            IsCompleted = false,
            UserId = 1,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        await _context.Todo.AddAsync(todo);
        await _context.SaveChangesAsync();

        // Assert
        var fromDb = await _context.Todo.FindAsync(1);
        fromDb.Should().NotBeNull();
        fromDb!.Name.Should().Be("Tarefa de teste");
        fromDb.IsCompleted.Should().BeFalse();
    }
}
