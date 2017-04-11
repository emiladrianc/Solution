using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Database.VersioningTool.Configuration;
using Database.VersioningTool.MigrationProvider;
using Moq;
using Xunit;

namespace Database.VersioningTool.Tests.MigrationProvider
{
    public class FileMigrationProviderTests
    {
		[Fact]
		public void ConfigurationManagerNotSet()
		{
			Assert.Throws<ArgumentNullException>(() => new FileMigrationProvider(null));
		}

		[Fact]
		public void MigrationDirectoryNotValid()
		{
			Mock<IConfigurationManager> configurationManager = new Mock<IConfigurationManager>();
			configurationManager.Setup(c => c.GetDirectoryMigrationProvider()).Returns("afdasdf");

			FileMigrationProvider provider = new FileMigrationProvider(configurationManager.Object);
						
			var migrations = provider.GetMigrations(0);

			Assert.Null(migrations);		
		}

		[Fact]
		public void NoFilesInMigrationDirectory()
		{
			Mock<IConfigurationManager> configurationManager = new Mock<IConfigurationManager>();
			configurationManager.Setup(c => c.GetDirectoryMigrationProvider()).Returns("MigrationFiles/NoFiles");

			FileMigrationProvider provider = new FileMigrationProvider(configurationManager.Object);

			var migrations = provider.GetMigrations(0);

			Assert.Null(migrations);
		}

		[Fact]
		public void IncorrectFileNameFormat()
		{
			Mock<IConfigurationManager> configurationManager = new Mock<IConfigurationManager>();
			configurationManager.Setup(c => c.GetDirectoryMigrationProvider()).Returns("MigrationFiles/IncorrectFileNameFormat");

			FileMigrationProvider provider = new FileMigrationProvider(configurationManager.Object);

			var migrations = provider.GetMigrations(0);

			Assert.NotNull(migrations);
			Assert.False(migrations.Any());
		}

		[Fact]
		public void OneMigrationOneFile()
		{
			Mock<IConfigurationManager> configurationManager = new Mock<IConfigurationManager>();
			configurationManager.Setup(c => c.GetDirectoryMigrationProvider()).Returns("MigrationFiles/OneMigrationOneFile");

			FileMigrationProvider provider = new FileMigrationProvider(configurationManager.Object);

			var migrations = provider.GetMigrations(0);

			Assert.NotNull(migrations);
			Assert.True(migrations.Any());
			Assert.True(migrations[0].IsValid());
			Assert.True(migrations[0].Version == 1);
			Assert.True(migrations[0].MigrationFiles.Any());
			Assert.True(migrations[0].MigrationFiles.Count == 1);
		}

		[Fact]
		public void OneMigrationTwoFiles()
		{
			Mock<IConfigurationManager> configurationManager = new Mock<IConfigurationManager>();
			configurationManager.Setup(c => c.GetDirectoryMigrationProvider()).Returns("MigrationFiles/OneMigrationTwoFiles");

			FileMigrationProvider provider = new FileMigrationProvider(configurationManager.Object);

			var migrations = provider.GetMigrations(0);

			Assert.NotNull(migrations);
			Assert.True(migrations.Any());
			Assert.True(migrations[0].IsValid());
			Assert.True(migrations[0].Version == 1);
			Assert.True(migrations[0].MigrationFiles.Any());
			Assert.True(migrations[0].MigrationFiles.Count == 2);
		}

		[Fact]
		public void MultipleMigrations()
		{
			Mock<IConfigurationManager> configurationManager = new Mock<IConfigurationManager>();
			configurationManager.Setup(c => c.GetDirectoryMigrationProvider()).Returns("MigrationFiles/MultipleMigrations");

			FileMigrationProvider provider = new FileMigrationProvider(configurationManager.Object);

			var migrations = provider.GetMigrations(0);

			Assert.NotNull(migrations);
			Assert.True(migrations.Any());
			Assert.True(migrations[0].IsValid());
			Assert.True(migrations[0].Version == 1);
			Assert.True(migrations[1].IsValid());
			Assert.True(migrations[1].Version == 2);
			Assert.True(migrations[2].IsValid());
			Assert.True(migrations[2].Version == 3);
			Assert.True(migrations[3].IsValid());
			Assert.True(migrations[3].Version == 4);
		}

		[Fact]
		public void MultipleMigrationsMinimimVersion()
		{
			Mock<IConfigurationManager> configurationManager = new Mock<IConfigurationManager>();
			configurationManager.Setup(c => c.GetDirectoryMigrationProvider()).Returns("MigrationFiles/MultipleMigrations");

			FileMigrationProvider provider = new FileMigrationProvider(configurationManager.Object);

			var migrations = provider.GetMigrations(2);

			Assert.NotNull(migrations);
			Assert.True(migrations.Any());
			Assert.True(migrations[2].IsValid());
			Assert.True(migrations[2].Version == 3);
			Assert.True(migrations[3].IsValid());
			Assert.True(migrations[3].Version == 4);
		}


		[Fact]
		public void MultipleMigrationsnoNewMigrations()
		{
			Mock<IConfigurationManager> configurationManager = new Mock<IConfigurationManager>();
			configurationManager.Setup(c => c.GetDirectoryMigrationProvider()).Returns("MigrationFiles/MultipleMigrations");

			FileMigrationProvider provider = new FileMigrationProvider(configurationManager.Object);

			var migrations = provider.GetMigrations(4);

			Assert.NotNull(migrations);
			Assert.False(migrations.Any());
		}

	}
}
