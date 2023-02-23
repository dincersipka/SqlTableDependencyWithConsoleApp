#region Application

using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;

string ConnectionString = "";

using (SqlTableDependency<User> TableDependency = new SqlTableDependency<User>(ConnectionString)) 
{
    TableDependency.OnChanged += TableOnChanged;
    TableDependency.OnError += TableOnError;
    TableDependency.Start();

    Console.ReadKey();
}

void TableOnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
{
    Console.WriteLine($"\nError: {e.Error.Message} \n");
}

void TableOnChanged(object sender, RecordChangedEventArgs<User> e)
{
    if (e.ChangeType != ChangeType.None) 
    {
        Console.WriteLine($"Type: {e.ChangeType}\n{e.Entity.Id}, {e.Entity.FirstName} {e.Entity.LastName}\n");
    }
}

#endregion

#region Model

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

#endregion