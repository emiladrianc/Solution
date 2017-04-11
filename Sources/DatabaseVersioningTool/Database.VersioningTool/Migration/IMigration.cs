using System.Collections.Generic;

namespace Database.VersioningTool.Migration
{
	public interface IMigration
	{
		int Version { get; }

		IReadOnlyList<IMigrationFile> MigrationFiles { get; }

		bool IsValid();
	}
}
