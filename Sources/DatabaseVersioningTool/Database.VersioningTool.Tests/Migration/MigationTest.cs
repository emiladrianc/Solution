using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Database.VersioningTool.Migration;
using Xunit;

namespace Database.VersioningTool.Tests.Migration
{
	public class MigationTest
	{
		private const int _validVersion = 1;

		[Fact]
		public void VersionShoudBePositive()
		{
			Assert.Throws<ArgumentException>(() => new Database.VersioningTool.Migration.Migration(-1, null));
		}

		[Fact]
		public void VersionShoudNotBeZero()
		{
			Assert.Throws<ArgumentException>(() => new Database.VersioningTool.Migration.Migration(0, null));
		}

		[Fact]
		public void TestGetVersion()
		{
			List<MigrationFile> migrationFiles = new List<MigrationFile>();
			var migrationFile = new MigrationFile(1, "Test", "Test");

			migrationFiles.Add(migrationFile);
			var migration = new Database.VersioningTool.Migration.Migration(_validVersion, migrationFiles);

			Assert.True(migration.Version == _validVersion);
		}

		[Fact]
		public void MigrationFilesShoudBeSet()
		{
			Assert.Throws<ArgumentException>(() => new Database.VersioningTool.Migration.Migration(_validVersion, null));
		}

		[Fact]
		public void MigrationFilesShoudContainAtLeastAFile()
		{
			Assert.Throws<ArgumentException>(() => new Database.VersioningTool.Migration.Migration(_validVersion, new List<MigrationFile>()));
		}

		[Fact]
		public void TestGetMigrationFiles()
		{

			List<MigrationFile> migrationFiles = new List<MigrationFile>();
			var migrationFile = new MigrationFile(1, "Test", "Test");

			migrationFiles.Add(migrationFile);

			var migration = new Database.VersioningTool.Migration.Migration(_validVersion, migrationFiles);
			Assert.True(migration.MigrationFiles.Any());

			Assert.True(migration.MigrationFiles[0].Order == migrationFile.Order);
			Assert.True(migration.MigrationFiles[0].Name == migrationFile.Name);
			Assert.True(migration.MigrationFiles[0].Content == migrationFile.Content);


		}


	}
}
