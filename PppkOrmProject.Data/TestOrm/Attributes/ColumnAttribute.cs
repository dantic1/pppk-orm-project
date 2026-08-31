namespace PppkOrmProject.Data.TestOrm.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ColumnAttribute : Attribute
{
    public bool IsPrimaryKey { get; set; }
    
    public string Name { get; }
    
    public ColumnAttribute(string name)
    {
        Name = name;
    }
}