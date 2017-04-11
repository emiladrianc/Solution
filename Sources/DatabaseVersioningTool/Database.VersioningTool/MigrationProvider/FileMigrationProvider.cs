using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Database.VersioningTool.Configuration;
using Database.VersioningTool.Migration;

namespace Database.VersioningTool.MigrationProvider
{
	public class FileMigrationProvider : IMigrationProvider
	{

		private IConfigurationManager _configurationManager;

		public FileMigrationProvider(IConfigurationManager configurationManager)
		{
			if (configurationManager == null)
			{
				throw new ArgumentNullException("Configuration manager is not set", "configurationManager");
			}

			_configurationManager = configurationManager;
		}
		
		public IReadOnlyList<IMigration> GetMigrations(int currentDatabaseVersion)
		{
			String directory = _configurationManager.GetDirectoryMigrationProvider();
			DirectoryInfo directoryInfo = new DirectoryInfo(directory);

			if (!directoryInfo.Exists)
			{
				Console.WriteLine(string.Format("Directory: {0} is not valid", directory));
				return null;
			}

			FileInfo[] fileInfos = directoryInfo.GetFiles("*.sql");
			if(!fileInfos.Any())
			{
				Console.WriteLine(string.Format("Directory: {0} has no files", directory));
				return null;
			}
			
			Dictionary<int, List<IMigrationFile>> migrationFiles = new Dictionary<int, List<IMigrationFile>>();

			foreach (FileInfo fileInfo in fileInfos)
			{
				Tuple<int, int, string, string> migrationInfo = ReadMigrationFile(fileInfo);
				if (migrationInfo == null)
				{
					continue;
				}

				int version = migrationInfo.Item1;

				if (!migrationFiles.ContainsKey(version))
				{
					migrationFiles.Add(version, new List<IMigrationFile>());
				}

				migrationFiles[version].Add(new MigrationFile(migrationInfo.Item2, migrationInfo.Item3, migrationInfo.Item4));
			}

			return migrationFiles.Select(s => new Migration.Migration(s.Key, s.Value)).Where(s => s.Version > currentDatabaseVersion).ToList();
		}

		private Tuple<int, int, string, string> ReadMigrationFile(FileInfo fileInfo)
		{
			string[] parts = fileInfo.Name.Split('_');
			if (parts.Count() != 3)
			{
				Console.WriteLine(string.Format("Could not split file name: {0}", fileInfo.Name));

				return null;
			}

			int version = 0;
			int.TryParse(parts[0], out version);

			int order = 0;
			int.TryParse(parts[1], out order);

			string name = parts[2];

			string content = File.ReadAllText(fileInfo.FullName);

			return new Tuple<int, int, string, string>(version, order, name, content);
		}

	}
}
