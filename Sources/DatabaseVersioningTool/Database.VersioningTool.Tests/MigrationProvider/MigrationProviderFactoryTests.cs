using System;
using Database.VersioningTool.Configuration;
using Database.VersioningTool.MigrationProvider;
using Moq;
using Xunit;

namespace Database.VersioningTool.Tests.MigrationProvider
{
	public class MigrationProviderFactoryTests
    {
		[Fact]
		public void ConfigurationManagerNotSet()
		{
			Assert.Throws<ArgumentNullException>(() => new MigrationProviderFactory(null, MigrationProviderType.File));
		}

		[Fact]
		public void GetFileMigrationProvider()
		{
			Mock<IConfigurationManager> configurationManager = new Mock<IConfigurationManager>();
			MigrationProviderFactory factory = new MigrationProviderFactory(configurationManager.Object, MigrationProviderType.File);
			var migrationProvider = factory.GetMigrationProvider();

			Assert.NotNull(migrationProvider);
			Assert.True(migrationProvider.GetType().Name == typeof(FileMigrationProvider).Name);
		}

		[Fact]
		public void NoMigrationProvider()
		{
			Mock<IConfigurationManager> configurationManager = new Mock<IConfigurationManager>();
			MigrationProviderFactory factory = new MigrationProviderFactory(configurationManager.Object, MigrationProviderType.None);
			var migrationProvider =

			Assert.Throws<ArgumentException>(() => factory.GetMigrationProvider());

		}
	}
}
