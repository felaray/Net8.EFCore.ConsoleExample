using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

//add time test
var stopwatch = new Stopwatch();
stopwatch.Start();

await TestMethod_addByForeach();
Console.WriteLine($"addByForeach: {stopwatch.ElapsedMilliseconds} ms");
stopwatch.Restart();

await TestMethod_addList();
Console.WriteLine($"addList: {stopwatch.ElapsedMilliseconds} ms");
stopwatch.Restart();

await TestMethod_addByWhenAll();
Console.WriteLine($"addByWhenAll: {stopwatch.ElapsedMilliseconds} ms");
stopwatch.Restart();

await TestMethod_addByParallel();
Console.WriteLine($"addByParallel: {stopwatch.ElapsedMilliseconds} ms");
stopwatch.Stop();


#region Test Methods

async Task TestMethod_addList()
{
    var names = new List<string>();
    for (int i = 0; i < 10000; i++)
        names.Add($"User{i}");

    await AddUserListAsync(names);
}

async Task TestMethod_addByForeach()
{
    var names = new List<string>();
    for (int i = 0; i < 10000; i++)
        names.Add($"User{i}");

    foreach (var name in names)
        await AddUserAsync(name);
}

//以平行寫入
async Task TestMethod_addByWhenAll()
{
    var stopwatch = new Stopwatch();
    var names = new List<string>();
    for (int i = 0; i < 10000; i++)
        names.Add($"User{i}");

    await Task.WhenAll(names.Select(name => AddUserAsync(name)));

    stopwatch.Stop();
}

//以parallel寫入
async Task TestMethod_addByParallel()
{ 
    var names = new List<string>();
    for (int i = 0; i < 10000; i++)
        names.Add($"User{i}");

    await Task.WhenAll(names.AsParallel().Select(name => AddUserAsync(name)));
}

#endregion



#region Database

// AddUserAsync method
async Task AddUserAsync(string name)
{
        using (var db = new BloggingContext())
        {

            var user = new User { Name = name };
            db.Users.Add(user);
            await db.SaveChangesAsync();
            //Console.WriteLine(user.Id);
        }
}

// AddUserListAsync method
async Task AddUserListAsync(List<string> names)
{
    using (var db = new BloggingContext())
    {
        var users = names.Select(name => new User { Name = name });
        db.Users.AddRange(users);
        await db.SaveChangesAsync();
    }
}

// GetUserAsync method
async Task<User> GetUserAsync(int id)
{
    using (var db = new BloggingContext())
    {
        return await db.Users.FindAsync(id);
    }
}

// UpdateUserAsync method
async Task UpdateUserAsync(int id, string newName)
{
    using (var db = new BloggingContext())
    {
        var user = await db.Users.FindAsync(id);
        if (user != null)
        {
            user.Name = newName;
            await db.SaveChangesAsync();
        }
    }
}

// UpdateUserListAsync method
async Task UpdateUserListAsync(List<int> ids, string newName)
{
    using (var db = new BloggingContext())
    {
        var users = await db.Users.Where(u => ids.Contains(u.Id)).ToListAsync();
        foreach (var user in users)
            user.Name = newName;
        await db.SaveChangesAsync();
    }
}

// DeleteUserAsync method
async Task DeleteUserAsync(int id)
{
    using (var db = new BloggingContext())
    {
        var user = await db.Users.FindAsync(id);
        if (user != null)
        {
            db.Users.Remove(user);
            await db.SaveChangesAsync();
        }
    }
}

// clear all users
async Task ClearAllUsersAsync()
{
    using (var db = new BloggingContext())
    {
        var users = await db.Users.ToListAsync();
        db.Users.RemoveRange(users);
        await db.SaveChangesAsync();
    }
}

//get count
async Task<int> GetCountAsync()
{
    using (var db = new BloggingContext())
    {
        return await db.Users.CountAsync();
    }
}

#endregion
