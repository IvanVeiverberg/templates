using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Templates.Api.Data;
using Templates.Api.DTOs;
using Templates.Api.Entities;
using Templates.Api.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public UserService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<User>> GetUsersAsync() => await _context.Users.ToListAsync();

    public async Task<User?> GetUserByIdAsync(int id) => await _context.Users.FindAsync(id);

    public async Task<User> CreateUserAsync(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        try
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        catch (DbUpdateException ex)
        {
            if (IsPrimaryKeyViolation(ex))
                throw new InvalidOperationException("A user with the same ID already exists.");

            if (IsUniqueConstraintViolation(ex))
                throw new InvalidOperationException("A user with the same email already exists.");

            throw;
        }
    }

    public async Task<bool> UpdateUserAsync(int id, UserDto userDto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _mapper.Map(userDto, user);
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    private bool IsUniqueConstraintViolation(DbUpdateException ex)
    {
        if (ex.InnerException is PostgresException postgresEx)
        {
            return postgresEx.SqlState == PostgresErrorCodes.UniqueViolation;
        }

        return false;
    }

    private bool IsPrimaryKeyViolation(DbUpdateException ex)
    {
        if (ex.InnerException is PostgresException postgresEx)
        {
            return postgresEx.SqlState == PostgresErrorCodes.UniqueViolation &&
                   postgresEx.ConstraintName == "PK_Users";
        }

        return false;
    }
}