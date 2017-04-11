using System;
using System.Collections.Generic;
using System.Linq;

namespace Database.VersioningTool.Migration
{
	public class Migration : IMigration
	{
		public int Version { get; private set; }
		public IReadOnlyList<IMigrationFile> MigrationFiles { get; private set; }

		public Migration(int version, IReadOnlyList<IMigrationFile> migrationFiles)
		{			
			if (migrationFiles == null)
			{
				throw new ArgumentNullException("MigrationFiles property should be set", "migrationFiles");
			}
			
			Version = version;
			MigrationFiles = migrationFiles;
		}

		public bool IsValid()
		{
			if (Version <= 0)
				return false;

			if (!MigrationFiles.Any())
				return false;
			
			if(!MigrationFiles.Any(m=>m.IsValid()))
				return false;

			return true;
		}

		
	}
}
