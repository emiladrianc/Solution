using System.Collections.Generic;
using Database.VersioningTool.Migration;

namespace Database.VersioningTool.MigrationProvider
{
	public interface IMigrationProvider
    {
		IReadOnlyList<IMigration> GetMigrations(int currentDatabaseVersion);
    }
}
