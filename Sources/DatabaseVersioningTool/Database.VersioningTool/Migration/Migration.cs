using System;
using System.Collections.Generic;
using System.Linq;

namespace Database.VersioningTool.Migration
{
	public class Migration
	{
		public int Version;
		public IReadOnlyList<MigrationFile> MigrationFiles;

		public Migration(int version, IReadOnlyList<MigrationFile> migrationFiles)
		{
			if (version <= 0)
			{
				throw new ArgumentException("Version should be a positive number", "version");
			}

			if (migrationFiles == null)
			{
				throw new ArgumentException("MigrationFiles property should be set", "migrationFiles");
			}

			if (!migrationFiles.Any())
			{
				throw new ArgumentException("MigrationFiles property should contain at least one member", "migrationFiles");
			}

			Version = version;
			MigrationFiles = migrationFiles;
		}
	}
}
