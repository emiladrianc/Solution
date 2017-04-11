namespace Database.VersioningTool.Migration
{
    public interface IMigrationFile
    {
		int Order { get; }

		string Name { get; }

		string Content { get;  }

		bool IsValid();
	}
}
