using var db = new BloggingContext();

//Add User
Console.WriteLine("Add User");
var user = new User { Name = "User1" };
db.Users.Add(user);
await db.SaveChangesAsync();
Console.WriteLine(user.Id);


//Get User
var result = await db.Users.FindAsync(user.Id);
Console.WriteLine(result.Name);

//Update User
Console.WriteLine("Update User");
result.Name = "User2";
await db.SaveChangesAsync();
var result2 = await db.Users.FindAsync(user.Id);
Console.WriteLine(result2.Name);



//Delete User
Console.WriteLine("Delete User");
db.Users.Remove(result);
await db.SaveChangesAsync();
var result3 = await db.Users.FindAsync(user.Id);
Console.WriteLine(result3?.Name ?? "null");