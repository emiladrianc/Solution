using System;
using System.Collections.Generic;
using Database.VersioningTool.Migration;
using Moq;
using Xunit;

namespace Database.VersioningTool.Tests.Migration
{
	public class MigationTest
	{
		private const int _validVersion = 1;
		
		[Fact]
		public void MigrationFilesShoudBeSet()
		{
			Assert.Throws<ArgumentNullException>(() => new Database.VersioningTool.Migration.Migration(_validVersion, null));
		}

		[Fact]
		public void TestGetMethods()
		{
			List<IMigrationFile> migrationFiles = new List<IMigrationFile>();

			var migration = new Database.VersioningTool.Migration.Migration(_validVersion, migrationFiles);

			Assert.True(migration.Version == _validVersion, String.Format("Version: Expected: {0}, actual: {1}", _validVersion, migration.Version));
			Assert.True(migration.MigrationFiles == migrationFiles, "Migration files are not the same");
		}
		
		[Fact]
		public void VersionShoudBePositive()
		{
			List<IMigrationFile> migrationFiles = new List<IMigrationFile>();

			var migration = new Database.VersioningTool.Migration.Migration(-1, migrationFiles);
			Assert.False(migration.IsValid(), "Migration is valid");
		}

		[Fact]
		public void VersionShoudNotBeZero()
		{
			List<IMigrationFile> migrationFiles = new List<IMigrationFile>();

			var migration = new Database.VersioningTool.Migration.Migration(0, migrationFiles);
			Assert.False(migration.IsValid(), "Migration is valid");
		}

		[Fact]
		public void MigrationFilesShoudContainAtLeastAFile()
		{
			List<IMigrationFile> migrationFiles = new List<IMigrationFile>();

			var migration = new Database.VersioningTool.Migration.Migration(_validVersion, migrationFiles);
			Assert.False(migration.IsValid(), "Migration is valid");
		}

		[Fact]
		public void MigrationFilesShoudContainAtLeastAValidFile()
		{
			List<IMigrationFile> migrationFiles = new List<IMigrationFile>();

			Mock<IMigrationFile> migrationFile = new Mock<IMigrationFile>();
			migrationFile.Setup(x => x.IsValid()).Returns(false);
			migrationFiles.Add(migrationFile.Object);

			var migration = new Database.VersioningTool.Migration.Migration(_validVersion, migrationFiles);
			Assert.False(migration.IsValid(), "Migration is valid");
		}

		[Fact]
		public void IsValid()
		{
			List<IMigrationFile> migrationFiles = new List<IMigrationFile>();

			Mock<IMigrationFile> migrationFile = new Mock<IMigrationFile>();
			migrationFile.Setup(x => x.IsValid()).Returns(true);
			migrationFiles.Add(migrationFile.Object);

			var migration = new Database.VersioningTool.Migration.Migration(_validVersion, migrationFiles);
			Assert.True(migration.IsValid(), "Migration is not valid");
		}
	}
}
